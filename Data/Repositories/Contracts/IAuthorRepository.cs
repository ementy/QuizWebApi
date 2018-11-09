using Entities.DbModels;
using QuizApi.Data.Repositories.Contracts;
using System.Collections.Generic;

namespace Data.Repositories.Contracts
{
	public interface IAuthorRepository : IBaseRepository<Author>
    {
		Author GetAuthorQuotesById(int id);
	}
}
