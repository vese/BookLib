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
    public class LibController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LibController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Lib/Users
        [HttpGet]
        [Route("users")]
        [Authorize(Roles = "admin")]
        public IActionResult GetUsers()
        {
            var users = _context.Users.Select(u => new
            {
                name = u.UserName,
                onHands = u.OnHands,
                returned = u.Returned,
                expired = u.Expired
            }).ToList();

            return new OkObjectResult(JsonConvert.SerializeObject(users, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        // GET: api/Lib/UserQueues
        [HttpGet]
        [Route("users")]
        [Authorize(Roles = "admin")]
        public IActionResult GetUserQueues(string username)
        {
            var positionsInQueues = _context.QueueOnBook.Where(q => q.UserId == username).Select(q => new
            {
                id = q.BookId,
                name = q.BookNavigation.Name,
                position = q.Position
            }).ToList();

            return new OkObjectResult(JsonConvert.SerializeObject(positionsInQueues, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult GetBooksCount(string userName)
        {
            var booksCount = _context.Availability.Select(a => new
            {
                bookId = a.BookId,
                totalCount = a.TotalCount,
                freeCount = a.FreeCount,
                onHandsCount = a.OnHandsCount,
                expiredCount = a.NotReturnedCount
            }).ToList();
            return new OkObjectResult(JsonConvert.SerializeObject(booksCount, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult TakeBook(int bookId, string userName)
        {
            if (!_context.Book.Any(b => b.Id == bookId))
            {
                ModelState.TryAddModelError("Model", $"Book with id = {bookId} does not exist.");
                return BadRequest(ModelState);
            }

            var aviability = _context.Availability.Where(a => a.BookId == bookId).Single();
            if (aviability.FreeCount == 0)
            {
                ModelState.TryAddModelError("Availability", "Свободных книг нет");
                return BadRequest(ModelState);
            }

            var queueOnBook = _context.QueueOnBook.Where(q => q.BookId == bookId && q.UserId == userName).Single();
            if (queueOnBook.Position != 1)
            {
                ModelState.TryAddModelError("QueueOnBook", "Пользователь не первый в очереди");
                return BadRequest(ModelState);
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    aviability.FreeCount--;
                    aviability.OnHandsCount++;
                    _context.BookOnHands.Add(new BookOnHands() { BookId = bookId, UserId = userName });
                    _context.QueueOnBook.Remove(queueOnBook);
                    foreach (var queue in _context.QueueOnBook.Where(q => q.BookId == bookId))
                    {
                        queue.Position--;
                    }
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    ModelState.TryAddModelError("Book", "Ошибка при выдаче книги");
                    return BadRequest(ModelState);
                }
            }

            return new OkResult();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult ReturnBook(int bookId, string userName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_context.BookOnHands.Any(b => b.BookId == bookId && b.UserId == userName))
            {
                ModelState.TryAddModelError("BookOnHands", "Такой книги нет у данного человека");
                return BadRequest(ModelState);
            }

            var bookOnHands = _context.BookOnHands.Where(b => b.BookId == bookId && b.UserId == userName).Single();
            var aviability = _context.Availability.Where(a => a.BookId == bookId).Single();
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.BookOnHands.Remove(bookOnHands);
                    aviability.FreeCount++;
                    aviability.OnHandsCount--;
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    ModelState.TryAddModelError("BookOnHand", "Ошибка при возврате книги");
                    return BadRequest(ModelState);
                }
            }

            return new OkResult();
        }
    }
}