using System;
using System.Collections.Generic;

namespace BookLib.Models.DBModels
{
    public partial class Availability
    {
        public Availability()
        {
            BooksOnHands = new HashSet<BookOnHands>();
            QueuesOnBook = new HashSet<QueueOnBook>();
        }

        public int Id { get; set; }
        public int IdBook { get; set; }
        public int TotalCount { get; set; }
        public int FreeCount { get; set; }
        public int OnHandsCount { get; set; }
        public int ExpiredCount { get; set; }

        public Book IdBookNavigation { get; set; }
        public ICollection<BookOnHands> BooksOnHands { get; set; }
        public ICollection<QueueOnBook> QueuesOnBook { get; set; }
    }
}
