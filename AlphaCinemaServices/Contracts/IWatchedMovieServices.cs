using AlphaCinemaData.Models;
using System.Collections.Generic;

namespace AlphaCinemaServices.Contracts
{
    public interface IWatchedMovieServices
    {
		void AddNewWatchedMovie(int userID, int resevationID);
		IEnumerable<Movie> GetWatchedMoviesByUserName(string userName);

	}
}
