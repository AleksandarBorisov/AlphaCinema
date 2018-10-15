using AlphaCinemaData.Models;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinemaServices
{
	public class CityServices : ICityServices
	{
		private readonly IUnitOfWork unitOfWork;
		private City city;

		public CityServices(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public int GetID(string cityName)
		{
			city = IfExist(cityName);
			if (city == null || city.IsDeleted)
			{
				throw new EntityDoesntExistException($"\n{cityName} is not present in the database.");
			}
			return city.Id;
		}

		public void AddNewCity(string cityName)
		{
            if (cityName.Length > 50)
            {
                throw new ArgumentException("City Name can't be more than 50 characters");
            }

            city = IfExist(cityName);
			if (city != null)
			{
				if (city.IsDeleted)
				{
					city.IsDeleted = false;
					this.unitOfWork.Cities.Save();
					return;
				}
				else
				{
					throw new EntityAlreadyExistsException("\nCity is already present in the database.");
				}
			}
			else
			{
				city = new City()
				{
					Name = cityName
				};
				this.unitOfWork.Cities.Add(city);
				this.unitOfWork.Cities.Save();
			}
		}

		public void DeleteCity(string cityName)
		{
			city = IfExist(cityName);
			if (city == null || city.IsDeleted)
			{
				throw new EntityDoesntExistException("\nCity is not present in the database.");
			}

			this.unitOfWork.Cities.Delete(city);
			this.unitOfWork.Cities.Save();
		}

		public ICollection<string> GetGenreNames(int cityID)
		{
			var genreNames = this.unitOfWork.Cities.All()
				.Where(city => city.Id == cityID)
				.SelectMany(c => c.Projections)
				.SelectMany(p => p.Movie.MovieGenres)
				.Select(g => g.Genre.Name)
				.Distinct()//За да вземем уникалните жанрове
				.ToList();

			return genreNames;
		}

		public ICollection<string> GetCityNames()
		{
			var cityNames = this.unitOfWork.Cities.All()
				.Select(city => city.Name)
				.ToList();

			return cityNames;
		}

		public City IfExist(string name)
		{
			return this.unitOfWork.Cities.AllAndDeleted()
				.Where(c => c.Name == name)
				.FirstOrDefault();
		}
	}
}
