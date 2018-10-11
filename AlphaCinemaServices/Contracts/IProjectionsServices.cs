using System;
using System.Collections.Generic;

namespace AlphaCinemaServices.Contracts
{
	public interface IProjectionsServices
	{
        void AddNewProjection(int movieID, int cityID, int openHourID, DateTime date);
        string GetID(string cityID, string movieID, string openHourID);
		List<string> GetProjections();
	}
}
