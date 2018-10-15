using AlphaCinemaData.Models;
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
    public class DeleteCity_Should
    {
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IRepository<City>> cityRepoMock;
        private string testCityName = "TestCity";
        private Mock<City> cityMock;
        private List<City> predifinedListOfCities;

        [TestInitialize]
        public void TestInitialize()//Този метод се изпълнява преди извикване на всеки един от другите методи в този клас
        {
            //Arrange
            cityMock = new Mock<City>();
            unitOfWork = new Mock<IUnitOfWork>();
            cityRepoMock = new Mock<IRepository<City>>();
            predifinedListOfCities = new List<City>() { cityMock.Object };
        }

        [TestMethod]
        public void ThrowEntityDoesntExistException_WhenCityDoesNotExist()
        {
            //Arrange
            unitOfWork.Setup(x => x.Cities).Returns(cityRepoMock.Object);

            //Act and Assert
            var command = new CityServices(unitOfWork.Object);
            Assert.ThrowsException<EntityDoesntExistException>(() => command.DeleteCity(testCityName));
        }

        [TestMethod]
        public void ThrowEntityDoesntExistException_WhenCityIsDeleted()
        {
            //Arrange
            cityMock.Object.IsDeleted = true;

            unitOfWork.Setup(x => x.Cities).Returns(cityRepoMock.Object);
            cityRepoMock.Setup(repo => repo.AllAndDeleted()).Returns(predifinedListOfCities.AsQueryable());

            //Act and Assert
            var command = new CityServices(unitOfWork.Object);
            Assert.ThrowsException<EntityDoesntExistException>(() => command.DeleteCity(testCityName));
        }

        [TestMethod]
        public void DeleteCity_WhenCityExists()
        {
            //Arrange
            cityMock.Object.IsDeleted = false;

            cityRepoMock
                .Setup(repo => repo.Delete(It.IsAny<City>()))
                .Callback<City>((city) =>
                {
                    predifinedListOfCities.Remove(cityMock.Object);
                });

            unitOfWork.Setup(x => x.Cities).Returns(cityRepoMock.Object);
            cityRepoMock.Setup(repo => repo.AllAndDeleted()).Returns(predifinedListOfCities.AsQueryable());

            //Act
            var command = new CityServices(unitOfWork.Object);
            command.DeleteCity(cityMock.Object.Name);

            //Assert
            Assert.AreEqual(0, predifinedListOfCities.Count);
        }
    }
}
