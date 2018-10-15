using AlphaCinemaData.Models;
using AlphaCinemaData.Repository;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinemaTests.AlphaCinemaServices.CityServicesTests
{
    [TestClass]
    public class GetCityNames_Should
    {
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IRepository<City>> cityRepoMock;
        private Mock<City> firstCityMock;
        private string firstCityName = "TestCityOne";
        private Mock<City> secondCityMock;
        private string secondCityName = "TestCityTwo";

        [TestInitialize]
        public void TestInitialize()//Този метод се изпълнява преди извикване на всеки един от другите методи в този клас
        {
            unitOfWork = new Mock<IUnitOfWork>();
            cityRepoMock = new Mock<IRepository<City>>();
            firstCityMock = new Mock<City>();
            firstCityMock.Object.Name = this.firstCityName;
            secondCityMock = new Mock<City>();
            secondCityMock.Object.Name = this.secondCityName;
        }

        [TestMethod]
        public void ReturnCollectionOfCities()
        {
            //Arrange
            var resultFromCityRepo = new List<City>() { firstCityMock.Object, secondCityMock.Object };
            var expectedList = new List<string>() { "TestCityOne", "TestCityTwo" };

            unitOfWork.Setup(x => x.Cities).Returns(cityRepoMock.Object);
            cityRepoMock.Setup(repo => repo.All()).Returns(resultFromCityRepo.AsQueryable());

            //Act 
            var cityService = new CityServices(unitOfWork.Object);
            var result = cityService.GetCityNames();

            //Assert
            CollectionAssert.AreEquivalent(expectedList, result.ToList());
        }
    }
}
