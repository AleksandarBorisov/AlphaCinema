using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using AlphaCinemaData.Repository;
using AlphaCinemaServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            var cities = repository.All();
            var id = cities
                .Select(city => city.Name == cityName)
                .ToString();

			return id;
		}

		public List<string> GetCityNames()
		{
            var cities = repository.All()
                .Select(select => select.Name)
                .ToList();
            
			return cities;
		}
	}
}
