using Data.Repositories.Contracts;
using Entities.DbModels;
using QuizApi.Data.Repositories.Contracts;

namespace Data.Repositories
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(QuizDbContext context) : base(context)
        {
        }
    }
}
