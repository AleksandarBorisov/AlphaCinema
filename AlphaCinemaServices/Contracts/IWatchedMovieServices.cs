using AlphaCinemaData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinemaServices.Contracts
{
    public interface IWatchedMovieServices
    {
		void AddNewWatchedMovie(int userID, int resevationID);
		IEnumerable<Movie> GetWatchedMoviesByUserName(string userName);

	}
}
