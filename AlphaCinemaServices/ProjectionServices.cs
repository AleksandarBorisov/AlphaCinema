using AlphaCinemaData.Models.Associative;
using AlphaCinemaData.Repository;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace AlphaCinemaServices
{
	public class ProjectionServices : IProjectionsServices
	{
		private readonly IUnitOfWork unitOfWork;

		public ProjectionServices(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		private Projection IfExist(Projection inputProjection)
		{
			return this.unitOfWork.Projections.AllAndDeleted()
				.Where(projection => projection.MovieId == inputProjection.MovieId &&
				projection.CityId == inputProjection.CityId &&
				projection.OpenHourId == inputProjection.OpenHourId &&
				projection.Date == inputProjection.Date)
				.FirstOrDefault();
		}

		private bool IsDeleted(Projection inputProjection)
		{
			return this.unitOfWork.Projections.AllAndDeleted()
				.Where(projection => projection.MovieId == inputProjection.MovieId &&
				projection.CityId == inputProjection.CityId &&
				projection.OpenHourId == inputProjection.OpenHourId &&
				projection.Date == inputProjection.Date)
				.FirstOrDefault()
				.IsDeleted;
		}

		public void AddNewProjection(int movieID, int cityID, int openHourID, DateTime date)
		{
			//Create projection object from the input parameters

			//Check if the projection already exist
			if (IfExist(movieID, cityID, openHourID, date))
			{
				if (IsDeleted(movieID, cityID, openHourID, date))
				{
					var projectionID = GetID(cityID, movieID, openHourID);
					var projection = GetProjectionByID(projectionID);
					projection.IsDeleted = false;
					this.unitOfWork.SaveChanges();
					return;
				}
				throw new EntityAlreadyExistsException($"Projection with cityId:{cityID}," +
				$" movieId:{movieID}, openHourId:{openHourID} and date:{date} " +
				$"is already present in the database.");
			}
			else
			{
				Projection projection = new Projection
				{
					MovieId = movieID,
					CityId = cityID,
					OpenHourId = openHourID,
					Date = date
				};
				this.unitOfWork.Projections.Add(projection);
				this.unitOfWork.SaveChanges();
			}
		}

		public int GetID(int cityID, int movieID, int openHourID)
		{
			var date = GetDate(movieID, cityID, openHourID);

			if (!IfExist(cityID, movieID, openHourID, date) || IsDeleted(cityID, movieID, openHourID, date))
			{
				throw new EntityDoesntExistException($"\nProjection is not present in the database.");
			}
			var id = this.unitOfWork.Projections.All()
				.Where(pr => pr.CityId == cityID)
				.Where(pr => pr.MovieId == movieID)
				.Where(pr => pr.OpenHourId == openHourID)
				.Select(pr => pr.Id)
				.FirstOrDefault();
			return id;
		}

		public Projection GetProjectionByID(int id)
		{
			var projection = this.unitOfWork.Projections.All()
				 .Where(proj => proj.Id == id)
				 .Select(proj => proj)
				 .FirstOrDefault();

			return projection;
		}

		public DateTime GetDate(int movieID, int cityID, int openHourID)
		{
			if (!IfExist(movieID, cityID, openHourID) || IsDeleted(movieID, cityID, openHourID))
			{
				throw new EntityDoesntExistException("\nProjection is not present in the database.");
			}
			var date = this.unitOfWork.Projections.AllAndDeleted()
				.Where(p => p.MovieId == movieID
				&& p.CityId == cityID
				&& p.OpenHourId == openHourID)
				.Select(p => p.Date)
				.FirstOrDefault();
			return date;
		}

		public void Delete(int movieID, int cityID, int openHourID, DateTime date)
		{

			if (!IfExist(movieID, cityID, openHourID, date) || IsDeleted(movieID, cityID, openHourID, date))
			{
				throw new EntityDoesntExistException("\nProjection is not present in the database.");
			}
			else if (IfExist(movieID, cityID, openHourID, date) && !IsDeleted(movieID, cityID, openHourID, date))
			{
				var projectionObject = this.unitOfWork.Projections.All()
					.Where(pr => pr.MovieId == movieID
					&& pr.CityId == cityID
					&& pr.OpenHourId == openHourID
					&& CompareDates(pr.Date, date))
					.FirstOrDefault();
				this.unitOfWork.Projections.Delete(projectionObject);
			}
			this.unitOfWork.Cities.Save();
		}

		private bool IfExist(int movieID, int cityID, int openHourID, DateTime date)
		{
				return this.unitOfWork.Projections.AllAndDeleted()
					.Where(pr => pr.MovieId == movieID
						&& pr.CityId == cityID
						&& pr.OpenHourId == openHourID
						&& CompareDates(pr.Date, date))
					.FirstOrDefault() == null ? false : true;
		}

		private Projection MyProjection(int movieID, int cityID, int openHourID)
		{
			return this.unitOfWork.Projections.AllAndDeleted()
				.Where(pr => pr.MovieId == movieID && pr.CityId == cityID && pr.OpenHourId == openHourID)
				.FirstOrDefault();
		}

		private bool IfExist(int movieID, int cityID, int openHourID)
		{
			return this.unitOfWork.Projections.AllAndDeleted()
				.Where(pr => pr.MovieId == movieID)
				.Where(pr => pr.CityId == cityID)
				.Where(pr => pr.OpenHourId == openHourID)
				.FirstOrDefault() == null ? false : true;
		}

		private bool IsDeleted(int movieID, int cityID, int openHourID, DateTime date)
		{
			return this.unitOfWork.Projections.AllAndDeleted()
				.Where(pr => pr.MovieId == movieID
					&& pr.CityId == cityID
					&& pr.OpenHourId == openHourID
					&& CompareDates(pr.Date, date))
				.FirstOrDefault()
				.IsDeleted;
		}

		private bool IsDeleted(int movieID, int cityID, int openHourID)
		{
			return this.unitOfWork.Projections.AllAndDeleted()
				.Where(pr => pr.MovieId == movieID
					&& pr.CityId == cityID
					&& pr.OpenHourId == openHourID)
				.FirstOrDefault()
				.IsDeleted;
		}

		private bool CompareDates(DateTime firstDate, DateTime secondDate)
		{
			return firstDate.Year == secondDate.Year
				&& firstDate.Day == secondDate.Day
				&& firstDate.Month == secondDate.Month;
		}

		public List<string> GetOpenHoursByMovieIDCityID(int movieID, int cityID)
		{
			var openHours = this.unitOfWork.Projections.All()
			.Where(movie => movie.MovieId == movieID)
			.Where(city => city.CityId == cityID)
			.Select(openHour => openHour.OpenHour.StartHour).ToList();

			return openHours;
		}

	}
}