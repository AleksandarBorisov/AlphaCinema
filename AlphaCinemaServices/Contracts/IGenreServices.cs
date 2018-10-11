using AlphaCinemaData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinemaServices.Contracts
{
    public interface IGenreServices
    {
        string GetID(string genreName);

        List<string> GetGenreNames();
        List<string> GetGenreNames(string cityID);

		void AddNewGenre(string genreName);
		void DeleteGenre(string genreName);
		Genre GetGenreByName(string movieName);

	}
}
