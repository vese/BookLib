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

        public Book IdBookNavigation { get; set; }
        public ApplicationUser IdUserNavigation { get; set; }
    }
}
