using AlphaCinemaData.Models;
using System;
using System.Collections.Generic;

namespace AlphaCinemaServices.Contracts
{
	public interface IMovieServices
	{
		int GetID(string movieName);
		List<string> GetMovieNames();
        void AddNewMovie(string name, string description, int releaseYear, int duration);
        void DeleteMovie(string movieName);
        Movie GetMovieByName(string movieName);

		List<string> GetMovieNamesByCityIDGenreID(
			int genreID,
			int cityID);



	}
}
