using AlphaCinemaData.Models;
using AlphaCinemaData.Models.Associative;
using AlphaCinemaData.Repository;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinemaTests.AlphaCinemaServices.ProjectionServicesTests
{
	[TestClass]
	public class GetOpenHoursByMovieIDCityID
	{
		private Projection projection;
		private List<Projection> resultFromProjectionRepo;
		private Mock<IUnitOfWork> unitOfWork;
		private Mock<IRepository<Projection>> projectionRepoMock;
		private int Id = 1;
		private int movieId = 1;
		private int cityId = 1;
		private int openHourId = 1;
		private Mock<Movie> movie;
		private Mock<City> city;
		private Mock<OpenHour> openHour;
		private bool IsDeleted;


		[TestInitialize]
		public void TestInitialize()
		{
			movie = new Mock<Movie>();
			movie
				.Setup(m => m.Id)
				.Returns(1);

			city = new Mock<City>();
			city
				.Setup(c => c.Id)
				.Returns(1);

			openHour = new Mock<OpenHour>();
			openHour
				.Setup(oh => oh.Id)
				.Returns(1);

			openHour
				.Setup(oh => oh.StartHour)
				.Returns("19:30h");

			projection = new Projection
			{
				Id = this.Id,
				MovieId = this.movieId,
				CityId = this.cityId,
				OpenHourId = this.openHourId,
				WatchedMovies = new List<WatchedMovie>(),
				IsDeleted = false,
				Movie = movie.Object,
				City = city.Object,
				OpenHour = openHour.Object
			};
			resultFromProjectionRepo = new List<Projection>() { projection };
			unitOfWork = new Mock<IUnitOfWork>();
			projectionRepoMock = new Mock<IRepository<Projection>>();
		}

		[TestMethod]
		public void CorrectlyReturnStartHoursCollection_WhenParametersAreValid()
		{
			//Arrange 
			unitOfWork.Setup(x => x.Projections)
				.Returns(projectionRepoMock.Object);

			projectionRepoMock.Setup(repo => repo.All())
				.Returns(resultFromProjectionRepo.AsQueryable());

			//Act 
			var sut = new ProjectionServices(unitOfWork.Object);
			var result = sut.GetOpenHoursByMovieIDCityID(this.movieId, this.cityId);
			var expected = new List<string>() { projection.OpenHour.StartHour };
			//Assert
			CollectionAssert.AreEqual(expected, result.ToList());
		}
	}
}
