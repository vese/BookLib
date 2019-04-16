namespace BookLib.Models.DBModels
{
    public partial class QueueOnBook
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string UserId { get; set; }
        public int Position { get; set; }

        public virtual Availability BookNavigation { get; set; }
        public virtual ApplicationUser UserNavigation { get; set; }
    }
}
