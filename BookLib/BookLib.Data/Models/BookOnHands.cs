using System;

namespace BookLib.Models.DBModels
{
    public partial class BookOnHands
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string UserId { get; set; }
        public DateTime TakingDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool Expired { get; set; }

        public virtual Book BookNavigation { get; set; }
        public virtual ApplicationUser UserNavigation { get; set; }
    }
}
