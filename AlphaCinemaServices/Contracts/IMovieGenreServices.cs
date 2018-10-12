using AlphaCinemaData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinemaServices.Contracts
{
    public interface IMovieGenreServices
    {
        List<string> GetMovieIDsByGenreName(string genreName);
        List<string> GetGenreIDsByMovie(string movieName);
        List<string> GetMovieNamesByGenreName(string genreName);
        List<string> GetGenreNamesByMovie(string movieName);
		List<string> GetMovieNamesByGenreID(string genreID);
		void AddNew(int movieID, int genreID);
		void Delete(int movieID, int genreID);
	}
}
