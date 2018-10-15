using AlphaCinemaData.Models;
using AlphaCinemaData.Models.Associative;
using AlphaCinemaData.Repository;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices;
using AlphaCinemaServices.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinemaTests.AlphaCinemaServices.CityServicesTests
{
    [TestClass]
    public class GetID_Should
    {
        private City city;
        private List<City> resultFromCityRepo;
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IRepository<City>> cityRepoMock;
        private string testCityName = "TestCity";

        [TestInitialize]
        public void TestInitialize()//Този метод се изпълнява преди извикване на всеки един от другите методи в този клас
        {
            //Arrange 
            city = new City
            {
                Id = 1,
                Name = testCityName,
                Projections = new List<Projection>(),
                IsDeleted = false
            };
            resultFromCityRepo = new List<City>() { city };
            unitOfWork = new Mock<IUnitOfWork>();
            cityRepoMock = new Mock<IRepository<City>>();
        }

        [TestMethod]
        public void ReturnCorrectCityId_WhenCityExists()
        {
            //Arrange 
            unitOfWork.Setup(x => x.Cities).Returns(cityRepoMock.Object);
            cityRepoMock.Setup(repo => repo.AllAndDeleted()).Returns(resultFromCityRepo.AsQueryable());

            //Act 
            var cityService = new CityServices(unitOfWork.Object);
            var result = cityService.GetID(city.Name);

            //Assert
            Assert.AreEqual(city.Id, result);
        }

        [TestMethod]
        public void ThrowEntityDoesNotExistException_WhenCityDoesNotExist()
        {
            //Act 
            var cityService = new CityServices(unitOfWork.Object);

            unitOfWork.Setup(x => x.Cities).Returns(cityRepoMock.Object);
            cityRepoMock.Setup(repo => repo.AllAndDeleted()).Returns(resultFromCityRepo.AsQueryable());

            //Assert
            Assert.ThrowsException<EntityDoesntExistException>(() => cityService.GetID("NonExistingCity"));
        }

        [TestMethod]
        public void ThrowEntityDoesNotExistException_WhenCityIsDeleted()
        {
            //Arrange 
            city.IsDeleted = true;
            unitOfWork.Setup(x => x.Cities).Returns(cityRepoMock.Object);
            cityRepoMock.Setup(repo => repo.AllAndDeleted()).Returns(resultFromCityRepo.AsQueryable());

            //Act 
            var cityService = new CityServices(unitOfWork.Object);

            //Assert
            Assert.ThrowsException<EntityDoesntExistException>(() => cityService.GetID(city.Name));
        }
    }
}
