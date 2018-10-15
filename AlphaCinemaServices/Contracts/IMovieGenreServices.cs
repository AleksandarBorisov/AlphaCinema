using AlphaCinemaData.Models.Associative;

namespace AlphaCinemaServices.Contracts
{
    public interface IMovieGenreServices
    {
		void AddNew(int movieID, int genreID);
		void Delete(int movieID, int genreID);
		MovieGenre IfExist(int movieID, int genreID);


	}
}
