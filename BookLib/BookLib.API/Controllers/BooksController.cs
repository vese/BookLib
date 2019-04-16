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

        #region Filter
        // GET: api/Books/FilterParams
        [HttpGet]
        [Route("filterparams")]
        public IActionResult GetFilterParams()
        {
            var response = new
            {
                releaseYears = _context.Book.Select(b => b.ReleaseYear).Distinct().ToList(),
                authors = _context.Author.Select(a => new
                {
                    id = a.Id,
                    name = a.Name
                }).ToList(),
                publishers = _context.Publisher.Select(a => new
                {
                    id = a.Id,
                    name = a.Name
                }).ToList(),
                series = _context.Series.Select(a => new
                {
                    id = a.Id,
                    name = a.Name
                }).ToList(),
                categories = _context.Category.Select(a => new
                {
                    category = new
                    {
                        id = a.Id,
                        name = a.Name
                    },
                    genres = a.Genres.Select(g => new
                    {
                        id = g.Id,
                        name = g.Name
                    })
                }).ToList(),
                sortProperties = new[]
                {
                    new
                    {
                        value = nameof(SortProperty.Author),
                        name = "автору"
                    },
                    new
                    {
                        value = nameof(SortProperty.Mark),
                        name = "средней оценке"
                    },
                    new
                    {
                        value = nameof(SortProperty.Name),
                        name = "названию"
                    },
                    new
                    {
                        value = nameof(SortProperty.ReleaseYear),
                        name = "дате издания"
                    }
                }.ToList()
            };

            return new OkObjectResult(JsonConvert.SerializeObject(response, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            }));
        }

        // GET: api/Books/Filter
        [HttpGet]
        [Route("filter")]
        public IActionResult Filter(string inName, int? releaseYear, int? authorId, int? publisherId, int? seriesId, int? categoryId, int? genreId, bool? hasFree, SortProperty sort, string order)
        {
            var books = _context.Book.Where(b =>
            (string.IsNullOrWhiteSpace(inName) || b.Name.Contains(inName, StringComparison.CurrentCultureIgnoreCase)) &&
            (releaseYear == null || b.ReleaseYear == releaseYear) &&
            (hasFree == null || !(bool)hasFree || b.Availability.FreeCount > 0) &&
            (authorId == null || b.AuthorId == authorId) &&
            (publisherId == null || b.PublisherId == publisherId) &&
            (seriesId == null || b.SeriesId != null && b.SeriesId == seriesId) &&
            (categoryId == null || b.CategoryId == categoryId) &&
            (genreId == null || b.GenreId == genreId)
            ).Select(b => new
            {
                id = b.Id,
                name = b.Name,
                croppedDescription = b.Description.Substring(0, 100),
                releaseYear = b.ReleaseYear,
                author = b.AuthorNavigation.Name,
                publisher = b.PublisherNavigation.Name,
                category = b.CategoryNavigation.Name,
                genre = b.GenreNavigation.Name,
                series = b.SeriesNavigation == null ? null : b.SeriesNavigation.Name,
                commentsCount = b.Comments.Count(),
                averageMark = b.Comments.Any() ? (int)b.Comments.Sum(c => c.Mark) / b.Comments.Count() : 0,
                freeCount = b.Availability.FreeCount
            }).ToList();

            bool desc = (order ?? default(string)) == "desc";

            switch (sort)
            {
                case SortProperty.Name:
                    books = books.OrderBy(b => desc ? default(string) : b.name).OrderByDescending(b => desc ? b.name : default(string)).ToList();
                    break;
                case SortProperty.Author:
                    books = books.OrderBy(b => desc ? default(string) : b.author).OrderByDescending(b => desc ? b.author : default(string)).ToList();
                    break;
                case SortProperty.ReleaseYear:
                    books = books.OrderBy(b => desc ? default(int) : b.releaseYear).OrderByDescending(b => desc ? b.releaseYear : default(int)).ToList();
                    break;
                case SortProperty.Mark:
                    books = books.OrderBy(b => desc ? default(int) : b.averageMark).OrderByDescending(b => desc ? b.averageMark : default(int)).ToList();
                    break;
            }

            return new OkObjectResult(JsonConvert.SerializeObject(books, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }
        #endregion

        #region Book
        // GET: api/Books
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult GetBook(int id)
        {
            if (!BookExists(id))
            {
                ModelState.TryAddModelError("Model", $"Book with id = {id} does not exist.");
                return BadRequest(ModelState);
            }

            var libBook = _context.Book.Find(id);

            var book = new
            {
                name = libBook.Name,
                description = libBook.Description,
                releaseYear = libBook.ReleaseYear,
                author = libBook.AuthorNavigation.Name,
                publisher = libBook.PublisherNavigation.Name,
                category = libBook.CategoryNavigation.Name,
                genre = libBook.GenreNavigation.Name,
                series = libBook.SeriesNavigation == null ? null : libBook.SeriesNavigation.Name,
                commentsCount = libBook.Comments.Count(),
                averageMark = libBook.Comments.Any() ? (int)libBook.Comments.Sum(c => c.Mark) / libBook.Comments.Count() : 0,
                freeCount = libBook.Availability.FreeCount
            };

            return new OkObjectResult(JsonConvert.SerializeObject(book, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        // PUT: api/Books
        [HttpPut]
        [Authorize(Roles = "admin")]
        public IActionResult PutBook(int id, [FromBody] ViewBook book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!BookExists(id))
            {
                ModelState.TryAddModelError("Book", $"Book with id = {id} does not exist.");
                return BadRequest(ModelState);
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

                    if (libBook.Description != book.Description)
                    {
                        libBook.Description = book.Description;
                    }

                    if (libBook.ReleaseYear != book.ReleaseYear)
                    {
                        libBook.ReleaseYear = book.ReleaseYear;
                    }

                    #region Author
                    var oldAuthor = libBook.AuthorNavigation;

                    if (book.AuthorId == null)
                    {
                        var newAuthor = _context.Author.Add(new Author
                        {
                            Name = book.Author
                        });

                        _context.SaveChanges();

                        libBook.AuthorId = newAuthor.Entity.Id;
                    }
                    else if (_context.Author.Any(a => a.Id == book.AuthorId))
                    {
                        if (libBook.AuthorId != book.AuthorId)
                        {
                            libBook.AuthorId = (int)book.AuthorId;
                        }
                    }
                    else
                    {
                        ModelState.TryAddModelError("Book", $"Author with id = {book.AuthorId} does not exist.");
                        return BadRequest(ModelState);
                    }

                    if (!oldAuthor.Books.Any())
                    {
                        _context.Author.Remove(oldAuthor);
                    }
                    #endregion

                    #region Publisher
                    var oldPublisher = libBook.PublisherNavigation;

                    if (book.PublisherId == null)
                    {
                        var newPublisher = _context.Publisher.Add(new Publisher
                        {
                            Name = book.Publisher
                        });

                        _context.SaveChanges();

                        libBook.PublisherId = newPublisher.Entity.Id;
                    }
                    else if (_context.Publisher.Any(p => p.Id == book.PublisherId))
                    {
                        if (libBook.PublisherId != book.PublisherId)
                        {
                            libBook.PublisherId = (int)book.PublisherId;
                        }
                    }
                    else
                    {
                        ModelState.TryAddModelError("Book", $"Publisher with id = {book.PublisherId} does not exist.");
                        return BadRequest(ModelState);
                    }

                    if (!oldPublisher.Books.Any())
                    {
                        _context.Publisher.Remove(oldPublisher);
                    }
                    #endregion

                    #region Series
                    var oldSeries = libBook.SeriesNavigation;

                    if (book.HasSeries)
                    {
                        if (book.SeriesId == null)
                        {
                            var newSeries = _context.Series.Add(new Series
                            {
                                Name = book.Series
                            });

                            _context.SaveChanges();

                            libBook.SeriesId = newSeries.Entity.Id;
                        }
                        else if (_context.Series.Any(s => s.Id == book.SeriesId))
                        {
                            if (libBook.SeriesId != book.SeriesId)
                            {
                                libBook.SeriesId = (int)book.SeriesId;
                            }
                        }
                        else
                        {
                            ModelState.TryAddModelError("Book", $"Series with id = {book.SeriesId} does not exist.");
                            return BadRequest(ModelState);
                        }
                    }
                    else if (libBook.SeriesId != null)
                    {
                        libBook.SeriesId = null;
                    }

                    if (oldSeries != null && !oldSeries.Books.Any())
                    {
                        _context.Series.Remove(oldSeries);
                    }
                    #endregion

                    #region Category & Genre
                    var oldCategory = libBook.CategoryNavigation;

                    var oldGenre = libBook.GenreNavigation;

                    if (book.CategoryId == null)
                    {
                        //добавить категорию и жанр
                        var newCategory = _context.Category.Add(new Category
                        {
                            Name = book.Category
                        });

                        _context.SaveChanges();

                        libBook.CategoryId = newCategory.Entity.Id;

                        var newGenre = _context.Genre.Add(new Genre
                        {
                            Name = book.Genre,
                            CategoryId = newCategory.Entity.Id
                        });

                        _context.SaveChanges();

                        libBook.GenreId = newGenre.Entity.Id;
                    }
                    else if (_context.Category.Any(c => c.Id == book.CategoryId))
                    {
                        if (libBook.CategoryId != book.CategoryId)
                        {
                            //переместить
                            libBook.CategoryId = (int)book.CategoryId;
                        }

                        if (book.GenreId == null)
                        {
                            //добавить жанр
                            var newGenre = _context.Genre.Add(new Genre
                            {
                                Name = book.Genre,
                                CategoryId = (int)book.CategoryId
                            });

                            _context.SaveChanges();

                            libBook.GenreId = newGenre.Entity.Id;
                        }
                        else if (_context.Genre.Any(g => g.CategoryId == book.CategoryId && g.Id == book.GenreId))
                        {
                            //переместить жанр
                            libBook.GenreId = (int)book.GenreId;
                        }
                        else
                        {
                            ModelState.TryAddModelError("Book", $"Genre with id = {book.GenreId} does not exist or is in another category.");
                            return BadRequest(ModelState);
                        }
                    }
                    else
                    {
                        ModelState.TryAddModelError("Book", $"Category with id = {book.CategoryId} does not exist.");
                        return BadRequest(ModelState);
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
                    ModelState.TryAddModelError("Book", "Can not perform database action.");
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

            if (_context.Book.Any(b => book.AuthorId != null && b.Name == book.Name && b.AuthorId == book.AuthorId))
            {
                ModelState.TryAddModelError("Book", "Такая книга уже существувет");
                return BadRequest(ModelState);
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    #region Author
                    if (book.AuthorId == null)
                    {
                        if (_context.Author.Any(a => a.Name == book.Author))
                        {
                            book.AuthorId = _context.Author.First(a => a.Name == book.Author).Id;
                        }
                        else
                        {
                            var newAuthor = _context.Author.Add(new Author
                            {
                                Name = book.Author
                            });

                            _context.SaveChanges();

                            book.AuthorId = newAuthor.Entity.Id;
                        }
                    }
                    else if (!_context.Author.Any(a => a.Id == book.AuthorId))
                    {
                        ModelState.TryAddModelError("Book", $"Author with id = {book.AuthorId} does not exist.");
                        return BadRequest(ModelState);
                    }
                    #endregion

                    #region Publisher
                    if (book.PublisherId == null)
                    {
                        if (_context.Publisher.Any(p => p.Name == book.Publisher))
                        {
                            book.PublisherId = _context.Publisher.First(p => p.Name == book.Publisher).Id;
                        }
                        else
                        {
                            var newPublisher = _context.Publisher.Add(new Publisher
                            {
                                Name = book.Publisher
                            });

                            _context.SaveChanges();

                            book.PublisherId = newPublisher.Entity.Id;
                        }
                    }
                    else if (!_context.Publisher.Any(p => p.Id == book.PublisherId))
                    {
                        ModelState.TryAddModelError("Book", $"Publisher with id = {book.PublisherId} does not exist.");
                        return BadRequest(ModelState);
                    }
                    #endregion

                    #region Series
                    if (book.HasSeries)
                    {
                        if (book.SeriesId == null)
                        {
                            var newSeries = _context.Series.Add(new Series
                            {
                                Name = book.Series
                            });

                            _context.SaveChanges();

                            book.SeriesId = newSeries.Entity.Id;
                        }
                        else if (!_context.Series.Any(s => s.Id == book.SeriesId))
                        {
                            ModelState.TryAddModelError("Book", $"Series with id = {book.SeriesId} does not exist.");
                            return BadRequest(ModelState);
                        }
                    }
                    else
                    {
                        book.SeriesId = null;
                    }
                    #endregion

                    #region Category
                    if (book.CategoryId == null)
                    {
                        if (_context.Category.Any(c => c.Name == book.Category))
                        {
                            book.CategoryId = _context.Category.First(c => c.Name == book.Category).Id;
                        }
                        else
                        {
                            var newCategory = _context.Category.Add(new Category
                            {
                                Name = book.Category
                            });

                            _context.SaveChanges();

                            book.CategoryId = newCategory.Entity.Id;
                        }
                    }
                    else if (!_context.Category.Any(c => c.Id == book.CategoryId))
                    {
                        ModelState.TryAddModelError("Book", $"Category with id = {book.CategoryId} does not exist.");
                        return BadRequest(ModelState);
                    }
                    #endregion

                    #region Genre
                    if (book.GenreId == null)
                    {
                        if (_context.Genre.Any(g => g.Name == book.Genre && g.CategoryId == book.CategoryId))
                        {
                            book.GenreId = _context.Genre.First(g => g.Name == book.Genre && g.CategoryId == book.CategoryId).Id;
                        }
                        else
                        {
                            var newGenre = _context.Genre.Add(new Genre
                            {
                                Name = book.Genre,
                                CategoryId = (int)book.CategoryId
                            });

                            _context.SaveChanges();

                            book.GenreId = newGenre.Entity.Id;
                        }
                    }
                    else if (!_context.Genre.Any(g => g.Id == book.GenreId && g.CategoryId == book.CategoryId))
                    {
                        ModelState.TryAddModelError("Book", $"Genre with id = {book.GenreId} does not exist.");
                        return BadRequest(ModelState);
                    }
                    #endregion

                    var newBook = _context.Book.Add(new Book
                    {
                        Name = book.Name,
                        Description = book.Description,
                        ReleaseYear = book.ReleaseYear,

                        AuthorId = (int)book.AuthorId,
                        PublisherId = (int)book.PublisherId,
                        SeriesId = book.SeriesId,
                        CategoryId = (int)book.CategoryId,
                        GenreId = (int)book.GenreId
                    });

                    _context.SaveChanges();

                    _context.Availability.Add(new Availability
                    {
                        TotalCount = count,
                        FreeCount = count,
                        OnHandsCount = 0,
                        NotReturnedCount = 0,
                        BookId = newBook.Entity.Id
                    });

                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    ModelState.TryAddModelError("Book", "Ошибка при добавлении книги");
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

            var oldAuthor = libBook.AuthorNavigation;
            var oldPublisher = libBook.PublisherNavigation;
            var oldSeries = libBook.SeriesNavigation;
            var oldCategory = libBook.CategoryNavigation;
            var oldGenre = libBook.GenreNavigation;

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
        #endregion

        #region Exists
        // GET: api/Books/AuthorExists
        [HttpGet]
        [Route("authorexists")]
        [Authorize(Roles = "admin")]
        public IActionResult AuthorExists(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                ModelState.TryAddModelError(nameof(name), $"The {nameof(name)} field is required.");
                return BadRequest(ModelState);
            }

            return new OkObjectResult(JsonConvert.SerializeObject(new
            {
                AutorExists = _context.Author.Any(a => a.Name == name)
            }, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        // GET: api/Books/PublisherExists
        [HttpGet]
        [Route("publisherexists")]
        [Authorize(Roles = "admin")]
        public IActionResult PublisherExists(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                ModelState.TryAddModelError(nameof(name), $"The {nameof(name)} field is required.");
                return BadRequest(ModelState);
            }

            return new OkObjectResult(JsonConvert.SerializeObject(new
            {
                PublisherExists = _context.Publisher.Any(a => a.Name == name)
            }, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        // GET: api/Books/SeriesExists
        [HttpGet]
        [Route("seriesexists")]
        [Authorize(Roles = "admin")]
        public IActionResult SeriesExists(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                ModelState.TryAddModelError(nameof(name), $"The {nameof(name)} field is required.");
                return BadRequest(ModelState);
            }

            return new OkObjectResult(JsonConvert.SerializeObject(new
            {
                SeriesExists = _context.Series.Any(a => a.Name == name)
            }, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        // GET: api/Books/CategoryExists
        [HttpGet]
        [Route("categoryexists")]
        [Authorize(Roles = "admin")]
        public IActionResult CategoryExists(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                ModelState.TryAddModelError(nameof(name), $"The {nameof(name)} field is required.");
                return BadRequest(ModelState);
            }

            return new OkObjectResult(JsonConvert.SerializeObject(new
            {
                CategoryExists = _context.Category.Any(a => a.Name == name)
            }, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        // GET: api/Books/GenreExists
        [HttpGet]
        [Route("genreexists")]
        [Authorize(Roles = "admin")]
        public IActionResult GenreExists(int categoryId, string name)
        {
            if (!_context.Category.Any(c => c.Id == categoryId))
            {
                ModelState.TryAddModelError("not_found", $"Category with id = {categoryId} does not exist.");
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                ModelState.TryAddModelError(nameof(name), $"The {nameof(name)} field is required.");
                return BadRequest(ModelState);
            }

            return new OkObjectResult(JsonConvert.SerializeObject(new
            {
                GenreExists = _context.Genre.Any(a => a.CategoryId == categoryId && a.Name == name)
            }, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
        #endregion
    }
}