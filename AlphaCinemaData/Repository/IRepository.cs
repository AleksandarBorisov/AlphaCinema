using System.Collections.Generic;
using AlphaCinemaData.Models.Contracts;

namespace AlphaCinemaData.Repository
{
    public interface IRepository<T> where T : class, IDeletable
    {
        void Add(T entity);
        IEnumerable<T> All();
        IEnumerable<T> AllAndDeleted();
        void Delete(T entity);
        void Save();
        void Update(T entity);
    }
}