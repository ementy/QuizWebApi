using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QuizApi.Data.Repositories.Contracts
{
    public interface IBaseRepository<T> where T : class
    {
        //receives no arguments and returns enumerable collection of entities of generic type T
        IQueryable<T> GetAll();

        //receives no arguments and returns asynchronously Task  with enumerable collection of entities of generic type T
        Task<IQueryable<T>> GetAllAsync();       

        //receives parameter id of type object and returns an entity of generic type T with the proviced id
        T GetById(object id);

        //receives parameter id of type object and returns asynchronously a taks of entity of generic type T with the proviced id
        Task<T> GetByIdAsync(object id);

        //receives parameter of generic type T and adds the provided argument to the DbSet of type T
        void Add(T entity);

        //receives parameter of generic type T, adds the provided argument asynchronously to the DbSet of type T and returns a Task
        Task AddAsync(T entity);

        //receives parameter of generic type T and deletes the provided argument from DbSet of type T
        void Delete(T entity);

        //receives parameter of generic type T, deletes the provided argument asynchronously from DbSet of type T and returns a Task
        Task DeleteAsync(T entity);

        //receives parameter of generic type T and updates the entity with in the DbSet of type T
        void Update(T entity);

        //receives parameter of generic type T, updates the entity asynchronously with in the DbSet of type T and returns a Task
        Task UpdateAsync(T entity);

        //returns the number of entities in the DbSet
        int Count();

        //returns asynchronously Task with the number of entities in the DbSet
        Task<int> CountAsync();

        //saves the changes to the DbContext
        void Save();

        //saves the changes asynchronously to the DbContext and returns an async Task
        Task SaveAsync();
	}
}
