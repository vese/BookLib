using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace BookLib.Models.DBModels
{
    public partial class BookOnHands
    {
        public int Id { get; set; }
        public int IdBook { get; set; }
        public string IdUser { get; set; }
        public DateTime TakingDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool Expired { get; set; }

        public Availability IdBookNavigation { get; set; }
        public ApplicationUser IdUserNavigation { get; set; }
    }
}
