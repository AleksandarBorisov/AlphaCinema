using AlphaCinema.Core.Commands.BasicCommands;
using AlphaCinema.Core.Contracts;
using AlphaCinemaData.ViewModels;
using AlphaCinemaServices.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaCinemaTests.AlphaCinema.Core.Commands.BasicCommandsTests.MoviesByAgeTests
{
    [TestClass]
    public class MoviesByAge_Execute_Should
    {
        [TestMethod]
        [DataRow("Menu BuyTickets LogAsAdmin ShowInfo PdfExport Exit 1 5",
            "20 | 40",
            "BuyTickets")]

        public void ReturnCorrectList_WhenArgumentsArePassed(string inputParameters, string inputResult, string expected)
        {
            //Arrange
            var parameters = inputParameters.Split().ToList();

            var userServicesMock = new Mock<IUserServices>();
            var itemSelectorMock = new Mock<IItemSelector>();
            var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();

            var model = new ProjectionDetailsViewModel()
            {
                CityName = "Sofia",
                MovieName = "Titanic",
                Hour = "19:30h"
            };

            //Setup
            itemSelectorMock.Setup(selector => selector
                    .ReadAtPosition(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
                    .Returns(inputResult);

            userServicesMock.Setup(userServices => userServices
                    .GetMoviesByUserAge(It.IsAny<int>(), It.IsAny<int>()))
                    .Returns(new List<ProjectionDetailsViewModel>() { model });

            cinemaConsoleMock.Setup(console => console
                    .ReadKey(It.IsAny<bool>()));

            //Act
            var moviesByAge = new MoviesByAge(itemSelectorMock.Object, cinemaConsoleMock.Object,
                userServicesMock.Object);
            var result = moviesByAge.Execute(parameters);

            //Assert
            Assert.AreEqual(expected, result.First());

        }

        [TestMethod]
        [DataRow("Menu BuyTickets LogAsAdmin ShowInfo PdfExport Exit 1 5", "20", "Menu")]
        public void ArgumentException_WhenInvalidCountOfArgumentsArePassed_AndIsSelectedRetry(string inputParameters, string inputResult,string expected)
        {
            //Arrange
            var parameters = inputParameters.Split().ToList();

            var userServicesMock = new Mock<IUserServices>();
            var itemSelectorMock = new Mock<IItemSelector>();
            var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();

            var model = new ProjectionDetailsViewModel()
            {
                CityName = "Sofia",
                MovieName = "Titanic",
                Hour = "19:30h"
            };

            //Setup
            itemSelectorMock.Setup(selector => selector
                    .ReadAtPosition(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
                    .Returns(inputResult);

            itemSelectorMock.Setup(selector => selector
                    .DisplayItems(It.IsAny<List<string>>()))
                    .Returns("Retry");
            
            //Act
            var moviesByAge = new MoviesByAge(itemSelectorMock.Object, cinemaConsoleMock.Object,
                userServicesMock.Object);
            var result = moviesByAge.Execute(parameters);

            //Assert
            Assert.AreEqual(expected, result.First());

        }

        [TestMethod]
        [DataRow("Menu BuyTickets LogAsAdmin ShowInfo PdfExport Exit 1 5", "20", "BuyTickets")]
        public void ArgumentException_WhenInvalidCountOfArgumentsArePassed_AndIsSelectedBack(string inputParameters, string inputResult, string expected)
        {
            //Arrange
            var parameters = inputParameters.Split().ToList();

            var userServicesMock = new Mock<IUserServices>();
            var itemSelectorMock = new Mock<IItemSelector>();
            var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();

            var model = new ProjectionDetailsViewModel()
            {
                CityName = "Sofia",
                MovieName = "Titanic",
                Hour = "19:30h"
            };

            //Setup
            itemSelectorMock.Setup(selector => selector
                    .ReadAtPosition(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
                    .Returns(inputResult);

            itemSelectorMock.Setup(selector => selector
                    .DisplayItems(It.IsAny<List<string>>()))
                    .Returns("Back");

            //Act
            var moviesByAge = new MoviesByAge(itemSelectorMock.Object, cinemaConsoleMock.Object,
                userServicesMock.Object);
            var result = moviesByAge.Execute(parameters);

            //Assert
            Assert.AreEqual(expected, result.First());

        }

        [TestMethod]
        [DataRow("Menu BuyTickets LogAsAdmin ShowInfo PdfExport Exit 1 5", "20", "LogAsAdmin")]
        public void ArgumentException_WhenInvalidCountOfArgumentsArePassed_AndIsSelectedHome(string inputParameters, string inputResult, string expected)
        {
            //Arrange
            var parameters = inputParameters.Split().ToList();

            var userServicesMock = new Mock<IUserServices>();
            var itemSelectorMock = new Mock<IItemSelector>();
            var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();

            var model = new ProjectionDetailsViewModel()
            {
                CityName = "Sofia",
                MovieName = "Titanic",
                Hour = "19:30h"
            };

            //Setup
            itemSelectorMock.Setup(selector => selector
                    .ReadAtPosition(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
                    .Returns(inputResult);

            itemSelectorMock.Setup(selector => selector
                    .DisplayItems(It.IsAny<List<string>>()))
                    .Returns("Home");

            //Act
            var moviesByAge = new MoviesByAge(itemSelectorMock.Object, cinemaConsoleMock.Object,
                userServicesMock.Object);
            var result = moviesByAge.Execute(parameters);

            //Assert
            Assert.AreEqual(expected, result.First());

        }

        [TestMethod]
        [DataRow("Menu BuyTickets LogAsAdmin ShowInfo PdfExport Exit 1 5", "20 | text", "Menu")]
        public void ArgumentException_WhenInvalidArgumentsArePassed_AndIsSelectedRetry(string inputParameters, string inputResult, string expected)
        {
            //Arrange
            var parameters = inputParameters.Split().ToList();

            var userServicesMock = new Mock<IUserServices>();
            var itemSelectorMock = new Mock<IItemSelector>();
            var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();

            var model = new ProjectionDetailsViewModel()
            {
                CityName = "Sofia",
                MovieName = "Titanic",
                Hour = "19:30h"
            };

            //Setup
            itemSelectorMock.Setup(selector => selector
                    .ReadAtPosition(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
                    .Returns(inputResult);

            itemSelectorMock.Setup(selector => selector
                    .DisplayItems(It.IsAny<List<string>>()))
                    .Returns("Retry");

            //Act
            var moviesByAge = new MoviesByAge(itemSelectorMock.Object, cinemaConsoleMock.Object,
                userServicesMock.Object);
            var result = moviesByAge.Execute(parameters);

            //Assert
            Assert.AreEqual(expected, result.First());

        }

        [TestMethod]
        [DataRow("Menu BuyTickets LogAsAdmin ShowInfo PdfExport Exit 1 5", "20 | text", "BuyTickets")]
        public void ArgumentException_WhenInvalidArgumentsArePassed_AndIsSelectedBack(string inputParameters, string inputResult, string expected)
        {
            //Arrange
            var parameters = inputParameters.Split().ToList();

            var userServicesMock = new Mock<IUserServices>();
            var itemSelectorMock = new Mock<IItemSelector>();
            var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();

            var model = new ProjectionDetailsViewModel()
            {
                CityName = "Sofia",
                MovieName = "Titanic",
                Hour = "19:30h"
            };

            //Setup
            itemSelectorMock.Setup(selector => selector
                    .ReadAtPosition(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
                    .Returns(inputResult);

            itemSelectorMock.Setup(selector => selector
                    .DisplayItems(It.IsAny<List<string>>()))
                    .Returns("Back");

            //Act
            var moviesByAge = new MoviesByAge(itemSelectorMock.Object, cinemaConsoleMock.Object,
                userServicesMock.Object);
            var result = moviesByAge.Execute(parameters);

            //Assert
            Assert.AreEqual(expected, result.First());

        }

        [TestMethod]
        [DataRow("Menu BuyTickets LogAsAdmin ShowInfo PdfExport Exit 1 5", "20 | text", "LogAsAdmin")]
        public void ArgumentException_WhenInvalidArgumentsArePassed_AndIsSelectedHome(string inputParameters, string inputResult, string expected)
        {
            //Arrange
            var parameters = inputParameters.Split().ToList();

            var userServicesMock = new Mock<IUserServices>();
            var itemSelectorMock = new Mock<IItemSelector>();
            var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();

            var model = new ProjectionDetailsViewModel()
            {
                CityName = "Sofia",
                MovieName = "Titanic",
                Hour = "19:30h"
            };

            //Setup
            itemSelectorMock.Setup(selector => selector
                    .ReadAtPosition(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
                    .Returns(inputResult);

            itemSelectorMock.Setup(selector => selector
                    .DisplayItems(It.IsAny<List<string>>()))
                    .Returns("Home");

            //Act
            var moviesByAge = new MoviesByAge(itemSelectorMock.Object, cinemaConsoleMock.Object,
                userServicesMock.Object);
            var result = moviesByAge.Execute(parameters);

            //Assert
            Assert.AreEqual(expected, result.First());
        }

    }
}