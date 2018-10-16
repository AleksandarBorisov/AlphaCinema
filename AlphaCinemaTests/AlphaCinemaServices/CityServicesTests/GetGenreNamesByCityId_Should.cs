using AlphaCinemaData.Models;
using AlphaCinemaData.Repository;
using AlphaCinemaData.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace AlphaCinemaTests.AlphaCinemaServices.CityServicesTests
{
	[TestClass]
	public class GetGenreNamesByCityId_Should
	{
		private Mock<IUnitOfWork> unitOfWork;
		private List<City> predifinedListOfCityNames;
		private Mock<IRepository<City>> cityRepoMock;
		private string testCityName = "TestCityOne";
		private City testCity;

		[TestInitialize]
		public void TestInitialize()//Този метод се изпълнява преди извикване на всеки един от другите методи в този клас
		{
			unitOfWork = new Mock<IUnitOfWork>();
			cityRepoMock = new Mock<IRepository<City>>();
			testCity = new City
			{
				Name = testCityName,

			};
			//TODO GetGenreNamesByCityID Test
		}

		[TestMethod]
		public void ReturnCollectionOfGenreNames_WhenTheyExist()
		{

		}
	}
}
