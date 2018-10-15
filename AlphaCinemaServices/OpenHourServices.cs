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
		private OpenHour openHour;

		public OpenHourServices(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

        public string GetHour(int startHourID)
        {
            return this.unitOfWork.OpenHours.AllAndDeleted()
                .Where(opHour => opHour.Id == startHourID)
                .Select(oh => oh.StartHour)
                .FirstOrDefault();
        }

        private OpenHour IfExist(string openHour)
        {
            return this.unitOfWork.OpenHours.AllAndDeleted()
                .Where(opHour => opHour.StartHour == openHour)
                .FirstOrDefault();
        }

        public int GetID(string startHour)
		{
			openHour = IfExist(startHour);

			if (openHour == null || openHour.IsDeleted)
			{//Ако няма такова или е изтрито
				throw new EntityDoesntExistException($"StartHour {startHour} is not present in the database.");
			}
			return openHour.Id;
		}

		public ICollection<string> GetOpenHours()
		{
			var hours = this.unitOfWork.OpenHours.All()
				.Select(hour => hour.StartHour)
				.ToList();

			return hours;
		}
	}
}
