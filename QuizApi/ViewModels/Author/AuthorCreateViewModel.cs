using System.ComponentModel.DataAnnotations;

namespace QuizApi.ViewModels.Author
{
    public class AuthorCreateViewModel
    {
        //display name and error message?
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
    }
}
