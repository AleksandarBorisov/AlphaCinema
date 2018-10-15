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
		private MovieGenre movieGenre;
        
        public MovieGenreServices(IUnitOfWork unitOfWork)
        {
			this.unitOfWork = unitOfWork;
        }

		public void AddNew(int movieID, int genreID)
		{
			movieGenre = IfExist(movieID, genreID);
			// TODO: трябва да се добави добавяне на нов жанр и филм, малко по-късно ще го оправя
			if (movieGenre != null)
			{
				if (movieGenre.IsDeleted)
				{
					movieGenre.IsDeleted = false;

					this.unitOfWork.SaveChanges();
					return;
				}
				throw new EntityAlreadyExistsException($"Movie Genre combination is already present in database.");
			}
			else
			{
				movieGenre = new MovieGenre()
				{
					MovieId = movieID,
					GenreId = genreID
				};
				this.unitOfWork.MovieGenres.Add(movieGenre);
				this.unitOfWork.SaveChanges();
			}
		}

		public void Delete(int movieID, int genreID)
		{
			movieGenre = IfExist(movieID, genreID);

			if (movieGenre == null || movieGenre.IsDeleted)
			{
				throw new EntityDoesntExistException("\nProjection is not present in the database.");
			}
			else if (!movieGenre.IsDeleted)
			{
				this.unitOfWork.MovieGenres.Delete(movieGenre);
			}
			this.unitOfWork.Cities.Save();
		}

		public MovieGenre IfExist(int movieID, int genreID)
		{
			return this.unitOfWork.MovieGenres.AllAndDeleted()
				.Where(mg => mg.Movie.Id == movieID && mg.Genre.Id == genreID)
				.FirstOrDefault();
		}
    }
}
