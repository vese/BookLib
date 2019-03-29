using System;
using System.Collections.Generic;
using System.Text;

namespace BookLib.Data.ViewModels
{
    public class ViewBook
    {
        public enum SortProperty
        {
            Name,
            ReleaseYear,
            Author,
            Mark
        }
        public string Isbn { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ReleaseYear { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Series { get; set; }
        public string Category { get; set; }
        public string Genre { get; set; }
        public int Mark { get; set; }
        public int FreeCount { get; set; }
    }
}
