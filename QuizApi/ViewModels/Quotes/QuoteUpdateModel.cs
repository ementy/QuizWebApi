using System.ComponentModel.DataAnnotations;

namespace QuizApi.ViewModels.Quotes
{
    public class QuoteUpdateModel
    {
        [Required]
        [StringLength(500)]
        public string Content { get; set; }

        [Required]
        public int AuthorId { get; set; }
    }
}
