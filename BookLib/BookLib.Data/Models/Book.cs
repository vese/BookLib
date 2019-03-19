using System;
using System.Collections.Generic;

namespace BookLib.Models.DBModels
{
    public partial class Book
    {
        public Book()
        {
            Comments = new HashSet<Comment>();
            ReadBooks = new HashSet<ReadBook>();
            SheduledBooks = new HashSet<SheduledBook>();
        }

        public int Id { get; set; }
        public string Isbn { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ReleaseYear { get; set; }
        public int IdAuthor { get; set; }
        public int IdPublisher { get; set; }
        public int? IdSeries { get; set; }
        public int IdCategory { get; set; }
        public int? IdGenre { get; set; }

        public virtual Author IdAuthorNavigation { get; set; }
        public virtual Category IdCategoryNavigation { get; set; }
        public virtual Genre IdGenreNavigation { get; set; }
        public virtual Publisher IdPublisherNavigation { get; set; }
        public virtual Series IdSeriesNavigation { get; set; }
        public virtual Availability Availability { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ReadBook> ReadBooks { get; set; }
        public virtual ICollection<SheduledBook> SheduledBooks { get; set; }
    }
}
