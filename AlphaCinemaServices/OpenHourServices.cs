using System.Collections.Generic;
using System.Linq;
using AlphaCinemaData.Models;
using AlphaCinemaData.Repository;
using AlphaCinemaServices.Contracts;

namespace AlphaCinemaServices
{
	public class OpenHourServices : IOpenHourServices
	{
		private readonly IRepository<OpenHour> repository;

		public OpenHourServices(IRepository<OpenHour> repository)
		{
			this.repository = repository;
		}

		public string GetID(string startHour)
		{
			var id = repository.All()
				.Where(h => h.StartHour == startHour)
				.Select(h => h.Id).FirstOrDefault();

			return id.ToString();
		}

		public List<string> GetOpenHours()
		{
			var hours = repository.All()
				.Select(hour => hour.StartHour)
				.ToList();

			return hours;
		}
	}
}
