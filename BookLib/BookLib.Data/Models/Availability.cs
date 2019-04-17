using System.Collections.Generic;

namespace BookLib.Models.DBModels
{
    public partial class Availability
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int TotalCount { get; set; }
        public int FreeCount { get; set; }
        public int OnHandsCount { get; set; }
        public int NotReturnedCount { get; set; }

        public virtual Book BookNavigation { get; set; }
    }
}
