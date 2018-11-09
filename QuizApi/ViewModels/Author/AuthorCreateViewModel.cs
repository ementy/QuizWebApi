using System.ComponentModel.DataAnnotations;

namespace QuizApi.ViewModels.Author
{
	/// <summary>
	/// The model that needs to be filled when creating a new author.
	/// </summary>
	public class AuthorCreateViewModel
    {
		/// <summary>
		/// A FullName field containing the name of the author in string format with maximum allowed lenght 100 symbols. The fiels is required.
		/// </summary>
        [Required]
        [StringLength(100)]
		[Display(Name = "Full Name")]
        public string FullName { get; set; }
    }
}
