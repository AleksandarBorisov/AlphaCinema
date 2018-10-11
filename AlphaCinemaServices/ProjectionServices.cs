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

		public string GetID(string cityID, string movieID, string openHourID)
		{
			var id = this.unitOfWork.Projections.All()
				.Where(pr => pr.CityId.ToString() == cityID
				&& pr.MovieId.ToString() == movieID
				&& pr.OpenHourId.ToString() == openHourID)
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
	}
}
