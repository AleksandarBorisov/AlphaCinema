using AlphaCinemaData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinemaServices.Contracts
{
    public interface IMovieGenreServices
    {
		void AddNew(int movieID, int genreID);
		void Delete(int movieID, int genreID);

		List<string> GetMovieNamesByGenreID(string genreID);
	}
}
