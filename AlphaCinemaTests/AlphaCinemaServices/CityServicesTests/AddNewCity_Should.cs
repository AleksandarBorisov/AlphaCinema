using AlphaCinemaData.Models;
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

namespace AlphaCinemaTests.AlphaCinemaServices.CityServicesTests
{
    [TestClass]
    public class AddNewCity_Should
    {
        private City city;
        private List<City> predifinedListOfCities;
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IRepository<City>> cityRepoMock;
        private int testCityId = 1;
        private string testCityName = "TestCity";

        [TestInitialize]
        public void TestInitialize()//Този метод се изпълнява преди извикване на всеки един от другите методи в този клас
        {
            //Arrange 
            city = new City
            {
                Id = testCityId,
                Name = testCityName,
                Projections = new List<Projection>(),
                IsDeleted = false
            };
            predifinedListOfCities = new List<City>() { city };
            unitOfWork = new Mock<IUnitOfWork>();
            cityRepoMock = new Mock<IRepository<City>>();
        }

        [TestMethod]
        public void CallAddMethodOnCities_WhenParametersAreCorrect()
        {
            //Arrange
            var cityMock = new Mock<City>();

            unitOfWork.Setup(x => x.Cities).Returns(cityRepoMock.Object);

            cityRepoMock
                .Setup(repo => repo.Add(It.IsAny<City>()))
                .Callback<City>((city) =>
                {
                    predifinedListOfCities.Add(cityMock.Object);
                });

            //Act
            var addCityCommand = new CityServices(unitOfWork.Object);
            addCityCommand.AddNewCity(testCityName);

            //Assert
            Assert.AreEqual(2, predifinedListOfCities.Count);
        }

        [TestMethod]
        public void ThrowArgumentException_WhenCityNameLengthIsNotInRange()
        {
            //Arrange
            int maxCityNameLength = 50;
            var longCityName = new string('c', maxCityNameLength + 1);

            var cityService = new CityServices(unitOfWork.Object);

            //Act and Assert
            Assert.ThrowsException<ArgumentException>(() => cityService.AddNewCity(longCityName));
        }

        [TestMethod]
        public void UnmarkCityAsDeleted_WhenCityExistButIsDeleted()
        {
            //Arrange 
            city.IsDeleted = true;

            unitOfWork.Setup(x => x.Cities).Returns(cityRepoMock.Object);
            cityRepoMock.Setup(repo => repo.AllAndDeleted()).Returns(predifinedListOfCities.AsQueryable());

            //Act
            var addCityCommand = new CityServices(unitOfWork.Object);
            addCityCommand.AddNewCity(testCityName);

            //Assert
            Assert.IsFalse(city.IsDeleted);
        }

        [TestMethod]
        public void ThrowEntityAlreadyExistsException_WhenCityExist()
        {
            //Arrange 
            unitOfWork.Setup(x => x.Cities).Returns(cityRepoMock.Object);
            cityRepoMock.Setup(repo => repo.AllAndDeleted()).Returns(predifinedListOfCities.AsQueryable());

            //Act and Assert
            var addCityCommand = new CityServices(unitOfWork.Object);
            Assert.ThrowsException<EntityAlreadyExistsException>(() => addCityCommand.AddNewCity(testCityName));
        }
    }
}
