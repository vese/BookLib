using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace BookLib.Models.DBModels
{
    public partial class SheduledBook
    {
        public int Id { get; set; }
        public int IdBook { get; set; }
        public string IdUser { get; set; }

        public virtual Book IdBookNavigation { get; set; }
        public virtual ApplicationUser IdUserNavigation { get; set; }
    }
}
