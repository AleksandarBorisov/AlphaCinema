using AlphaCinemaData.Models;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using System.Linq;

namespace AlphaCinemaServices
{
	public class GenreServices : IGenreServices
	{
		private readonly IUnitOfWork unitOfWork;
		private Genre genre;

		public GenreServices(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public int GetID(string genreName)
		{
			genre = IfExist(genreName);
			if (genre == null || genre.IsDeleted)
			{
				throw new EntityDoesntExistException($"\n{genreName} is not present in the database.");
			}
			return genre.Id;
		}

		public void AddNewGenre(string genreName)
		{
			genre = IfExist(genreName);
			if (genre != null)
			{
				if (genre.IsDeleted)
				{
					genre.IsDeleted = false;
					this.unitOfWork.Genres.Save();
					return;
				}
				else
				{
					throw new EntityAlreadyExistsException("\nGenre is already present in the database.");
				}
			}
			else
			{
				genre = new Genre()
				{
					Name = genreName
				};
				this.unitOfWork.Genres.Add(genre);
				this.unitOfWork.Genres.Save();
			}
		}

		public void DeleteGenre(string genreName)
		{
			genre = IfExist(genreName);
			if (genre == null || genre.IsDeleted)
			{
				throw new EntityDoesntExistException("\nGenre is not present in the database.");
			}

			this.unitOfWork.Genres.Delete(genre);
			this.unitOfWork.Genres.Save();
		}

        public Genre IfExist(string genreName)
		{
			return this.unitOfWork.Genres.AllAndDeleted()
				.Where(g => g.Name == genreName)
				.FirstOrDefault();
		}
	}
}
