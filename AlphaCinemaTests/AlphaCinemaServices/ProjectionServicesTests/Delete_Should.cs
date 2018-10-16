using AlphaCinemaData.Models.Associative;
using AlphaCinemaData.Repository;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices;
using AlphaCinemaServices.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaCinemaTests.AlphaCinemaServices.ProjectionServicesTests
{
	[TestClass]
	public class Delete_Should
	{
		private Projection projection;
		private List<Projection> resultFromProjectionRepo;
		private Mock<IUnitOfWork> unitOfWork;
		private Mock<IRepository<Projection>> projectionRepoMock;
		private int Id = 1;
		private int movieId = 1;
		private int cityId = 1;
		private int openHourId = 1;
		private bool IsDeleted;


		[TestInitialize]
		public void TestInitialize()
		{
			projection = new Projection
			{
				Id = this.Id,
				MovieId = this.movieId,
				CityId = this.cityId,
				OpenHourId = this.openHourId,
				WatchedMovies = new List<WatchedMovie>(),
				IsDeleted = false
			};
			resultFromProjectionRepo = new List<Projection>() { projection };
			unitOfWork = new Mock<IUnitOfWork>();
			projectionRepoMock = new Mock<IRepository<Projection>>();
		}

		[TestMethod]
		public void ThrowEntityDoesNotExistException_WhenProjectionIsDeleted()
		{
			//Arrange 
			projection.IsDeleted = true;
			unitOfWork.Setup(x => x.Projections)
				.Returns(projectionRepoMock.Object);

			projectionRepoMock.Setup(repo => repo.AllAndDeleted())
				.Returns(resultFromProjectionRepo.AsQueryable());

			//Act 
			var sut = new ProjectionServices(unitOfWork.Object);

			//Assert
			Assert.ThrowsException<EntityDoesntExistException>(() => sut.Delete(
				projection.CityId, projection.MovieId, projection.OpenHourId));
		}

		[TestMethod]
		public void ThrowEntityDoesNotExistException_WhenProjectionIsNull()
		{
			//Arrange 
			unitOfWork.Setup(x => x.Projections)
				.Returns(projectionRepoMock.Object);

			projectionRepoMock.Setup(repo => repo.AllAndDeleted())
				.Returns(resultFromProjectionRepo.AsQueryable());

			//Act 
			var sut = new ProjectionServices(unitOfWork.Object);

			//Assert
			Assert.ThrowsException<EntityDoesntExistException>(() => sut.Delete(
				projection.CityId, 5, projection.OpenHourId));
		}

		[TestMethod]
		public void CorrectlyCalledDeleteProjectionRepoMethod_WhenProjectionDoesntExists()
		{
			//Arrange 
			unitOfWork.Setup(x => x.Projections)
				.Returns(projectionRepoMock.Object);

			projectionRepoMock.Setup(repo => repo.AllAndDeleted())
				.Returns(resultFromProjectionRepo.AsQueryable());

			//Act 
			var sut = new ProjectionServices(unitOfWork.Object);
			sut.Delete(this.cityId, this.movieId, this.openHourId);

			//Assert
			projectionRepoMock.Verify(p => p.Delete(projection), Times.Once);
		}

	}
}
