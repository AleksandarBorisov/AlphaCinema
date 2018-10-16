using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using AlphaCinemaData.Models.Associative;
using AlphaCinemaData.Repository;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinemaTests.AlphaCinemaData.RepositoryTests
{
	[TestClass]
	public class Delete_Should
	{
		[TestMethod]
		public void CityServices_ShouldDeleteCity_WhenPassedValidParameter()
		{
			// Arrange
			var contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
				.UseInMemoryDatabase(databaseName: "CityServices_ShouldDeleteCity_WhenPassedValidParameter")
				.Options;

			City city = new City()
			{
				Name = "TestName",
				Id = 1,
				IsDeleted = false,
				Projections = new List<Projection>()
			};

			var unitOfWorkMock = new Mock<IUnitOfWork>();
			

			// Act
			using (var actContext = new AlphaCinemaContext(contextOptions))
			{
				var cityServicesRepo = new Repository<City>(actContext);
				unitOfWorkMock
				.Setup(u => u.Cities)
				.Returns(cityServicesRepo);

				var sut = new CityServices(unitOfWorkMock.Object);

				sut.AddNewCity(city.Name); // first add a city
				sut.DeleteCity(city.Name);
			}

			// Assert
			using (var assertContext = new AlphaCinemaContext(contextOptions))
			{
				Assert.IsTrue(assertContext.Cities.Count() == 1);
				Assert.AreEqual(assertContext.Cities.FirstOrDefault().IsDeleted, true);
			}
		}
	}
}
