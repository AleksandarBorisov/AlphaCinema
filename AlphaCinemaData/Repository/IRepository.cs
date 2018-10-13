using System.Linq;
using AlphaCinemaData.Models.Contracts;

namespace AlphaCinemaData.Repository
{
    public interface IRepository<T> where T : class, IDeletable
    {
        void Add(T entity);
		IQueryable<T> All();
		IQueryable<T> AllAndDeleted();
        void Delete(T entity);
        void Save();
        void Update(T entity);
    }
}