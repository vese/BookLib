using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace BookLib.Models.DBModels
{
    public partial class Comment
    {
        public int Id { get; set; }
        public int IdBook { get; set; }
        public string IdUser { get; set; }
        public string Text { get; set; }
        public int? Mark { get; set; }

        public virtual Book IdBookNavigation { get; set; }
        public virtual ApplicationUser IdUserNavigation { get; set; }
    }
}
