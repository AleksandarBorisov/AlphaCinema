using AlphaCinemaData.Models.Associative;
using AlphaCinemaData.Repository;
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
            Projection projection = new Projection
            {
                MovieId = movieID,
                CityId = cityID,
                OpenHourId = openHourID,
                Date = date
            };

            //Check if the projection already exist
            if(IfExist(projection) != null)
            {
                //Check if the projection is deleted and if it is - set it to NOT deleted
                if(IsDeleted(projection))
                {
                    var genre = this.unitOfWork.Projections.All()
                    .Where(pr => pr.MovieId == projection.MovieId &&
                    pr.CityId == projection.CityId &&
                    pr.OpenHourId == projection.OpenHourId &&
                    pr.Date == projection.Date)
                    .FirstOrDefault()
                    .IsDeleted = false;

                    this.unitOfWork.SaveChanges();
                    return;
                }

                throw new EntityAlreadyExistsException($"Projection with cityId:{cityID}," +
                    $" movieId:{movieID}, openHourId:{openHourID} and date:{date} " +
                    $"is already present in the database.");
            }

            if (IfExist(projection) != null && !IsDeleted(projection))
            {
                throw new EntityAlreadyExistsException($"Projection with cityId:{cityID}," +
                $" movieId:{movieID}, openHourId:{openHourID} and date:{date} " +
                $"is already present in the database.");
            }
            else
            {
                this.unitOfWork.Projections.Add(projection);
                this.unitOfWork.SaveChanges();
            }

        }

        public string GetID(string cityID, string movieID, string openHourID)
		{
			var id = this.unitOfWork.Projections.All()
				.Where(pr => pr.CityId.ToString() == cityID
				&& pr.MovieId.ToString() == movieID
				&& pr.OpenHourId.ToString() == openHourID)
				.Select(pr => pr.Id).FirstOrDefault();

			return id.ToString();
		}

		public List<string> GetProjections()
		{
			var projections = this.unitOfWork.Projections.All()
				.Select(pr => pr.Id.ToString())
				.ToList();

			return projections;
		}
	}
}
