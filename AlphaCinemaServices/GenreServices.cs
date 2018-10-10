using AlphaCinemaData.Models;
using AlphaCinemaData.Repository;
using AlphaCinemaServices.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinemaServices
{
    public class GenreServices : IGenreServices
    {
        private readonly IRepository<Genre> repository;

        public GenreServices(IRepository<Genre> repository)
        {
            this.repository = repository;
        }

        public string GetID(string genreName)
        {
            var id = this.repository.All()
                .Where(genre => genre.Name == genreName)
                .Select(genre => genre.Id)
                .FirstOrDefault();

            return id.ToString();
        }
        
        public List<string> GetGenreNames()
        {
            var genreNames = this.repository.All()
                .Select(genre => genre.Name)
                .ToList();

            return genreNames;
        }

    }
}
