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
        public IActionResult GetComment(int id)
        {
            if (!CommentExist(id))
            {
                ModelState.TryAddModelError("not_found", $"Comment with id = {id} does not exist.");
                return BadRequest(ModelState);
            }

            var libComment = _context.Comment.Find(id);

            var comment = new
            {
                idBook = libComment.IdBookNavigation,
                text = libComment.Text,
                mark = libComment.Mark != null ? Convert.ToString(libComment.Mark) : "Mark does not exist",
                idUser = libComment.IdUserNavigation
            };

            return new OkObjectResult(JsonConvert.SerializeObject(comment, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        private bool CommentExist(int id)
        {
            return _context.Comment.Any(e => e.Id == id);
        }
    }
}
