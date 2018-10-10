using AlphaCinemaData.Models;
using AlphaCinemaData.Repository;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
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
                .Where(genre => genre.Name == genreName && genre.IsDeleted == false)
                .Select(genre => genre.Id)
                .FirstOrDefault();

            return id.ToString();
        }
        
        public List<string> GetGenreNames()
        {
            var genreNames = this.repository.All()
				.Where(genre => genre.IsDeleted == false)
                .Select(genre => genre.Name)
                .ToList();

            return genreNames;
        }

		public void AddNewGenre(string genreName)
		{
			if (IfExist(genreName) && IsDeleted(genreName))
			{
				var genre = repository.All()
					.FirstOrDefault(g => g.Name == genreName);
				genre.IsDeleted = false;
				repository.Save();
				return;
			}
			else if (IfExist(genreName) && !IsDeleted(genreName))
			{
				throw new EntityAlreadyExistsException("Genre is already present in the database.");
			}
			else
			{
				var genre = new Genre()
				{
					Name = genreName
				};
				repository.Add(genre);
				repository.Save();
			}
		}

		public void DeleteGenre(string genreName)
		{
			if (!IfExist(genreName))
			{
				throw new EntityDoesntExistException("Genre is not present in the database.");
			}
			var entity = repository.All()
				.Where(g => g.Name == genreName)
				.FirstOrDefault();

			repository.Delete(entity);
			repository.Save();
		}

		private bool IfExist(string name)
		{
			var result = repository.All()
				.Where(g => g.Name == name)
				.FirstOrDefault();
			// трябва ми ламбда израз ама ми се спи много в момента :D
			if (result == null) return false;
			else return true;
		}

		private bool IsDeleted(string genreName)
		{
			var result = repository.All()
				.Where(g => g.Name == genreName)
				.FirstOrDefault()
				.IsDeleted;
			return result;
		}
    }
}
