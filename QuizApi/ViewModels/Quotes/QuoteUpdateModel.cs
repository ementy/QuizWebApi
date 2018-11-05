using System.ComponentModel.DataAnnotations;

namespace QuizApi.ViewModels.Quotes
{
    public class QuoteUpdateModel
    {
        [Required]
        [StringLength(500)]
        //display name and error message?
        public string Content { get; set; }

        [Required]
        public int AuthorId { get; set; }
    }
}
