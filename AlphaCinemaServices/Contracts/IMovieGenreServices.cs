namespace AlphaCinemaServices.Contracts
{
    public interface IMovieGenreServices
    {
		void AddNew(int movieID, int genreID);
		void Delete(int movieID, int genreID);

    }
}
