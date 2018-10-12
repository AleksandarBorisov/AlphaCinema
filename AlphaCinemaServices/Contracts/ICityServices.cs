using AlphaCinemaData.Models;
using System.Collections.Generic;

namespace AlphaCinemaServices.Contracts
{
	public interface ICityServices
	{
		int GetID(string cityName);
		List<string> GetCityNames();
        List<string> GetGenreNames(int cityID);
		void AddNewCity(string cityName);
		void DeleteCity(string cityName);
	}
}
