using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinemaServices.Contracts
{
	public interface ICityServices
	{
		string GetID(string cityName);
		List<string> GetCityNames();
	}
}
