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
    public class CommentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Comments
        [HttpGet]
        public IActionResult GetComment(int bookId, int start, int count, string order)
        {
            bool desc = (order ?? default(string)) == "desc";
            var comments = _context.Comment.Where(c => c.IdBook == bookId).OrderBy(b => desc ? null : b.Mark)
                .OrderByDescending(b => desc ? b.Mark : null).Skip(start).Take(count).Select(c => new
                {
                    text = c.Text,
                    mark = c.Mark,
                    name = c.IdUserNavigation.UserName
                }).ToList();

            return new OkObjectResult(JsonConvert.SerializeObject(comments, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        // POST: api/Comments
        [HttpPost]
        [Authorize(Roles = "user")]
        public async System.Threading.Tasks.Task<IActionResult> PostCommentAsync(string text, int mark, string username, int bookId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = (await _userManager.FindByNameAsync(username))?.Id;
            if (userId == null)
            {
                ModelState.TryAddModelError("Comment", "Пользователь не найден");
                return BadRequest(ModelState);
            }

            if (_context.Comment.Any(c => c.IdBook == bookId && c.IdUser == userId))
            {
                ModelState.TryAddModelError("Comment", "Отзыв на эту книгу от этого пользователя уже существует");
                return BadRequest(ModelState);
            }

            try
            {
                _context.Comment.Add(new Comment()
                {
                    Text = text,
                    Mark = mark,
                    IdBook = bookId,
                    IdUser = userId
                });

                _context.SaveChanges();
            }
            catch (Exception)
            {
                ModelState.TryAddModelError("Comment", "Ошибка при добавлении отзыва");
                return BadRequest(ModelState);
            }

            return new OkResult();
        }

        // GET: api/Comments/Exists
        [HttpGet]
        [Route("exists")]
        public IActionResult CommentExists(string username, int bookId)
        {
            var res = new
            {
                exists = _context.Comment.Any(c => c.IdUserNavigation.UserName == username && c.IdBook == bookId)
            };

            return new OkObjectResult(JsonConvert.SerializeObject(res, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }
    }
}
