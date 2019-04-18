namespace BookLib.Models.DBModels
{
    public partial class ScheduledBook
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string UserId { get; set; }

        public virtual Book BookNavigation { get; set; }
        public virtual ApplicationUser UserNavigation { get; set; }
    }
}