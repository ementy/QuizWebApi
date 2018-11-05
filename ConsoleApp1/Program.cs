using Data;
using Data.Repositories;
using Entities.DbModels;
using System;
using System.Linq;

namespace sometests
{
    public class Program
    {
        static void Main(string[] args)
        {
            var context = new QuizDbContext();
            var repo = new AuthorRepository(context);

            ////Get all authors
            //var authors = repo.GetAll().Select(x => x.FullName);
            //Console.WriteLine(string.Join(Environment.NewLine, authors));

            ////Get all author async
            //var authorsAsync = repo.GetAllAsync().Result.Select(x => x.FullName);
            //Console.WriteLine(string.Join(Environment.NewLine, authorsAsync));

            ////get author by id
            //var author = repo.GetById(1).FullName;
            //Console.WriteLine(author);

            ////get author async by id
            //var author = repo.GetByIdAsync(1).Result.FullName;
            //Console.WriteLine(author);

            ////Add new author
            //var newAuthor = new Author { FullName = "Winnie the pooh" };
            //repo.Add(newAuthor);
            //var authorss = repo.GetAll().Select(x => x.FullName);
            //Console.WriteLine(string.Join(Environment.NewLine, authorss));

            ////get the count of all authors and delete the last one
            //var index = repo.Count();
            //var author = repo.GetById(index);
            //repo.Delete(author);
            //var authorss = repo.GetAll().Select(x => x.FullName);
            //Console.WriteLine(string.Join(Environment.NewLine, authorss));

            ////edit the last author - add "edited" to his/its name
            //var index = repo.Count();
            //var author = repo.GetById(index);
            //author.FullName = author.FullName + " edited";
            //repo.Update(author);
            //var authorss = repo.GetAll().Select(x => x.FullName);
            //Console.WriteLine(string.Join(Environment.NewLine, authorss));
        }
    }
}
