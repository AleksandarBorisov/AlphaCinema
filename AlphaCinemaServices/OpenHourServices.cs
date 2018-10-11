using System.Collections.Generic;
using System.Linq;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices.Contracts;

namespace AlphaCinemaServices
{
	public class OpenHourServices : IOpenHourServices
	{
		private readonly IUnitOfWork unitOfWork;

		public OpenHourServices(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public string GetID(string startHour)
		{
			var id = this.unitOfWork.OpenHours.All()
				.Where(h => h.StartHour == startHour)
				.Select(h => h.Id).FirstOrDefault();

			return id.ToString();
		}

		public List<string> GetOpenHours()
		{
			var hours = this.unitOfWork.OpenHours.All()
				.Select(hour => hour.StartHour)
				.ToList();

			return hours;
		}
	}
}
