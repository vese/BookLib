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

        public virtual ICollection<ReadBook> ReadBooks { get; set; }
        public virtual ICollection<SheduledBook> SheduledBooks { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<QueueOnBook> QueuesOnBook { get; set; }
        public virtual ICollection<BookOnHands> BooksOnHands { get; set; }
    }
}
