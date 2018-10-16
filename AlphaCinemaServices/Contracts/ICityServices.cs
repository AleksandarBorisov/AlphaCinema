using AlphaCinemaData.Models;
using System.Collections.Generic;

namespace AlphaCinemaServices.Contracts
{
	public interface ICityServices
	{
		int GetID(string cityName);
		ICollection<string> GetGenreNames(int cityID);

		void AddNewCity(string cityName);
		void DeleteCity(string cityName);
		ICollection<string> GetCityNames();
	}
}
