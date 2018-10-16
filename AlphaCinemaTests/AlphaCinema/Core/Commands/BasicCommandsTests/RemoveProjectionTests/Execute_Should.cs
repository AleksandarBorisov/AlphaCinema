using AlphaCinema.Core.Commands.BasicCommands;
using AlphaCinema.Core.Contracts;
using AlphaCinemaServices.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaCinemaTests.AlphaCinema.Core.Commands.BasicCommandsTests.RemoveProjectionTests
{
	[TestClass]
	public class Execute_Should
	{
		[TestMethod]
		[DataRow("Venom|AdminMenu|Sofia|19:30h", "Venom|Sofia|19:30h", "AdminMenu")]
		public void ReturnCorrectList_WhenALinkIsRemoved(string inputParameters, string consoleInput, string expected)
		{
			// Arrange
			var parameters = inputParameters.Split('|').ToList();

			var cityServicesMock = new Mock<ICityServices>();
			var movieServicesMock = new Mock<IMovieServices>();
			var openHourServicesMock = new Mock<IOpenHourServices>();
			var projectionServicesMock = new Mock<IProjectionsServices>();
			var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();

			cityServicesMock
				.Setup(cs => cs.GetID(parameters[1]))
				.Returns(1);

			movieServicesMock
				.Setup(ms => ms.GetID(parameters[0]))
				.Returns(1);

			openHourServicesMock
				.Setup(gs => gs.GetID(parameters[2]))
				.Returns(1);

			cinemaConsoleMock
				.Setup(c => c.ReadLineMiddle(It.IsAny<int>(), It.IsAny<int>()))
				.Returns(consoleInput);

			// Act
			var sut = new RemoveProjection(cinemaConsoleMock.Object, movieServicesMock.Object,
				cityServicesMock.Object, openHourServicesMock.Object, projectionServicesMock.Object);
			var result = sut.Execute(parameters);

			// Assert
			Assert.AreEqual(expected, result.First());
		}

		[TestMethod]
		[DataRow("Venom|AdminMenu|Sofia|19:30h", "Venom|Sofia|19:30h")]
		public void EnsureDeleteMethod_IsCalled(string inputParameters, string consoleInput)
		{
			// Arrange
			var parameters = inputParameters.Split('|').ToList();

			var cityServicesMock = new Mock<ICityServices>();
			var movieServicesMock = new Mock<IMovieServices>();
			var openHourServicesMock = new Mock<IOpenHourServices>();
			var projectionServicesMock = new Mock<IProjectionsServices>();
			var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();

			cityServicesMock
				.Setup(cs => cs.GetID(parameters[1]))
				.Returns(1);

			movieServicesMock
				.Setup(ms => ms.GetID(parameters[0]))
				.Returns(1);

			openHourServicesMock
				.Setup(gs => gs.GetID(parameters[2]))
				.Returns(1);

			cinemaConsoleMock
				.Setup(c => c.ReadLineMiddle(It.IsAny<int>(), It.IsAny<int>()))
				.Returns(consoleInput);

			// Act
			var sut = new RemoveProjection(cinemaConsoleMock.Object, movieServicesMock.Object,
				cityServicesMock.Object, openHourServicesMock.Object, projectionServicesMock.Object);
			var result = sut.Execute(parameters);

			// Assert
			projectionServicesMock.Verify(ps => ps.Delete(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
		}

		[TestMethod]
		[DataRow("Venom|AdminMenu|Sofia|19:30h", "Venom|Sofia|19:30h", "AdminMenu")]
		public void InvalidClientInputException_WhenParameterIsInvalidMethod_IsCalled(string inputParameters, string consoleInput, string expected)
		{
			// Arrange
			var parameters = inputParameters.Split('|').ToList();

			var cityServicesMock = new Mock<ICityServices>();
			var movieServicesMock = new Mock<IMovieServices>();
			var openHourServicesMock = new Mock<IOpenHourServices>();
			var projectionServicesMock = new Mock<IProjectionsServices>();
			var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();

			cityServicesMock
				.Setup(cs => cs.GetID(parameters[1]))
				.Returns(1);

			movieServicesMock
				.Setup(ms => ms.GetID(parameters[0]))
				.Returns(1);

			openHourServicesMock
				.Setup(gs => gs.GetID(parameters[2]))
				.Returns(1);

			cinemaConsoleMock
				.Setup(c => c.ReadLineMiddle(It.IsAny<int>(), It.IsAny<int>()))
				.Returns(consoleInput);

			// Act
			var sut = new RemoveProjection(cinemaConsoleMock.Object, movieServicesMock.Object,
				cityServicesMock.Object, openHourServicesMock.Object, projectionServicesMock.Object);
			var result = sut.Execute(parameters);

			//Assert
			Assert.AreEqual(expected, result.First());
		}

	}
}