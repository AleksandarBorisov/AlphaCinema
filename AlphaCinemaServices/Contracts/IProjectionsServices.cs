using AlphaCinemaData.Models.Associative;
using System;
using System.Collections.Generic;

namespace AlphaCinemaServices.Contracts
{
	public interface IProjectionsServices
	{
        void AddNewProjection(int movieID, int cityID, int openHourID);
		int GetID(int cityID, int movieID, int openHourID);
		void Delete(int movieID, int cityID, int openHourID);

		ICollection<string> GetOpenHoursByMovieIDCityID(int movieID, int cityID);
		Projection IfExist(int cityID, int movieID, int openHourID);
	}
}
