using System;
using System.Collections.Generic;

namespace BookLib.Models.DBModels
{
    public partial class Category
    {
        public Category()
        {
            Books = new HashSet<Book>();
            Genres = new HashSet<Genre>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
        public ICollection<Genre> Genres { get; set; }
    }
}
