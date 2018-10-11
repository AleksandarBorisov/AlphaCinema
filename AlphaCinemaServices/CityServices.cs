using AlphaCinemaData.Models;
using AlphaCinemaData.Repository;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinemaServices
{
	public class CityServices : ICityServices
	{
		private readonly IUnitOfWork unitOfWork;

		public CityServices(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

        private City IfExist(string name)
        {
            return this.unitOfWork.Cities.AllAndDeleted()
                .Where(c => c.Name == name)
                .FirstOrDefault();
        }
        
        public string GetID(string cityName)
		{
            if (IfExist(cityName) == null)
            {
                throw new EntityDoesntExistException($"City {cityName} is not present in the database.");
            }

			var id = this.unitOfWork.Cities.All()
				.Where(c => c.Name == cityName)
				.Select(c => c.Id).FirstOrDefault();

			return id.ToString();
		}

		public List<string> GetCityNames()
		{
            var cityNames = this.unitOfWork.Cities.All()
                .Select(city => city.Name)
                .ToList();
            
			return cityNames;
		}

		public void AddNewCity(string cityName)
		{
			if (IfExist(cityName) && IsDeleted(cityName))
			{
				var city = this.unitOfWork.Cities.AllAndDeleted()
					.FirstOrDefault(c => c.Name == cityName);
				city.IsDeleted = false;
				this.unitOfWork.Cities.Save();

				return;
			}
			else if (IfExist(cityName) && !IsDeleted(cityName))
			{
				throw new EntityAlreadyExistsException("\nCity is already present in the database.");
			}
			else
			{
				var city = new City()
				{
					Name = cityName
				};
				this.unitOfWork.Cities.Add(city);
				this.unitOfWork.Cities.Save();
			}
		}

		public void DeleteCity(string cityName)
		{
			if (!IfExist(cityName))
			{
				throw new EntityDoesntExistException("\\nCity is not present in the database.");
			}
			else if (IfExist(cityName) && IsDeleted(cityName))
			{
				throw new EntityDoesntExistException("\\nCity is not present in the database.");
			}
			var entity = this.unitOfWork.Cities.All()
				.Where(c => c.Name == cityName)
				.FirstOrDefault();

			this.unitOfWork.Cities.Delete(entity);
			this.unitOfWork.Cities.Save();
		}

		private bool IfExist(string name)
		{
			return this.unitOfWork.Cities.AllAndDeleted()
				.Where(c => c.Name == name)
				.FirstOrDefault() == null ? false : true;
		}

		private bool IsDeleted(string cityName)
		{
			var result = this.unitOfWork.Cities.AllAndDeleted()
				.Where(c => c.Name == cityName)
				.FirstOrDefault()
				.IsDeleted;
			return result;
		}
	}
}
