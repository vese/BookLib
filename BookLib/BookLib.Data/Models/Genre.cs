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
        public int CategoryId { get; set; }

        public virtual Category CategoryNavigation { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
