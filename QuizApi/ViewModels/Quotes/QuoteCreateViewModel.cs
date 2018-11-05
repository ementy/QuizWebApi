using System.ComponentModel.DataAnnotations;

namespace QuizApi.ViewModels.Quotes
{
    public class QuoteCreateViewModel
    {
        //display name and error message?
        [Required]
        [StringLength(500)]
        public string Content { get; set; }

        [Required]
        public int AuthorId { get; set; }
    }
}
