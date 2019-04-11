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
        public IActionResult GetComment(int bookId, int beginNumber, int number)
        {
            var comments = (from c in _context.Comment
                where c.IdBook == bookId
                select c).Skip(beginNumber).Take(number).Select(c => new
            {
                c.Text,
                c.Mark,
                c.IdUserNavigation.UserName
            });

            return new OkObjectResult(JsonConvert.SerializeObject(comments,
                new JsonSerializerSettings {Formatting = Formatting.Indented}));
        }
    }
}
