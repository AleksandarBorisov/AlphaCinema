using System.Collections.Generic;

namespace AlphaCinemaServices.Contracts
{
	public interface IOpenHourServices
	{
		int GetID(string startHour);
		List<string> GetOpenHours();

	}
}
