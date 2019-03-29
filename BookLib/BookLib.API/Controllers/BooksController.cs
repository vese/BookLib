using BookLib.Data;
using BookLib.Data.ViewModels;
using BookLib.Models.DBModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLib.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("filter")]
        [HttpGet]
        public IActionResult Filter(string inName, int? releaseYear, string author, string publisher, string series, string category, string genre, bool? hasFree, ViewBook.SortProperty sort, string order)
        {
            int? authorId = _context.Author.FirstOrDefault(el => el.Name == author)?.Id;
            int? publisherId = _context.Publisher.FirstOrDefault(el => el.Name == publisher)?.Id;
            int? seriesId = _context.Publisher.FirstOrDefault(el => el.Name == series)?.Id;
            int? categoryId = _context.Category.FirstOrDefault(el => el.Name == category)?.Id;
            int? genreId = _context.Genre.FirstOrDefault(el => el.Name == genre)?.Id;

            List<ViewBook> books = new List<ViewBook>();

            _context.Book.Where(b =>
            (string.IsNullOrWhiteSpace(inName) || b.Name.Contains(inName)) &&
            (releaseYear == null || b.ReleaseYear == releaseYear) &&
            (hasFree == null || b.Availability.FreeCount > 0) &&
            (authorId == null || b.IdAuthor == authorId) &&
            (publisherId == null || b.IdPublisher == publisherId) &&
            (seriesId == null || b.IdSeries != null && b.IdSeries == seriesId) &&
            (categoryId == null || b.IdCategory == categoryId) &&
            (genreId == null || b.IdGenre == genreId)
            ).ToList().ForEach(b => books.Add(new ViewBook
            {
                Author = b.IdAuthorNavigation.Name,
                Category = b.IdCategoryNavigation.Name,
                Mark = b.Comments.Any() ? (int)b.Comments.Sum(c => c.Mark) / b.Comments.Count() : 0,
                Description = b.Description,
                FreeCount = b.Availability.FreeCount,
                Genre = b.IdGenreNavigation.Name,
                Isbn = b.Isbn,
                Name = b.Name,
                Publisher = b.IdPublisherNavigation.Name,
                ReleaseYear = b.ReleaseYear,
                Series = b.IdSeriesNavigation?.Name
            }));

            bool desc = (order ?? default(string)) == "desc";

            switch (sort)
            {
                case ViewBook.SortProperty.Name:
                    books = books.OrderBy(b => desc ? default(string) : b.Name).OrderByDescending(b => desc ? b.Name : default(string)).ToList();
                    break;
                case ViewBook.SortProperty.Author:
                    books = books.OrderBy(b => desc ? default(string) : b.Author).OrderByDescending(b => desc ? b.Author : default(string)).ToList();
                    break;
                case ViewBook.SortProperty.ReleaseYear:
                    books = books.OrderBy(b => desc ? default(int) : b.ReleaseYear).OrderByDescending(b => desc ? b.ReleaseYear : default(int)).ToList();
                    break;
                case ViewBook.SortProperty.Mark:
                    books = books.OrderBy(b => desc ? default(int) : b.Mark).OrderByDescending(b => desc ? b.Mark : default(int)).ToList();
                    break;
            }

            return new OkObjectResult(JsonConvert.SerializeObject(books, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }
    }
}