using AlphaCinemaData.Models.Associative;
using System;
using System.Collections.Generic;

namespace AlphaCinemaServices.Contracts
{
	public interface IProjectionsServices
	{
        void AddNewProjection(int movieID, int cityID, int openHourID, DateTime date);
		int GetID(int cityID, int movieID, int openHourID);
		DateTime GetDate(int movieID, int cityID, int openHourID);
		void Delete(int movieID, int cityID, int openHourID, DateTime date);

		Projection GetProjectionByID(int id);
		List<string> GetOpenHoursByMovieIDCityID(int movieID, int cityID);
		Projection GetProjection(int cityID, int movieID, int openHourID);
	}
}
