namespace BookLib.Models.DBModels
{
    public partial class Comment
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string UserId { get; set; }
        public string Text { get; set; }
        public int? Mark { get; set; }

        public virtual Book BookNavigation { get; set; }
        public virtual ApplicationUser UserNavigation { get; set; }
    }
}
