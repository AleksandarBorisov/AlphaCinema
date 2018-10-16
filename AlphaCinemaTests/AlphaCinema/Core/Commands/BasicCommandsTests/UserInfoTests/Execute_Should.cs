using AlphaCinema.Core.Commands.BasicCommands;
using AlphaCinema.Core.Contracts;
using AlphaCinemaData.Models;
using AlphaCinemaData.ViewModels;
using AlphaCinemaServices.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaCinemaTests.AlphaCinema.Core.Commands.BasicCommandsTests.UserInfoTests
{
	[TestClass]
	public class Execute_Should
	{
		[TestMethod]
		[DataRow("Krasi|AdminMenu|21|7|3", "AdminMenu",
			"Krasi|21",
			"AdminMenu")]
		public void ReturnCorrectList_WhenALinkIsRemoved(string inputParameters,
				string selectorResult, string readFromSelector, string expected)
		{
			// Arrange
			var parameters = inputParameters.Split('|');
			var name = parameters[0];
			var age = int.Parse(parameters[2]);

			var projections = new ProjectionDetailsViewModel
			{
				CityName = "TestCity",
				MovieName = "TestMovie",
				Hour = "TestHour"
			};

			var selectorMock = new Mock<IItemSelector>();
			var projectionServicesMock = new Mock<IProjectionsServices>();
			var userServicesMock = new Mock<IUserServices>();
            var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();

			selectorMock
				.Setup(s => s.DisplayItems(It.IsAny<List<string>>()))
				.Returns(selectorResult);

			selectorMock
				.Setup(s => s.ReadAtPosition(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
				.Returns(readFromSelector);

			userServicesMock
				.Setup(us => us.GetID(name))
				.Returns(1);

			userServicesMock
				.Setup(us => us.GetProjectionsByUserID(1))
				.Returns(new List<ProjectionDetailsViewModel> { projections });

            cinemaConsoleMock
                .Setup(console => console.ReadKey(It.IsAny<bool>()));
            
			// Act
			var sut = new UserInfo(selectorMock.Object, cinemaConsoleMock.Object, userServicesMock.Object,
				projectionServicesMock.Object);
			var result = sut.Execute(parameters);

			// Assert
			Assert.AreEqual(expected, result.First());
		}

		[TestMethod]
		[DataRow("Krasi|AdminMenu|21|7|3", "AdminMenu",
			"Krasi|21",
			"AdminMenu")]
		public void EnsureGetIDMethod_IsCalled(string inputParameters,
				string selectorResult, string readFromSelector, string expected)
		{
			// Arrange
			var parameters = inputParameters.Split('|');
			var name = parameters[0];
			var age = int.Parse(parameters[2]);
			var selectorMock = new Mock<IItemSelector>();
			var userServicesMock = new Mock<IUserServices>();
			var projectionServicesMock = new Mock<IProjectionsServices>();
            var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();


            selectorMock
                .Setup(s => s.DisplayItems(It.IsAny<List<string>>()))
				.Returns(selectorResult);

			selectorMock
				.Setup(s => s.ReadAtPosition(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
				.Returns(readFromSelector);

            cinemaConsoleMock
                .Setup(console => console.ReadKey(It.IsAny<bool>()));


            // Act
            var sut = new UserInfo(selectorMock.Object,cinemaConsoleMock.Object,userServicesMock.Object,
				projectionServicesMock.Object);
			var result = sut.Execute(parameters);

			// Assert
			userServicesMock.Verify(us => us.GetID(It.IsAny<string>()), Times.Once);
		}

		[TestMethod]
		[DataRow("Krasi|AdminMenu|21|7|3", "AdminMenu",
			"Krasi|21",
			"AdminMenu")]
		public void EnsureGetProjectionsByUserIDMethod_IsCalled(string inputParameters,
				string selectorResult, string readFromSelector, string expected)
		{
			// Arrange
			var parameters = inputParameters.Split('|');
			var name = parameters[0];
			var age = int.Parse(parameters[2]);
			var selectorMock = new Mock<IItemSelector>();
			var userServicesMock = new Mock<IUserServices>();
			var projectionServicesMock = new Mock<IProjectionsServices>();
            var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();
            
            selectorMock
                .Setup(s => s.DisplayItems(It.IsAny<List<string>>()))
				.Returns(selectorResult);

			selectorMock
				.Setup(s => s.ReadAtPosition(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
				.Returns(readFromSelector);

			userServicesMock
				.Setup(us => us.GetID(name))
				.Returns(1);

            cinemaConsoleMock
                .Setup(console => console.ReadKey(It.IsAny<bool>()));
            

            // Act
            var sut = new UserInfo(selectorMock.Object,cinemaConsoleMock.Object, userServicesMock.Object,
				projectionServicesMock.Object);
			var result = sut.Execute(parameters);

			// Assert
			userServicesMock.Verify(us => us.GetProjectionsByUserID(It.IsAny<int>()), Times.Once);
		}

		[TestMethod]
		[DataRow("Krasi|AdminMenu|Test|21|7|3", "Home",
			"Krasi|21|test",
			"Test")]
		public void ReturnCorrectList_WhenHomeIsSelected(string inputParameters,
					string selectorResult, string readFromSelector, string expected)
		{
			// Arrange
			var parameters = inputParameters.Split('|');
			var name = parameters[0];
			var age = int.Parse(parameters[3]);
			var selectorMock = new Mock<IItemSelector>();
			var userServicesMock = new Mock<IUserServices>();
			var projectionServicesMock = new Mock<IProjectionsServices>();
            var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();


            selectorMock
                .Setup(s => s.DisplayItems(It.IsAny<List<string>>()))
				.Returns(selectorResult);

			selectorMock
				.Setup(s => s.ReadAtPosition(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
				.Returns(readFromSelector);

            cinemaConsoleMock
                .Setup(console => console.ReadKey(It.IsAny<bool>()));


            // Act
            var sut = new UserInfo(selectorMock.Object,cinemaConsoleMock.Object,userServicesMock.Object,
				projectionServicesMock.Object);
			var result = sut.Execute(parameters);

			// Assert
			Assert.AreEqual(expected, result.First());
		}

		[TestMethod]
		[DataRow("Krasi|AdminMenu|Test|21|7|3", "Retry",
			"Krasi|21|test",
			"Test")]
		public void ReturnCorrectList_WhenRetryIsSelected(string inputParameters,
					string selectorResult, string readFromSelector, string expected)
		{
			// Arrange
			var parameters = inputParameters.Split('|');
			var name = parameters[0];
			var age = int.Parse(parameters[3]);
			var selectorMock = new Mock<IItemSelector>();
			var userServicesMock = new Mock<IUserServices>();
			var projectionServicesMock = new Mock<IProjectionsServices>();
            var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();

            selectorMock
                .Setup(s => s.DisplayItems(It.IsAny<List<string>>()))
				.Returns(selectorResult);

			selectorMock
				.Setup(s => s.ReadAtPosition(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
				.Returns(readFromSelector);

            cinemaConsoleMock
                .Setup(console => console.ReadKey(It.IsAny<bool>()));

            // Act
            var sut = new UserInfo(selectorMock.Object, cinemaConsoleMock.Object, userServicesMock.Object,
				projectionServicesMock.Object);
			var result = sut.Execute(parameters);

			// Assert
			CollectionAssert.AreEqual(parameters, result.ToList());
		}

		[TestMethod]
		[DataRow("Krasi|AdminMenu|Test|21|7|3", "Back",
			"Krasi|21|test",
			"AdminMenu")]
		public void ReturnCorrectList_WhenBackIsSelected(string inputParameters,
					string selectorResult, string readFromSelector, string expected)
		{
			// Arrange
			var parameters = inputParameters.Split('|');
			var name = parameters[0];
			var age = int.Parse(parameters[3]);
			var selectorMock = new Mock<IItemSelector>();
			var userServicesMock = new Mock<IUserServices>();
			var projectionServicesMock = new Mock<IProjectionsServices>();
            var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();

            selectorMock
                .Setup(s => s.DisplayItems(It.IsAny<List<string>>()))
				.Returns(selectorResult);

			selectorMock
				.Setup(s => s.ReadAtPosition(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
				.Returns(readFromSelector);

            cinemaConsoleMock
                .Setup(console => console.ReadKey(It.IsAny<bool>()));
            
            // Act
            var sut = new UserInfo(selectorMock.Object,cinemaConsoleMock.Object, userServicesMock.Object,
				projectionServicesMock.Object);
			var result = sut.Execute(parameters);

			// Assert
			Assert.AreEqual(expected, result.First());
		}
	}
}
