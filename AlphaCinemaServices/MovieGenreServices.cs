using AlphaCinemaData.Models.Associative;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using System.Linq;

namespace AlphaCinemaServices
{
    public class MovieGenreServices : IMovieGenreServices
    {
		private readonly IUnitOfWork unitOfWork;
        
        public MovieGenreServices(IUnitOfWork unitOfWork)
        {
			this.unitOfWork = unitOfWork;
        }

		public void AddNew(int movieID, int genreID)
		{
			 // TODO: трябва да се добави добавяне на нов жанр и филм, малко по-късно ще го оправя
			if (IfExist(movieID, genreID) && IsDeleted(movieID, genreID))
			{
				var movieGenreObj = this.unitOfWork.MovieGenres.AllAndDeleted()
					.FirstOrDefault(mg => mg.MovieId == movieID && mg.GenreId == genreID);
				movieGenreObj.IsDeleted = false;
				this.unitOfWork.SaveChanges();
			}
			else if (IfExist(movieID, genreID) && !IsDeleted(movieID, genreID))
			{
				throw new EntityAlreadyExistsException("That link is already added.");
			}
			else
			{
				var movieGenreObj = new MovieGenre()
				{
					MovieId = movieID,
					GenreId = genreID
				};
				this.unitOfWork.MovieGenres.Add(movieGenreObj);
				this.unitOfWork.SaveChanges();
			}
		}

		public void Delete(int movieID, int genreID)
		{
			if (IfExist(movieID, genreID) && IsDeleted(movieID, genreID))
			{
				throw new EntityDoesntExistException("\n Link is not present in the database.");
			}
			else if (IfExist(movieID, genreID) && !IsDeleted(movieID, genreID))
			{
				var movieGenreObject = this.unitOfWork.MovieGenres.All()
					.Where(mg => mg.MovieId == movieID && mg.GenreId == genreID)
					.FirstOrDefault();
				this.unitOfWork.MovieGenres.Delete(movieGenreObject);
			}
			this.unitOfWork.Cities.Save();
		}

		private bool IfExist(int movieID, int genreID)
		{
			return this.unitOfWork.MovieGenres.AllAndDeleted()
				.Where(mg => mg.Movie.Id == movieID && mg.Genre.Id == genreID)
				.FirstOrDefault() == null ? false : true;
		}

		private bool IsDeleted(int movieId, int genreID)
		{
			return this.unitOfWork.MovieGenres.AllAndDeleted()
				.Where(mg => mg.MovieId == movieId && mg.GenreId == genreID)
				.FirstOrDefault()
				.IsDeleted;
		}
    }
}
