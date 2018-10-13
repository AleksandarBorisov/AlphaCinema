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

		public GenreServices(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public int GetID(string genreName)
		{
			if (!IfExist(genreName) || IsDeleted(genreName))
			{
				throw new EntityDoesntExistException($"\n{genreName} is not present in the database.");
			}
			var id = this.unitOfWork.Genres.All()
				.Where(genre => genre.Name == genreName)
				.Select(genre => genre.Id)
				.FirstOrDefault();

			return id;
		}

		public void AddNewGenre(string genreName)
		{
            if (IfExist(genreName) && IsDeleted(genreName))
			{
				var genre = this.unitOfWork.Genres.AllAndDeleted()
					.FirstOrDefault(g => g.Name == genreName);
				genre.IsDeleted = false;
				this.unitOfWork.Genres.Save();

				return;
			}
			else if (IfExist(genreName) && !IsDeleted(genreName))
			{
				throw new EntityAlreadyExistsException($"Genre {genreName} is already present in the database.");
			}
			else
			{
				var genre = new Genre()
				{
					Name = genreName
				};
				this.unitOfWork.Genres.Add(genre);
				this.unitOfWork.Genres.Save();
			}
		}

		public void DeleteGenre(string genreName)
		{
			if (!IfExist(genreName))
			{
				throw new EntityDoesntExistException("\nGenre is not present in the database.");
			}
			else if (IfExist(genreName) && IsDeleted(genreName))
			{
				throw new EntityDoesntExistException($"Genre {genreName} is not present in the database.");
			}
			var entity = this.unitOfWork.Genres.All()
				.Where(g => g.Name == genreName)
				.FirstOrDefault();

			this.unitOfWork.Genres.Delete(entity);
			this.unitOfWork.Genres.Save();
		}

        private bool IfExist(string name)
		{
			return this.unitOfWork.Genres.AllAndDeleted()
				.Where(g => g.Name == name)
				.FirstOrDefault() == null ? false : true;
		}

		private bool IsDeleted(string genreName)
		{
			var result = this.unitOfWork.Genres.AllAndDeleted()
				.Where(g => g.Name == genreName)
				.FirstOrDefault()
				.IsDeleted;
			return result;
		}
	}
}
