using AlphaCinemaData.Models.Associative;
using AlphaCinemaData.Repository;
using AlphaCinemaServices.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinemaServices
{
	public class ProjectionsServices : IProjectionsServices
	{
		private readonly IRepository<Projection> repository;

		public ProjectionsServices(IRepository<Projection> repository)
		{
			this.repository = repository;
		}

		public string GetID(string cityID, string movieID, string openHourID)
		{
			var id = repository.All()
				.Where(pr => pr.CityId.ToString() == cityID
				&& pr.MovieId.ToString() == movieID
				&& pr.OpenHourId.ToString() == openHourID)
				.Select(pr => pr.Id).FirstOrDefault();

			return id.ToString();
		}

		public List<string> GetProjections()
		{
			var projections = repository.All()
				.Select(pr => pr.Id.ToString())
				.ToList();

			return projections;
		}
	}
}
