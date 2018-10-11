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
	}
}
