using BookLib.Data;
using BookLib.Models.DBModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace BookLib.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ListController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/List/Favourite
        [HttpPost]
        [Route("favourite")]
        [Authorize(Roles = "user")]
        public IActionResult AddBookInFavourite(string username, int bookId)
        {
            if (_context.FavouriteBook.Any(s => s.BookId == bookId && s.UserNavigation.UserName == username))
            {
                ModelState.TryAddModelError("ScheduledBook", "Книга уже находится в очереди");
                return BadRequest(ModelState);
            }
            try
            {
                _context.FavouriteBook.Add(new FavouriteBook
                {
                    BookId = bookId,
                    UserId = _context.Users.First(u => u.UserName == username).Id
                });
                _context.SaveChanges();
            }
            catch (Exception)
            {
                ModelState.TryAddModelError("ScheduledBook", "Ошибка при попытке сохранить книгу в рекомендации");
                return BadRequest(ModelState);
            }

            return new OkResult();
        }

        // POST: api/List/Read
        [HttpPost]
        [Route("read")]
        [Authorize(Roles = "user")]
        public IActionResult AddBookInRead(string username, int bookId)
        {
            if (_context.ReadBook.Any(r => r.BookId == bookId && r.UserNavigation.UserName == username))
            {
                ModelState.TryAddModelError("ReadBook", "Книга уже находится в списке прочитанных");
                return BadRequest(ModelState);
            }
            try
            {
                _context.ReadBook.Add(new ReadBook
                {
                    BookId = bookId,
                    UserId = _context.Users.First(u => u.UserName == username).Id
                });
                _context.SaveChanges();
            }
            catch (Exception)
            {
                ModelState.TryAddModelError("ReadBook", "Ошибка при попытке сохранить книгу в прочитанные");
                return BadRequest(ModelState);
            }

            return new OkResult();
        }

        // GET: api/List/Favourite
        [HttpGet]
        [Route("favourite")]
        [Authorize(Roles = "user")]
        public IActionResult GetScheduledBooks(string username)
        {
            var favouriteBooks = _context.FavouriteBook.Where(s => s.UserNavigation.UserName == username).Select(s => new
            {
                id = s.BookId,
                author = s.BookNavigation.AuthorNavigation.Name,
                name = s.BookNavigation.Name,
            }).ToList();

            return new OkObjectResult(JsonConvert.SerializeObject(favouriteBooks, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        // GET: api/List/Read
        [HttpGet]
        [Route("read")]
        [Authorize(Roles = "user")]
        public IActionResult GetReadBooks(string username)
        {
            var readBooks = _context.ReadBook.Where(r => r.UserNavigation.UserName == username).Select(r => new
            {
                id = r.BookId,
                author = r.BookNavigation.AuthorNavigation.Name,
                name = r.BookNavigation.Name,
            }).ToList();

            return new OkObjectResult(JsonConvert.SerializeObject(readBooks, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        // DELETE: api/List/Favourite
        [HttpDelete]
        [Route("favourite")]
        [Authorize(Roles = "user")]
        public IActionResult DeleteScheduledBook(string username, int bookId)
        {
            var favouriteBook = _context.FavouriteBook.FirstOrDefault(s => s.UserNavigation.UserName == username && s.BookId == bookId);
            _context.FavouriteBook.Remove(favouriteBook);
            _context.SaveChanges();
            return new OkResult();
        }

        // DELETE: api/List/Read
        [HttpDelete]
        [Route("read")]
        [Authorize(Roles = "user")]
        public IActionResult DeleteReadBook(string username, int bookId)
        {
            var readBook = _context.ReadBook.FirstOrDefault(s => s.UserNavigation.UserName == username && s.BookId == bookId);
            _context.ReadBook.Remove(readBook);
            _context.SaveChanges();
            return new OkResult();
        }

        // GET: api/List/InFavourite
        [HttpGet]
        [Route("infavourite")]
        [Authorize(Roles = "user")]
        public IActionResult BookInFavourite(string username, int bookId)
        {
            var res = _context.FavouriteBook.Any(s => s.UserNavigation.UserName == username && s.BookId == bookId);

            return new OkObjectResult(JsonConvert.SerializeObject(res, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        // GET: api/List/InRead
        [HttpGet]
        [Route("inread")]
        [Authorize(Roles = "user")]
        public IActionResult BookInRead(string username, int bookId)
        {
            var res = _context.ReadBook.Any(s => s.UserNavigation.UserName == username && s.BookId == bookId);

            return new OkObjectResult(JsonConvert.SerializeObject(res, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }
    }
}