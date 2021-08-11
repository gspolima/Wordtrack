using System.ComponentModel.DataAnnotations;
using Wordtrack.Api.Attributes;

namespace Wordtrack.Api.Dtos
{
    public class BookForUpdateDto
    {
        [Required]
        [StringLength(
            150,
            MinimumLength = 2,
            ErrorMessage = nameof(Title) + " should be at least 2 and less than 150 characters long.")]
        public string Title { get; set; }

        [Required]
        [StringLength(
            50,
            MinimumLength = 4,
            ErrorMessage = nameof(Author) + " should be at least 4 and less than 50 characters long.")]
        public string Author { get; set; }

        [Required]
        [ValidYear(ErrorMessage = nameof(YearPublished) + " cannot be in the future.")]
        public int YearPublished { get; set; }

        [Required]
        [ValidPagesLenght]
        public int Pages { get; set; }
    }
}
