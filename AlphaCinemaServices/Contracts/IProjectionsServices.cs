using System;
using System.Collections.Generic;

namespace AlphaCinemaServices.Contracts
{
	public interface IProjectionsServices
	{
        void AddNewProjection(int movieID, int cityID, int openHourID, DateTime date);
		int GetID(int cityID, int movieID, int openHourID);
        List<string> GetOpenHoursByMovieIDCityID(string movieIDAsString, string cityIDAsString);
		DateTime GetDate(int movieID, int cityID, int openHourID);
		void Delete(int movieID, int cityID, int openHourID, DateTime date);
	}
}
