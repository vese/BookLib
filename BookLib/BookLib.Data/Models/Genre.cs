using System;
using System.Collections.Generic;

namespace BookLib.Models.DBModels
{
    public partial class Genre
    {
        public Genre()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int IdCategory { get; set; }

        public Category IdCategoryNavigation { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
