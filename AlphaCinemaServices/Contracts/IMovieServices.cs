using AlphaCinemaData.Models;
using System;
using System.Collections.Generic;

namespace AlphaCinemaServices.Contracts
{
	public interface IMovieServices
	{
		int GetID(string movieName);
        void AddNewMovie(string name, string description, int releaseYear, int duration);
        List<string> GetMovieNamesByCityIDGenreID(
            string genreIDAsString,
            string cityIDAsString);
        void DeleteMovie(string movieName);
	}
}
