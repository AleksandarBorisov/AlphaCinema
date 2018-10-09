using AlphaCinemaData.Models.Contracts;
using System.Collections.Generic;

namespace AlphaCinemaData.Repository
{
    public interface IRepository<T> where T : class, IDeletable
    {
        IEnumerable<T> All();
        IEnumerable<T> AllAndDeleted();

        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        void Save();
    }
}