using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

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
        public string Name { get; set; }

        public string Isbn { get; set; }

        public string Description { get; set; }
        
        public int ReleaseYear { get; set; }

        public int? AuthorId { get; set; }

        public string Author { get; set; }
        
        public int? PublisherId { get; set; }
        
        public string Publisher { get; set; }
        
        public bool HasSeries { get; set; }

        public int? SeriesId { get; set; }
        
        public string Series { get; set; }

        public int? CategoryId { get; set; }

        public string Category { get; set; }

        public int? GenreId { get; set; }

        public string Genre { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(Name))
            {
                errors.Add(new ValidationResult("Введите имя!", new List<string>() { nameof(Name) }));
            }

            if (string.IsNullOrWhiteSpace(Isbn))
            {
                errors.Add(new ValidationResult("Введите ISBN!", new List<string>() { nameof(Isbn) }));
            }

            if (Isbn.Length != 17 || !new Regex(@"\d+-\d+-\d+-\d+-\d+").IsMatch(Isbn))
            {
                errors.Add(new ValidationResult("Неверный ISBN!", new List<string>() { nameof(Isbn) }));
            }

            if (string.IsNullOrWhiteSpace(Description))
            {
                errors.Add(new ValidationResult("Введите описание!", new List<string>() { nameof(Description) }));
            }

            if (AuthorId == null && string.IsNullOrWhiteSpace(Author))
            {
                errors.Add(new ValidationResult("Укажите автора!", new List<string>() { nameof(AuthorId), nameof(Author) }));
            }

            if (PublisherId == null && string.IsNullOrWhiteSpace(Publisher))
            {
                errors.Add(new ValidationResult("Укажите издателя!", new List<string>() { nameof(PublisherId), nameof(Publisher) }));
            }

            if (HasSeries && SeriesId == null && string.IsNullOrWhiteSpace(Series))
            {
                errors.Add(new ValidationResult("Укажите серию!", new List<string>() { nameof(SeriesId), nameof(Series) }));
            }

            if (CategoryId == null && string.IsNullOrWhiteSpace(Category))
            {
                errors.Add(new ValidationResult("Укажите категорию!", new List<string>() { nameof(CategoryId), nameof(Category) }));
            }

            if (CategoryId == null && GenreId != null)
            {
                errors.Add(new ValidationResult("Создайте новый жанр для новой категории!", new List<string>() { nameof(CategoryId), nameof(GenreId) }));
            }

            if (GenreId == null && string.IsNullOrWhiteSpace(Genre))
            {
                errors.Add(new ValidationResult("Укажите жанр!", new List<string>() { nameof(GenreId), nameof(Genre) }));
            }

            return errors;
        }
    }
}
