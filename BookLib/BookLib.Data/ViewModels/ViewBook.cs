using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookLib.Data.ViewModels
{
    public enum SortProperty
    {
        Name,
        ReleaseYear,
        Author,
        Mark
    }

    public class ViewBook
    {
        [Required]
        [MinLength(1)]
        public string Name { get; set; }

        [Required]
        [MinLength(13)]
        [MaxLength(20)]
        public string Isbn { get; set; }
        
        [Required]
        [MinLength(1)]
        public string Description { get; set; }

        [Required]
        public int ReleaseYear { get; set; }

        [Required]
        [MinLength(1)]
        public string Author { get; set; }

        [Required]
        [MinLength(1)]
        public string Publisher { get; set; }
        
        [MinLength(1)]
        public string Series { get; set; }

        [Required]
        [MinLength(1)]
        public string Category { get; set; }

        [Required]
        [MinLength(1)]
        public string Genre { get; set; }
    }
}
