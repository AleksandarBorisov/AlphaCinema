using AlphaCinemaData.Models.Associative;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinemaServices
{
	public class ProjectionServices : IProjectionsServices
	{
		private readonly IUnitOfWork unitOfWork;
		private Projection projection;

		public ProjectionServices(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public void AddNewProjection(int movieID, int cityID, int openHourID)
		{
			projection = IfExist(cityID, movieID, openHourID);
			//Check if the projection already exist
			if (projection != null)
			{
				if (projection.IsDeleted)
				{
                    projection.IsDeleted = false;

                    this.unitOfWork.SaveChanges();

                    return;
				}
                throw new EntityAlreadyExistsException($"Projection with cityId:{cityID}," +
				$" movieId:{movieID}, openHourId:{openHourID} " +
				$"is already present in the database.");
			}
			else
			{
				projection = new Projection
				{
					MovieId = movieID,
					CityId = cityID,
					OpenHourId = openHourID
				};

                this.unitOfWork.Projections.Add(projection);
				this.unitOfWork.SaveChanges();
			}
		}

		public int GetID(int cityID, int movieID, int openHourID)
		{
			projection = IfExist(cityID, movieID, openHourID);
            if (projection == null || projection.IsDeleted)
			{
				throw new EntityDoesntExistException($"\nProjection is not present in the database.");
			}
			return projection.Id;
		}

		public void Delete(int cityID, int movieID, int openHourID)
		{
			projection = IfExist(cityID, movieID, openHourID);

            if (projection == null || projection.IsDeleted)
			{
				throw new EntityDoesntExistException("\nProjection is not present in the database.");
			}
			else if (!projection.IsDeleted)
			{
				this.unitOfWork.Projections.Delete(projection);
			}
			this.unitOfWork.SaveChanges();
		}

		public Projection IfExist(int cityID, int movieID, int openHourID)
		{
			return this.unitOfWork.Projections.AllAndDeleted()
				.Where(pr => pr.MovieId == movieID)
				.Where(pr => pr.CityId == cityID)
				.Where(pr => pr.OpenHourId == openHourID)
				.FirstOrDefault();
		}

		//private bool CompareDates(DateTime firstDate, DateTime secondDate)
		//{
		//	return firstDate.Year == secondDate.Year
		//		&& firstDate.Day == secondDate.Day
		//		&& firstDate.Month == secondDate.Month;
		//}

		public ICollection<string> GetOpenHoursByMovieIDCityID(int movieID, int cityID)
		{
			var openHours = this.unitOfWork.Projections.All()
			.Where(movie => movie.MovieId == movieID)
			.Where(city => city.CityId == cityID)
			.Select(openHour => openHour.OpenHour.StartHour).ToList();

			return openHours;
		}

	}
}