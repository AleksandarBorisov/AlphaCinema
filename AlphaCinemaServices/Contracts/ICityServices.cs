using System.Collections.Generic;

namespace AlphaCinemaServices.Contracts
{
	public interface ICityServices
	{
		string GetID(string cityName);
		List<string> GetCityNames();
        List<string> GetGenreNames(string cityIDAsString);

    }
}
