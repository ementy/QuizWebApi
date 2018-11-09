using QuizApi.ViewModels.Quotes;
using System.Collections.Generic;

namespace QuizApi.ViewModels.Author
{
	public class AuthorWithQuotesViewModel : AuthorViewModel
	{
		public ICollection<QuoteViewModel> Quotes { get; set; }
	}
}
