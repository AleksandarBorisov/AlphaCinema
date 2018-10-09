using System.Collections.Generic;

namespace AlphaCinemaServices.Contracts
{
	public interface IProjectionsServices
	{
		string GetID(string cityID, string movieID, string openHourID);
		List<string> GetProjections();
	}
}
