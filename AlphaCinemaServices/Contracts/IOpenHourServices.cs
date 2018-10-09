using System.Collections.Generic;

namespace AlphaCinemaServices.Contracts
{
	public interface IOpenHourServices
	{
		string GetID(string startHour);
		List<string> GetOpenHours();

	}
}
