using AlphaCinemaData.Models;
using AlphaCinemaData.Repository;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices.Contracts;
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

		public string GetID(string cityName)
		{
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

        public List<string> GetGenreNames(string cityIDAsString)
        {
            int cityID = int.Parse(cityIDAsString);

            var genreNames = this.unitOfWork.Cities.All()
                .Where(city => city.Id == cityID)
                .SelectMany(c => c.Projections)
                .SelectMany(p => p.Movie.MovieGenres)
                .Select(g => g.Genre.Name)
                .Distinct()//За да вземем уникалните жанрове
                .ToList();

            return genreNames;
        }
    }
}
