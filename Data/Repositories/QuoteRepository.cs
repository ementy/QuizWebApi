using Microsoft.EntityFrameworkCore;
using QuizApi.Data.Repositories.Contracts;
using Entities.DbModels;
using Data.Repositories.Contracts;
using Data;

namespace QuizApi.Data.Repositories
{
    public class QuoteRepository : BaseRepository<Quote>, IQuoteRepository 
    {
        public QuoteRepository(QuizDbContext context) : base(context)
        {

        }
    }
}
