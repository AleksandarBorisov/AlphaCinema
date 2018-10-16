using AlphaCinema.Core.Commands.BasicCommands;
using AlphaCinema.Core.Contracts;
using AlphaCinemaData.Models;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaCinemaTests.AlphaCinema.Core.Commands.BasicCommandsTests
{
	[TestClass]
	public class RemoveMovieGenre_Execute_Should
	{
		[TestMethod]
		[DataRow("Venom|AdminMenu|Action", "AdminMenu")]
		public void ReturnCorrectList_WhenALinkIsRemoved(string inputParameters, string expected)
		{
			// Arrange
			var parameters = inputParameters.Split('|').ToList();

			var movieServicesMock = new Mock<IMovieServices>();
			var genreServicesMock = new Mock<IGenreServices>();
			var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();
			var movieGenreServicesMock = new Mock<IMovieGenreServices>();

			movieServicesMock
				.Setup(ms => ms.GetID(parameters[0]))
				.Returns(1);

			genreServicesMock
				.Setup(gs => gs.GetID(parameters[1]))
				.Returns(1);

			cinemaConsoleMock
				.Setup(c => c.ReadLineMiddle(It.IsAny<int>(), It.IsAny<int>()))
				.Returns("Venom|Action");

			// Act
			var sut = new RemoveMovieGenre(genreServicesMock.Object, cinemaConsoleMock.Object,
				movieServicesMock.Object, movieGenreServicesMock.Object);
			var result = sut.Execute(parameters);

			//Assert
			Assert.AreEqual(expected, result.First());
		}

		[TestMethod]
		[DataRow("Venom|AdminMenu|Action")]
		public void DeleteMethod_IsCalled(string inputParameters)
		{
			// Arrange
			var parameters = inputParameters.Split('|').ToList();

			var movieServicesMock = new Mock<IMovieServices>();
			var genreServicesMock = new Mock<IGenreServices>();
			var movieGenreServicesMock = new Mock<IMovieGenreServices>();
			var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();

			movieServicesMock
				.Setup(ms => ms.GetID(parameters[0]))
				.Returns(1);

			genreServicesMock
				.Setup(gs => gs.GetID(parameters[1]))
				.Returns(1);

			movieGenreServicesMock
				.Setup(mgs => mgs.Delete(1, 1));

			cinemaConsoleMock
				.Setup(c => c.ReadLineMiddle(It.IsAny<int>(), It.IsAny<int>()))
				.Returns("Venom|Action");

			// Act
			var sut = new RemoveMovieGenre(genreServicesMock.Object, cinemaConsoleMock.Object,
				movieServicesMock.Object, movieGenreServicesMock.Object);
			var result = sut.Execute(parameters);

			//Assert
			movieGenreServicesMock.Verify(mg => mg.Delete(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
		}

		[TestMethod]
		[DataRow("Venom,|AdminMenu|1234", "AdminMenu")]
		public void ThrowInvalidClientInputException_WhenParametersAreInvalid(string inputParameters, string expected)
		{
			// Arrange
			var parameters = inputParameters.Split('|').ToList();

			var movieServicesMock = new Mock<IMovieServices>();
			var genreServicesMock = new Mock<IGenreServices>();
			var movieGenreServicesMock = new Mock<IMovieGenreServices>();
			var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();

			movieServicesMock
				.Setup(ms => ms.GetID(parameters[0]))
				.Returns(1);

			genreServicesMock
				.Setup(gs => gs.GetID(parameters[1]))
				.Returns(1);

			cinemaConsoleMock
				.Setup(c => c.ReadLineMiddle(It.IsAny<int>(), It.IsAny<int>()))
				.Returns("venom|1234");

			// Act
			var sut = new RemoveMovieGenre(genreServicesMock.Object, cinemaConsoleMock.Object,
				movieServicesMock.Object, movieGenreServicesMock.Object);
			var result = sut.Execute(parameters);

			// Assert
			Assert.AreEqual(expected, result.First());
		}
	}
}
