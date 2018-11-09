using Data.Repositories.Contracts;
using Entities.DbModels;
using Microsoft.EntityFrameworkCore;
using QuizApi.Data.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
	public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
	{
		public AuthorRepository(QuizDbContext context) : base(context)
		{
			
		}

		//TEST 1
		public Author GetAuthorQuotesById(int id)
		{
			var author = context.Set<Author>()
				.Include(x => x.Quotes)
				.FirstOrDefault(y => y.Id == id);
			return author;
		}
	}
}

