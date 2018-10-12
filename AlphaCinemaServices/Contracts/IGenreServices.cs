using AlphaCinemaData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinemaServices.Contracts
{
    public interface IGenreServices
    {
        int GetID(string genreName);
		void AddNewGenre(string genreName);
		void DeleteGenre(string genreName);

	}
}
