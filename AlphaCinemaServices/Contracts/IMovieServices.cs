using System;
using System.Collections.Generic;

namespace AlphaCinemaServices.Contracts
{
	public interface IMovieServices
	{
		string GetID(string movieName);
		List<string> GetMovieNames();
        void AddNewMovie(string name, string description, int releaseYear, int duration);
        List<string> GetMovieNamesByCityIDGenreID(
            string genreIDAsString,
            string cityIDAsString);
    }
}
