using System.Collections.Generic;
using System.Linq;
using AlphaCinemaData.Models;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;

namespace AlphaCinemaServices
{
	public class OpenHourServices : IOpenHourServices
	{
		private readonly IUnitOfWork unitOfWork;

		public OpenHourServices(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

        private OpenHour IfExist(string openHour)
        {
            return this.unitOfWork.OpenHours.AllAndDeleted()
                .Where(opHour => opHour.StartHour.ToString() == openHour)
                .FirstOrDefault();
        }

        public int GetID(string startHour)
		{
            if(IfExist(startHour) == null)
            {
                throw new EntityDoesntExistException($"StartHour {startHour} is not present in the database.");
            }

            var id = this.unitOfWork.OpenHours.All()
				.Where(h => h.StartHour == startHour)
				.Select(h => h.Id).FirstOrDefault();

			return id;
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
