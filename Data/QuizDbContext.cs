using Entities.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class QuizDbContext : DbContext
    {
        public QuizDbContext()
        {

        }

        public QuizDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Quote> Quotes { get; set; }

        public DbSet<Author> Authors { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //TODO: connection string should be in config file
        //    optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=QuizApi;Integrated Security=True;");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasIndex(x => x.FullName).IsUnique();

            modelBuilder.Entity<Author>()
                .HasData(
                new Author { Id = 1, FullName = "Albert Einstein" },
                new Author { Id = 2, FullName = "Dr. Seuss" },
                new Author { Id = 3, FullName = "Mahatma Gandhi" },
                new Author { Id = 4, FullName = "Mark Twain" }
                );

            modelBuilder.Entity<Quote>()
                .HasData(
                new Quote { Id = 1, AuthorId = 1, Content = "Two things are infinite: the universe and human stupidity; and I'm not sure about the universe." },
                new Quote { Id = 2, AuthorId = 2, Content = "Don't cry because it's over, smile because it happened." },
                new Quote { Id = 3, AuthorId = 2, Content = "You know you're in love when you can't fall asleep because reality is finally better than your dreams." },
                new Quote { Id = 4, AuthorId = 3, Content = "Be the change that you wish to see in the world." },
                new Quote { Id = 5, AuthorId = 4, Content = "If you tell the truth, you don't have to remember anything" },
                new Quote { Id = 6, AuthorId = 3, Content = "Live as if you were to die tomorrow. Learn as if you were to live forever." }
                );
        }
    }
}
