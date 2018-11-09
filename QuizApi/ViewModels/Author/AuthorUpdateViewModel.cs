using System.ComponentModel.DataAnnotations;

namespace QuizApi.ViewModels.Author
{
    public class AuthorUpdateViewModel
    {
        //display name and error message?
        [Required]
        [StringLength(100)]
		[Display(Name = "Full Name")]
        public string FullName { get; set; }
    }
}
