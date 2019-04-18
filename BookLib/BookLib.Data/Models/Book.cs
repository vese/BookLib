using System.Collections.Generic;

namespace BookLib.Models.DBModels
{
    public partial class Book
    {
        public Book()
        {
            Comments = new HashSet<Comment>();
            ReadBooks = new HashSet<ReadBook>();
            ScheduledBooks = new HashSet<FavouriteBook>();
            QueuesOnBook = new HashSet<QueueOnBook>();
            BooksOnHands = new HashSet<BookOnHands>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ReleaseYear { get; set; }
        public int AuthorId { get; set; }
        public int PublisherId { get; set; }
        public int? SeriesId { get; set; }
        public int CategoryId { get; set; }
        public int GenreId { get; set; }
        public string CoverImage { get; set; }

        public virtual Author AuthorNavigation { get; set; }
        public virtual Category CategoryNavigation { get; set; }
        public virtual Genre GenreNavigation { get; set; }
        public virtual Publisher PublisherNavigation { get; set; }
        public virtual Series SeriesNavigation { get; set; }
        public virtual Availability AvailabilityNavigation { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ReadBook> ReadBooks { get; set; }
        public virtual ICollection<FavouriteBook> ScheduledBooks { get; set; }
        public virtual ICollection<QueueOnBook> QueuesOnBook { get; set; }
        public virtual ICollection<BookOnHands> BooksOnHands { get; set; }
    }
}
