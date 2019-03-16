using System;
using System.Collections.Generic;

namespace BookLib.Models.DBModels
{
    public partial class Series
    {
        public Series()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int BooksCount { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
