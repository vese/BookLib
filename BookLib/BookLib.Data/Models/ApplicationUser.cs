using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BookLib.Models.DBModels
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base()
        {
            ReadBooks = new HashSet<ReadBook>();
            ScheduledBooks = new HashSet<ScheduledBook>();
            Comments = new HashSet<Comment>();
            QueuesOnBook = new HashSet<QueueOnBook>();
            BooksOnHands = new HashSet<BookOnHands>();
        }

        public int OnHands { get; set; }
        public int Returned { get; set; }
        public int Expired { get; set; }
        public int NotReturned { get; set; }

        public virtual ICollection<ReadBook> ReadBooks { get; set; }
        public virtual ICollection<ScheduledBook> ScheduledBooks { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<QueueOnBook> QueuesOnBook { get; set; }
        public virtual ICollection<BookOnHands> BooksOnHands { get; set; }
    }
}
