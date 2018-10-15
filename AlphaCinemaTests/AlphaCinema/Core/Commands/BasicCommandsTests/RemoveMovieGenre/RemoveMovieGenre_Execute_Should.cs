using AlphaCinema.Core.Commands.BasicCommands;
using AlphaCinema.Core.Contracts;
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
		[DataRow("Venom Action", "Venom|AdminMenu|Action", "AdminMenu")]
		public void ReturnCorrectList_WhenALinkIsRemoved(string input, string parameters, string expected)
		{
			// Arrange
			var inputParameters = input.Split().ToList();

			var movieServicesMock = new Mock<IMovieServices>();
			movieServicesMock
				.Setup(ms => ms.GetID(inputParameters[0]))
				.Returns(1);

			var genreServicesMock = new Mock<IGenreServices>();
			genreServicesMock
				.Setup(gs => gs.GetID(inputParameters[1]))
				.Returns(1);

			var movieGenreServicesMock = new Mock<IMovieGenreServices>();
			movieGenreServicesMock
				.Setup(mgs => mgs.Delete(1, 1));

			var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();
			cinemaConsoleMock
				.Setup(c => c.ReadLineMiddle(It.IsAny<int>(), It.IsAny<int>()))
				.Returns("Venom|Action");

			// Act
			var sut = new RemoveMovieGenre(genreServicesMock.Object, cinemaConsoleMock.Object,
				movieServicesMock.Object, movieGenreServicesMock.Object);
			var result = sut.Execute(parameters.Split('|'));

			//Assert
			Assert.AreEqual(expected, result.First());
		}

		[TestMethod]
		[DataRow("Venom|Action|Test", "Action")]
		public void InvalidClientInputException_WhenParametersAreInvalid(string input, string expected)
		{
			// Arrange
			var parameters = input.Split('|').ToList();
			var movieServicesMock = new Mock<IMovieServices>();
			movieServicesMock
				.Setup(ms => ms.GetID(parameters[0]))
				.Returns(1);

			var genreServicesMock = new Mock<IGenreServices>();
			genreServicesMock
				.Setup(gs => gs.GetID(parameters[1]))
				.Returns(1);

			var movieGenreServicesMock = new Mock<IMovieGenreServices>();
			movieGenreServicesMock
				.Setup(mgs => mgs.Delete(1, 1));

			var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();
			cinemaConsoleMock
				.Setup(c => c.ReadLineMiddle(It.IsAny<int>(), It.IsAny<int>()))
				.Returns(input);

			// Act
			var sut = new RemoveMovieGenre(genreServicesMock.Object, cinemaConsoleMock.Object,
				movieServicesMock.Object, movieGenreServicesMock.Object);
			var result = sut.Execute(parameters);

			// Assert
			Assert.AreEqual(expected, result.First());
		}
	}
}
