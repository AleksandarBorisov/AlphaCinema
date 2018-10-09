using AlphaCinemaData.Models;
using AlphaCinemaData.Repository;
using AlphaCinemaServices.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinemaServices
{
	public class CityServices : ICityServices
	{
		private readonly IRepository<City> repository;

		public CityServices(IRepository<City> repository)
		{
			this.repository = repository;
		}

		public string GetID(string cityName)
		{
			var id = repository.All()
				.Where(c => c.Name == cityName)
				.Select(c => c.Id).FirstOrDefault();

			return id.ToString();
		}

		public List<string> GetCityNames()
		{
            var cityNames = repository.All()
                .Select(city => city.Name)
                .ToList();
            
			return cityNames;
		}
	}
}
