using AlphaCinemaData.Models.Associative;
using AlphaCinemaData.Repository;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinemaServices
{
	public class ProjectionServices : IProjectionsServices
	{
		private readonly IUnitOfWork unitOfWork;

		public ProjectionServices(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public string GetID(string cityIDAsString, string movieIDAsString, string openHourIDAsString)
		{
            int cityID = int.Parse(cityIDAsString);
            int movieID = int.Parse(movieIDAsString);
            int openHourID = int.Parse(openHourIDAsString);

            var id = this.unitOfWork.Projections.All()
				.Where(pr => pr.CityId == cityID)
                .Where(pr => pr.MovieId == movieID)
                .Where(pr => pr.OpenHourId == openHourID)
				.Select(pr => pr.Id).FirstOrDefault();

			return id.ToString();
		}

		public List<string> GetProjections()
		{
			var projections = this.unitOfWork.Projections.All()
				.Select(pr => pr.Id.ToString())
				.ToList();

			return projections;
		}

        public List<string> GetOpenHoursByMovieIDCityID(string movieIDAsString, string cityIDAsString)
        {
            int cityID = int.Parse(cityIDAsString);
            int movieID = int.Parse(movieIDAsString);
            var openHours = this.unitOfWork.Projections.All()
            .Where(movie => movie.MovieId == movieID)
            .Where(city => city.CityId == cityID)
            .Select(openHour => openHour.OpenHour.StartHour).ToList();

            return openHours;
        }

    }
}
