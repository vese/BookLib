using BookLib.Data;
using BookLib.Data.ViewModels;
using BookLib.Models.DBModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookLib.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Books/SortParams
        [HttpGet]
        [Route("sortparams")]
        public IActionResult SortParams()
        {
            var response = new
            {
                ReleaseYears = _context.Book.Select(b => b.ReleaseYear).Distinct().ToList(),
                Authors = _context.Author.Select(a => a.Name).ToList(),
                Publishers = _context.Publisher.Select(a => a.Name).ToList(),
                Series = _context.Series.Select(a => a.Name).ToList(),

                Categories = _context.Category.Select(a => new
                {
                    Category = a.Name,
                    Genres = a.Genres.Select(g => g.Name)
                }).ToList()
            };

            return new OkObjectResult(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        // GET: api/Books/Filter
        [HttpGet]
        [Route("filter")]
        public IActionResult Filter(string inName, int? releaseYear, string author, string publisher, string series, string category, string genre, bool? hasFree, SortProperty sort, string order)
        {
            int? authorId = _context.Author.FirstOrDefault(el => el.Name == author)?.Id;
            int? publisherId = _context.Publisher.FirstOrDefault(el => el.Name == publisher)?.Id;
            int? seriesId = _context.Publisher.FirstOrDefault(el => el.Name == series)?.Id;
            int? categoryId = _context.Category.FirstOrDefault(el => el.Name == category)?.Id;
            int? genreId = _context.Genre.FirstOrDefault(el => el.Name == genre)?.Id;

            var books = _context.Book.Where(b =>
            (string.IsNullOrWhiteSpace(inName) || b.Name.Contains(inName)) &&
            (releaseYear == null || b.ReleaseYear == releaseYear) &&
            (hasFree == null || b.Availability.FreeCount > 0) &&
            (authorId == null || b.IdAuthor == authorId) &&
            (publisherId == null || b.IdPublisher == publisherId) &&
            (seriesId == null || b.IdSeries != null && b.IdSeries == seriesId) &&
            (categoryId == null || b.IdCategory == categoryId) &&
            (genreId == null || b.IdGenre == genreId)
            ).Select(b => new
            {
                Id = b.Id,
                Name = b.Name,
                CroppedDescription = b.Description.Substring(0, 100),
                ReleaseYear = b.ReleaseYear,
                Author = b.IdAuthorNavigation.Name,
                Publisher = b.IdPublisherNavigation.Name,
                Category = b.IdCategoryNavigation.Name,
                Genre = b.IdGenreNavigation.Name,
                Series = b.IdSeriesNavigation == null ? null : b.IdSeriesNavigation.Name,
                CommentsCount = b.Comments.Count(),
                AverageMark = b.Comments.Any() ? (int)b.Comments.Sum(c => c.Mark) / b.Comments.Count() : 0,
                FreeCount = b.Availability.FreeCount
            }).ToList();

            bool desc = (order ?? default(string)) == "desc";

            switch (sort)
            {
                case SortProperty.Name:
                    books = books.OrderBy(b => desc ? default(string) : b.Name).OrderByDescending(b => desc ? b.Name : default(string)).ToList();
                    break;
                case SortProperty.Author:
                    books = books.OrderBy(b => desc ? default(string) : b.Author).OrderByDescending(b => desc ? b.Author : default(string)).ToList();
                    break;
                case SortProperty.ReleaseYear:
                    books = books.OrderBy(b => desc ? default(int) : b.ReleaseYear).OrderByDescending(b => desc ? b.ReleaseYear : default(int)).ToList();
                    break;
                case SortProperty.Mark:
                    books = books.OrderBy(b => desc ? default(int) : b.AverageMark).OrderByDescending(b => desc ? b.AverageMark : default(int)).ToList();
                    break;
            }

            return new OkObjectResult(JsonConvert.SerializeObject(books, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        // GET: api/Books
        [HttpGet]
        public IActionResult GetBook(int id, bool fullInfo = false)
        {
            if (!BookExists(id))
            {
                return NotFound();
            }

            var libBook = _context.Book.Find(id);

            if (fullInfo)
            {
                var book = new
                {
                    Name = libBook.Name,
                    Isbn = libBook.Isbn,
                    Description = libBook.Description,
                    ReleaseYear = libBook.ReleaseYear,
                    Author = libBook.IdAuthorNavigation.Name,
                    Publisher = libBook.IdPublisherNavigation.Name,
                    Category = libBook.IdCategoryNavigation.Name,
                    Genre = libBook.IdGenreNavigation.Name,
                    Series = libBook.IdSeriesNavigation == null ? null : libBook.IdSeriesNavigation.Name,
                    CommentsCount = libBook.Comments.Count(),
                    AverageMark = libBook.Comments.Any() ? (int)libBook.Comments.Sum(c => c.Mark) / libBook.Comments.Count() : 0,
                    Comments = libBook.Comments.Select(c => new
                    {
                        Text = c.Text,
                        Mark = c.Mark
                    }).ToList(),
                    FreeCount = libBook.Availability.FreeCount
                };

                return new OkObjectResult(JsonConvert.SerializeObject(book, new JsonSerializerSettings { Formatting = Formatting.Indented }));
            }
            else
            {
                var book = new
                {
                    Isbn = libBook.Isbn,
                    Description = libBook.Description,
                    Comments = libBook.Comments.Select(c => new
                    {
                        Text = c.Text,
                        Mark = c.Mark
                    }).ToList()
                };

                return new OkObjectResult(JsonConvert.SerializeObject(book, new JsonSerializerSettings { Formatting = Formatting.Indented }));
            }
        }

        // PUT: api/Books
        [HttpPut]
        [Authorize(Roles = "admin")]
        public IActionResult PutBook(int id, [FromBody] ViewBook book)
        {
            var t = User;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!BookExists(id))
            {
                return NotFound();
            }

            var libBook = _context.Book.Find(id);

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (libBook.Name != book.Name)
                    {
                        libBook.Name = book.Name;
                    }

                    if (libBook.Isbn != book.Isbn)
                    {
                        if (_context.Book.Any(b => b.Isbn == book.Isbn))
                        {
                            ModelState.TryAddModelError("db_error", $"Book with ISBN = {book.Isbn} already exists.");
                            return BadRequest(ModelState);
                        }
                        libBook.Isbn = book.Isbn;
                    }

                    if (libBook.Description != book.Description)
                    {
                        libBook.Description = book.Description;
                    }

                    if (libBook.ReleaseYear != book.ReleaseYear)
                    {
                        libBook.ReleaseYear = book.ReleaseYear;
                    }

                    if (libBook.IdAuthorNavigation.Name != book.Author)
                    {
                        var oldAuthor = libBook.IdAuthorNavigation;

                        if (_context.Author.Any(a => a.Name == book.Author))
                        {
                            libBook.IdAuthor = _context.Author.First(a => a.Name == book.Author).Id;
                        }
                        else
                        {
                            var newAuthor = _context.Author.Add(new Author
                            {
                                Name = book.Author
                            });
                            _context.SaveChanges();
                            libBook.IdAuthor = newAuthor.Entity.Id;
                        }
                        if (!oldAuthor.Books.Any())
                        {
                            _context.Author.Remove(oldAuthor);
                        }
                    }

                    if (libBook.IdPublisherNavigation.Name != book.Publisher)
                    {
                        var oldPublisher = libBook.IdPublisherNavigation;

                        if (_context.Publisher.Any(p => p.Name == book.Publisher))
                        {
                            libBook.IdPublisher = _context.Publisher.First(p => p.Name == book.Publisher).Id;
                        }
                        else
                        {
                            var newPublisher = _context.Publisher.Add(new Publisher
                            {
                                Name = book.Publisher
                            });
                            _context.SaveChanges();
                            libBook.IdPublisher = newPublisher.Entity.Id;
                        }
                        if (!oldPublisher.Books.Any())
                        {
                            _context.Publisher.Remove(oldPublisher);
                        }
                    }

                    #region Series
                    var oldSeries = libBook.IdSeriesNavigation;

                    if (string.IsNullOrWhiteSpace(book.Series))
                    {
                        libBook.IdSeries = null;
                    }
                    else if (libBook.IdSeries == null || libBook.IdSeriesNavigation.Name != book.Series)
                    {
                        if (_context.Series.Any(s => s.Name == book.Series))
                        {
                            libBook.IdSeries = _context.Series.First(s => s.Name == book.Series).Id;
                        }
                        else
                        {
                            var newSeries = _context.Series.Add(new Series
                            {
                                Name = book.Series
                            });
                            _context.SaveChanges();
                            libBook.IdSeries = newSeries.Entity.Id;
                        }
                    }
                    if (oldSeries != null && !oldSeries.Books.Any())
                    {
                        _context.Series.Remove(oldSeries);
                    }
                    #endregion

                    #region Category & Genre
                    var oldCategory = libBook.IdCategoryNavigation;
                    var oldGenre = libBook.IdGenreNavigation;
                    if (libBook.IdCategoryNavigation.Name != book.Category)
                    {
                        if (_context.Category.Any(c => c.Name == book.Category))
                        {
                            if (_context.Genre.Any(g => g.Name == book.Genre && g.IdCategoryNavigation.Name == book.Category))
                            {
                                //переместить
                                libBook.IdCategory = _context.Category.First(c => c.Name == book.Category).Id;

                                libBook.IdGenre = _context.Genre.First(g => g.Name == book.Genre && g.IdCategoryNavigation.Name == book.Category).Id;
                            }
                            else
                            {
                                //добавить жанр
                                libBook.IdCategory = _context.Category.First(c => c.Name == book.Category).Id;

                                var newGenre = _context.Genre.Add(new Genre
                                {
                                    Name = book.Genre,
                                    IdCategory = libBook.IdCategory
                                });
                                _context.SaveChanges();
                                libBook.IdGenre = newGenre.Entity.Id;
                            }
                        }
                        else
                        {
                            //добавить категорию и жанр
                            var newCategory = _context.Category.Add(new Category
                            {
                                Name = book.Category
                            });
                            _context.SaveChanges();
                            libBook.IdCategory = newCategory.Entity.Id;

                            var newGenre = _context.Genre.Add(new Genre
                            {
                                Name = book.Genre,
                                IdCategory = newCategory.Entity.Id
                            });
                            _context.SaveChanges();
                            libBook.IdGenre = newGenre.Entity.Id;
                        }
                    }
                    else if (libBook.IdGenreNavigation.Name != book.Genre)
                    {
                        if (_context.Genre.Any(g => g.Name == book.Genre && g.IdCategoryNavigation.Name == book.Category))
                        {
                            libBook.IdGenre = _context.Genre.First(g => g.Name == book.Genre && g.IdCategoryNavigation.Name == book.Category).Id;
                        }
                        else
                        {
                            var newGenre = _context.Genre.Add(new Genre
                            {
                                Name = book.Genre,
                                IdCategory = _context.Category.First(c => c.Name == book.Category).Id
                            });
                            _context.SaveChanges();
                            libBook.IdGenre = newGenre.Entity.Id;
                        }
                    }
                    if (!oldGenre.Books.Any())
                    {
                        _context.Genre.Remove(oldGenre);
                        _context.SaveChanges();
                    }
                    if (!oldCategory.Books.Any())
                    {
                        _context.Category.Remove(oldCategory);
                    }
                    #endregion

                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    ModelState.TryAddModelError("db_error", "Can not perform database action.");
                    return BadRequest(ModelState);
                }
            }

            return new OkResult();
        }

        // POST: api/Books
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult PostBook(int count, [FromBody] ViewBook book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.Book.Any(b => b.Isbn == book.Isbn || b.Name == book.Name && b.IdAuthorNavigation.Name == book.Name))
            {
                ModelState.TryAddModelError("db_error", $"Book already exists.");
                return BadRequest(ModelState);
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    #region Author
                    int authorId;
                    if (_context.Author.Any(a => a.Name == book.Author))
                    {
                        authorId = _context.Author.First(a => a.Name == book.Author).Id;
                    }
                    else
                    {
                        var newAuthor = _context.Author.Add(new Author
                        {
                            Name = book.Author
                        });
                        _context.SaveChanges();
                        authorId = newAuthor.Entity.Id;
                    }
                    #endregion

                    #region Publisher
                    int publisherId;
                    if (_context.Publisher.Any(p => p.Name == book.Publisher))
                    {
                        publisherId = _context.Publisher.First(p => p.Name == book.Publisher).Id;
                    }
                    else
                    {
                        var newPublisher = _context.Publisher.Add(new Publisher
                        {
                            Name = book.Publisher
                        });
                        _context.SaveChanges();
                        publisherId = newPublisher.Entity.Id;
                    }
                    #endregion

                    #region Series
                    int? seriesId = null;
                    if (!string.IsNullOrWhiteSpace(book.Series))
                    {
                        if (_context.Series.Any(s => s.Name == book.Series))
                        {
                            seriesId = _context.Series.First(s => s.Name == book.Series).Id;
                        }
                        else
                        {
                            var newSeries = _context.Series.Add(new Series
                            {
                                Name = book.Series
                            });
                            _context.SaveChanges();
                            seriesId = newSeries.Entity.Id;
                        }
                    }
                    #endregion

                    #region Category
                    int categoryId;
                    if (_context.Category.Any(c => c.Name == book.Category))
                    {
                        categoryId = _context.Category.First(c => c.Name == book.Category).Id;
                    }
                    else
                    {
                        var newCategory = _context.Category.Add(new Category
                        {
                            Name = book.Category
                        });
                        _context.SaveChanges();
                        categoryId = newCategory.Entity.Id;
                    }
                    #endregion

                    #region Genre
                    int genreId;
                    if (_context.Genre.Any(g => g.Name == book.Genre && g.IdCategory == categoryId))
                    {
                        genreId = _context.Genre.First(g => g.Name == book.Genre && g.IdCategory == categoryId).Id;
                    }
                    else
                    {
                        var newGenre = _context.Genre.Add(new Genre
                        {
                            Name = book.Genre,
                            IdCategory = categoryId
                        });
                        _context.SaveChanges();
                        genreId = newGenre.Entity.Id;
                    }
                    #endregion

                    var newBook = _context.Book.Add(new Book
                    {
                        Isbn = book.Isbn,
                        Name = book.Name,
                        Description = book.Description,
                        ReleaseYear = book.ReleaseYear,

                        IdAuthor = authorId,
                        IdPublisher = publisherId,
                        IdSeries = seriesId,
                        IdCategory = categoryId,
                        IdGenre = genreId
                    });

                    _context.SaveChanges();

                    _context.Availability.Add(new Availability
                    {
                        TotalCount = count,
                        FreeCount = count,
                        OnHandsCount = 0,
                        ExpiredCount = 0,
                        IdBook = newBook.Entity.Id
                    });

                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    ModelState.TryAddModelError("db_error", "Can not add book.");
                    return BadRequest(ModelState);
                }
            }

            return new OkResult();
        }

        // DELETE: api/Books
        [HttpDelete]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteBook(int id)
        {
            if (!BookExists(id))
            {
                return NotFound();
            }

            var libBook = _context.Book.Find(id);

            var oldAuthor = libBook.IdAuthorNavigation;
            var oldPublisher = libBook.IdPublisherNavigation;
            var oldSeries = libBook.IdSeriesNavigation;
            var oldCategory = libBook.IdCategoryNavigation;
            var oldGenre = libBook.IdGenreNavigation;

            _context.Book.Remove(libBook);

            _context.SaveChanges();

            if (!oldAuthor.Books.Any())
            {
                _context.Author.Remove(oldAuthor);
                _context.SaveChanges();
            }

            if (!oldPublisher.Books.Any())
            {
                _context.Publisher.Remove(oldPublisher);
                _context.SaveChanges();
            }

            if (oldSeries != null && !oldSeries.Books.Any())
            {
                _context.Series.Remove(oldSeries);
                _context.SaveChanges();
            }

            if (!oldGenre.Books.Any())
            {
                _context.Genre.Remove(oldGenre);
                _context.SaveChanges();

                if (!oldCategory.Books.Any())
                {
                    _context.Category.Remove(oldCategory);
                    _context.SaveChanges();
                }
            }

            return new OkResult();
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}