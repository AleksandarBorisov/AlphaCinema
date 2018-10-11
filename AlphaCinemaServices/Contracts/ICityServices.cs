using System.Collections.Generic;

namespace AlphaCinemaServices.Contracts
{
	public interface ICityServices
	{
		string GetID(string cityName);
		List<string> GetCityNames();

		void AddNewCity(string cityName);
		void DeleteCity(string cityName);
	}
}
