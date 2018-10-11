using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinemaServices.Contracts
{
    public interface IMovieGenreServices
    {
        List<string> GetMovieIDsByGenre(string genreName);
        List<string> GetGenreIDsByMovie(string movieName);
        List<string> GetMovieNamesByGenre(string genreName);
        List<string> GetGenreNamesByMovie(string movieName);
		List<string> GetMovieNamesByGenreID(string genreID);


	}
}
