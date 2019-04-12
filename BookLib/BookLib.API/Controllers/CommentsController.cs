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
    public class CommentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetComment(int bookId, int beginNumber, int number, string order)
        {
            bool desc = (order ?? default(string)) == "desc";
            var comments = _context.Comment.Where(c => c.IdBook == bookId).OrderBy(b => desc ? null : b.Mark)
                .OrderByDescending(b => desc ? b.Mark : null).Skip(beginNumber).Take(number).Select(c => new
                {
                    text = c.Text,
                    mark = c.Mark,
                    name = c.IdUserNavigation.UserName
                }).ToList();

            return new OkObjectResult(JsonConvert.SerializeObject(comments,
                new JsonSerializerSettings {Formatting = Formatting.Indented}));
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public IActionResult PostComment(string text, int mark, string idUser, int idBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (_context.Comment.Any(c => c.IdBook == idBook && c.IdUser == idUser))
            {
                ModelState.TryAddModelError("Comment", "Отзыв на эту книгу от этого пользователя уже существует");
                return BadRequest(ModelState);
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var newComment = _context.Comment.Add(new Comment()
                    {
                        Text = text,
                        Mark = mark,
                        IdBook = idBook,
                        IdUser = idUser
                    });

                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    ModelState.TryAddModelError("Comment", "Ошибка при добавлении отзыва");
                    return BadRequest(ModelState);
                }
            }

            return new OkResult();
        }
    }
}
