using System.ComponentModel.DataAnnotations;
using Wordtrack.Api.Attributes;

namespace Wordtrack.Api.Dtos
{
    public class BookForCreationDto
    {
        [Required]
        [StringLength(
            150,
            ErrorMessage = nameof(Title) + " should be at least 2 and less than 150 characters long.",
            MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        [StringLength(
            50,
            ErrorMessage = nameof(Author) + " should be at least 4 and less than 50 characters long.",
            MinimumLength = 4)]
        public string Author { get; set; }

        [Required]
        [ValidYear(ErrorMessage = nameof(YearPublished) + " cannot be in the future.")]
        public int YearPublished { get; set; }

        [Required]
        [ValidPagesLenght(ErrorMessage = nameof(Pages) + " should be at least 1 and less than 7312")]
        public int Pages { get; set; }
    }
}
