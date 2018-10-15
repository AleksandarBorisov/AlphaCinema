using AlphaCinema.Core.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlphaCinema.Core.Commands.BasicCommands;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlphaCinemaServices.Contracts;

namespace AlphaCinemaTests.AlphaCinema.Core.Commands.BasicCommandsTests.RemoveCityTests
{
    [TestClass]
    public class RemoveCity_Execute_Should
    {
        [TestMethod]
        [DataRow("Sofia", "Sofia AdminMenu Action", "AdminMenu")]
        public void ReturnCorrectList_WhenACityIsRemoved(string input, string inputParameters, string expected)
        {
            // Arrange
            var parameters = inputParameters.Split().ToList();

            var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();
            var cityServicesMock = new Mock<ICityServices>();

            cinemaConsoleMock
                .Setup(c => c.ReadLineMiddle(It.IsAny<int>(), It.IsAny<int>()))
                .Returns("Sofia");
            
            // Act
            var sut = new RemoveCity(cityServicesMock.Object,cinemaConsoleMock.Object);
            var result = sut.Execute(parameters);

            //Assert
            Assert.AreEqual(expected, result.First());
        }

        [TestMethod]
        [DataRow("Sofia", "Sofia AdminMenu Action")]
        public void CityServicesDeleteMethod_IsCalled(string input, string inputParameters)
        {
            // Arrange
            var parameters = inputParameters.Split().ToList();

            var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();
            var cityServicesMock = new Mock<ICityServices>();

            cinemaConsoleMock
                .Setup(c => c.ReadLineMiddle(It.IsAny<int>(), It.IsAny<int>()))
                .Returns("Sofia");
            
            // Act
            var sut = new RemoveCity(cityServicesMock.Object, cinemaConsoleMock.Object);
            var result = sut.Execute(parameters);

            //Assert
            cityServicesMock.Verify(services => services.DeleteCity(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [DataRow("Sofia", "Sofia AdminMenu Action","AdminMenu")]
        public void InvalidClientInputException_WhenParameterIsInvalid(string input, string inputParameters,string expected)
        {
            // Arrange
            var parameters = inputParameters.Split().ToList();

            var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();
            var cityServicesMock = new Mock<ICityServices>();

            cinemaConsoleMock
                .Setup(c => c.ReadLineMiddle(It.IsAny<int>(), It.IsAny<int>()))
                .Returns("Sofia12");
            
            // Act
            var sut = new RemoveCity(cityServicesMock.Object, cinemaConsoleMock.Object);
            var result = sut.Execute(parameters);

            //Assert
            Assert.AreEqual(expected, result.First());
        }

    }
}
