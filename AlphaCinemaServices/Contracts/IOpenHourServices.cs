using System.Collections.Generic;

namespace AlphaCinemaServices.Contracts
{
	public interface IOpenHourServices
	{
		int GetID(string startHour);
		ICollection<string> GetOpenHours();

		string GetHour(int startHourID);
    }
}
