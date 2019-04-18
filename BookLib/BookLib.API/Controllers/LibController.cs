using BookLib.Data;
using BookLib.Models.DBModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public LibController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Lib/Notifications
        [HttpGet]
        [Route("notifications")]
        [Authorize(Roles = "user")]
        public IActionResult GetNotifications(string username)
        {
            var user = _context.Users.First(u => u.UserName == username);
            var res = new
            {
                queue = user.QueuesOnBook.Where(q => q.Position <= q.BookNavigation.Availability.FreeCount).Select(q => new
                {
                    id = q.BookNavigation.Id,
                    name = q.BookNavigation.Name
                }),
                onHands = user.BooksOnHands.Where(b => b.ReturnDate == null).Select(b => new
                {
                    name = b.BookNavigation.Name,
                    days = (b.TakingDate.AddMonths(2) - DateTime.UtcNow).Days
                }).OrderBy(b => b.days)
            };

            return new OkObjectResult(JsonConvert.SerializeObject(res, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        // GET: api/Lib/Users
        [HttpGet]
        [Route("users")]
        [Authorize(Roles = "admin")]
        public IActionResult GetUsers()
        {
            var userRoleId = _context.Roles.First(r => r.Name == "user").Id;
            var users = _context.Users.Where(u => _context.UserRoles.Any(r => r.RoleId == userRoleId && r.UserId == u.Id)).Select(u => new
            {
                name = u.UserName,
                onHands = u.OnHands,
                returned = u.Returned,
                expired = u.Expired,
                notReturned = u.NotReturned
            }).ToList();

            return new OkObjectResult(JsonConvert.SerializeObject(users, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        // GET: api/Lib/UserQueues
        [HttpGet]
        [Route("userqueues")]
        [Authorize(Roles = "admin")]
        public IActionResult GetUserQueues(string username)
        {
            var positionsInQueues = _context.QueueOnBook.Where(q => q.UserNavigation.UserName == username).Select(q => new
            {
                id = q.BookId,
                name = q.BookNavigation.Name,
                position = q.Position
            }).ToList();

            return new OkObjectResult(JsonConvert.SerializeObject(positionsInQueues, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        // GET: api/Lib/Books
        [HttpGet]
        [Route("books")]
        [Authorize(Roles = "admin")]
        public IActionResult GetBooks()
        {
            var booksCount = _context.Availability.Select(a => new
            {
                id = a.BookId,
                name = a.BookNavigation.Name,
                free = a.FreeCount,
                onHands = a.OnHandsCount,
                queueLength = a.BookNavigation.QueuesOnBook.Count
            }).ToList();
            return new OkObjectResult(JsonConvert.SerializeObject(booksCount, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        // GET: api/Lib/BookGiven
        [HttpGet]
        [Route("bookgiven")]
        [Authorize]
        public IActionResult BookGiven(string username, int bookId)
        {
            var given = _context.BookOnHands.Any(b => b.BookId == bookId && b.UserNavigation.UserName == username && b.ReturnDate == null);
            return new OkObjectResult(JsonConvert.SerializeObject(given, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        // POST: api/Lib/GiveBook
        [HttpPost]
        [Route("givebook")]
        [Authorize(Roles = "admin")]
        public IActionResult GiveBook(string username, int bookId)
        {
            if (!_context.Book.Any(b => b.Id == bookId))
            {
                ModelState.TryAddModelError("Model", $"Book with id = {bookId} does not exist");
                return BadRequest(ModelState);
            }

            if (_context.BookOnHands.Any(b => b.BookId == bookId && b.UserNavigation.UserName == username && b.ReturnDate == null))
            {
                ModelState.TryAddModelError("Model", $"У пользователя уже есть такая книга");
                return BadRequest(ModelState);
            }

            var user = _context.Users.FirstOrDefault(u => u.UserName == username);
            if (user.NotReturned > 0)
            {
                ModelState.TryAddModelError("Model", "У пользователя есть невозвращенные книги");
                return BadRequest(ModelState);
            }

            var availability = _context.Availability.FirstOrDefault(a => a.BookId == bookId);
            if (availability.FreeCount == 0)
            {
                ModelState.TryAddModelError("Model", "Свободных книг нет");
                return BadRequest(ModelState);
            }

            var queue = _context.QueueOnBook.Where(q => q.BookId == bookId).ToList();


            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var userInQueue = queue.FirstOrDefault(q => q.UserNavigation.UserName == username);

                    if (queue.Count >= availability.FreeCount)
                    {
                        if (userInQueue == null)
                        {
                            ModelState.TryAddModelError("Model", "Пользователь не в очереди");
                            return BadRequest(ModelState);
                        }
                        if (userInQueue.Position > availability.FreeCount)
                        {
                            ModelState.TryAddModelError("Model", "Пользователь далеко в очереди");
                            return BadRequest(ModelState);
                        }
                    }
                    //из очереди
                    if (userInQueue != null && userInQueue.Position <= availability.FreeCount)
                    {
                        var pos = userInQueue.Position;
                        _context.QueueOnBook.Remove(userInQueue);
                        queue.Where(q => q.Position > pos).ToList().ForEach(q => q.Position--);
                    }
                    user.OnHands++;
                    availability.FreeCount--;
                    availability.OnHandsCount++;
                    _context.BookOnHands.Add(new BookOnHands()
                    {
                        BookId = bookId,
                        UserId = user.Id,
                        TakingDate = DateTime.UtcNow
                    });
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

        // POST: api/Lib/GiveBook
        [HttpPost]
        [Route("returnbook")]
        [Authorize(Roles = "admin")]
        public IActionResult ReturnBook(string username, int bookId)
        {
            if (!_context.BookOnHands.Any(b => b.BookId == bookId && b.UserNavigation.UserName == username && b.ReturnDate == null))
            {
                ModelState.TryAddModelError("BookOnHands", "Такой книги нет у данного человека");
                return BadRequest(ModelState);
            }


            var bookOnHands = _context.BookOnHands.First(b => b.BookId == bookId && b.UserNavigation.UserName == username && b.ReturnDate == null);
            var aviability = _context.Availability.First(a => a.BookId == bookId);
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    bookOnHands.ReturnDate = DateTime.UtcNow;
                    if (bookOnHands.TakingDate.AddMonths(2) < bookOnHands.ReturnDate)
                    {
                        bookOnHands.UserNavigation.Expired++;
                    }
                    bookOnHands.UserNavigation.Returned++;
                    bookOnHands.UserNavigation.OnHands--;
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

        // GET: api/Lib/Queue
        [HttpGet]
        [Route("queue")]
        [Authorize]
        public IActionResult UserInQueue(string username, int bookId)
        {
            var res = new
            {
                inQueue = _context.QueueOnBook.Any(q => q.BookId == bookId && q.UserNavigation.UserName == username),
                position = _context.QueueOnBook.FirstOrDefault(q => q.BookId == bookId && q.UserNavigation.UserName == username)?.Position
            };
            return new OkObjectResult(JsonConvert.SerializeObject(res, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        // POST: api/Lib/Queue
        [HttpPost]
        [Route("queue")]
        [Authorize]
        public IActionResult GetInQueue(string username, int bookId)
        {

            if (_context.QueueOnBook.Any(q => q.BookId == bookId && q.UserNavigation.UserName == username))
            {
                ModelState.TryAddModelError("Model", "Пользователь уже в очереди на эту книгу");
                return BadRequest(ModelState);
            }

            int maxPosition = _context.QueueOnBook.Any(q => q.BookId == bookId) ? _context.QueueOnBook.Where(q => q.BookId == bookId).Max(q => q.Position) : 0;
            try
            {
                _context.QueueOnBook.Add(new QueueOnBook()
                {
                    BookId = bookId,
                    UserId = _context.Users.First(u => u.UserName == username).Id,
                    Position = ++maxPosition
                });
                _context.SaveChanges();
            }
            catch (Exception)
            {
                ModelState.TryAddModelError("Model", "Ошибка при попытке встать в очередь на книгу");
                return BadRequest(ModelState);
            }

            return new OkObjectResult(JsonConvert.SerializeObject(maxPosition, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        // DELETE: api/Lib/Queue
        [HttpDelete]
        [Route("queue")]
        [Authorize]
        public IActionResult GetOutOfQueue(string username, int bookId)
        {
            var userInQueue = _context.QueueOnBook.FirstOrDefault(q => q.BookId == bookId && q.UserNavigation.UserName == username);
            if (userInQueue == null)
            {
                ModelState.TryAddModelError("Model", "Пользователь не в очереди");
                return BadRequest(ModelState);
            }

            try
            {
                var pos = userInQueue.Position;
                _context.QueueOnBook.Remove(userInQueue);
                _context.QueueOnBook.Where(q => q.BookId == bookId && q.Position > pos).ToList().ForEach(q => q.Position--);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                ModelState.TryAddModelError("Model", "Ошибка при выйти из очереди на книгу");
                return BadRequest(ModelState);
            }

            return new OkResult();
        }
    }
}