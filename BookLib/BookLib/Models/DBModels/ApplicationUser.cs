using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLib.Models.DBModels
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base()
        {
            ReadBooks = new HashSet<ReadBook>();
            SheduledBooks = new HashSet<SheduledBook>();
            Comments = new HashSet<Comment>();
            QueuesOnBook = new HashSet<QueueOnBook>();
            BooksOnHands = new HashSet<BookOnHands>();
        }

        public ICollection<ReadBook> ReadBooks { get; set; }
        public ICollection<SheduledBook> SheduledBooks { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<QueueOnBook> QueuesOnBook { get; set; }
        public ICollection<BookOnHands> BooksOnHands { get; set; }
    }
}
