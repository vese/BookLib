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

        // POST: api/List/Sheduled
        [HttpPost]
        [Route("sheduled")]
        [Authorize(Roles = "user")]
        public IActionResult AddBookInSheduled(string username, int bookId)
        {
            if (_context.SheduledBook.Any(s => s.BookId == bookId && s.UserNavigation.UserName == username))
            {
                ModelState.TryAddModelError("SheduledBook", "Книга уже находится в запланированных");
                return BadRequest(ModelState);
            }
            try
            {
                _context.SheduledBook.Add(new SheduledBook
                {
                    BookId = bookId,
                    UserId = _context.Users.First(u => u.UserName == username).Id
                });
                _context.SaveChanges();
            }
            catch (Exception)
            {
                ModelState.TryAddModelError("SheduledBook", "Ошибка при попытке сохранить книгу в рекомендации");
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

        // GET: api/List/Sheduled
        [HttpGet]
        [Route("sheduled")]
        [Authorize(Roles = "user")]
        public IActionResult GetSheduledBooks(string username)
        {
            var sheduledBooks = _context.SheduledBook.Where(s => s.UserNavigation.UserName == username).Select(s => new
            {
                id = s.BookId,
                name = s.BookNavigation.Name,
            }).ToList();

            return new OkObjectResult(JsonConvert.SerializeObject(sheduledBooks, new JsonSerializerSettings { Formatting = Formatting.Indented }));
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
                name = r.BookNavigation.Name,
            }).ToList();

            return new OkObjectResult(JsonConvert.SerializeObject(readBooks, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        // DELETE: api/List/Sheduled
        [HttpDelete]
        [Route("sheduled")]
        [Authorize(Roles = "user")]
        public IActionResult DeleteSheduledBook(string username, int bookId)
        {
            var sheduledBook = _context.SheduledBook.FirstOrDefault(s => s.UserNavigation.UserName == username && s.BookId == bookId);
            _context.SheduledBook.Remove(sheduledBook);
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

        // GET: api/List/InSheduled
        [HttpGet]
        [Route("insheduled")]
        [Authorize(Roles = "user")]
        public IActionResult BookInSheduled(string username, int bookId)
        {
            var res = _context.SheduledBook.Any(s => s.UserNavigation.UserName == username && s.BookId == bookId);

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