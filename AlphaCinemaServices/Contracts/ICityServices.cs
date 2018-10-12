using AlphaCinemaData.Models;
using System.Collections.Generic;

namespace AlphaCinemaServices.Contracts
{
	public interface ICityServices
	{
		int GetID(string cityName);
		List<string> GetCityNames();

		void AddNewCity(string cityName);
		void DeleteCity(string cityName);
		List<string> GetGenreNames(int cityID);
	}
}
