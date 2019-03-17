using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace BookLib.Models.DBModels
{
    public partial class QueueOnBook
    {
        public int Id { get; set; }
        public int IdBook { get; set; }
        public string IdUser { get; set; }
        public int Position { get; set; }

        public Availability IdBookNavigation { get; set; }
        public ApplicationUser IdUserNavigation { get; set; }
    }
}
