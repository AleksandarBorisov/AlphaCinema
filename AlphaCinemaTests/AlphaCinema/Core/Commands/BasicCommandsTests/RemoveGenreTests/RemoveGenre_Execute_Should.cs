using AlphaCinema.Core.Commands.BasicCommands;
using AlphaCinema.Core.Contracts;
using AlphaCinemaServices.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaCinemaTests.AlphaCinema.Core.Commands.BasicCommandsTests.RemoveGenreTests
{
    [TestClass]
    public class RemoveGenre_Execute_Should
    {
        [TestMethod]
        [DataRow("Drama", "Drama AdminMenu Action", "AdminMenu")]
        public void ReturnCorrectList_WhenAGenreIsRemoved(string input, string inputParameters, string expected)
        {
            // Arrange
            var parameters = inputParameters.Split().ToList();

            var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();
            var genreServicesMock = new Mock<IGenreServices>();

            cinemaConsoleMock
                .Setup(c => c.ReadLineMiddle(It.IsAny<int>(), It.IsAny<int>()))
                .Returns("Drama");

            // Act
            var sut = new RemoveGenre(genreServicesMock.Object, cinemaConsoleMock.Object);
            var result = sut.Execute(parameters);

            //Assert
            Assert.AreEqual(expected, result.First());
        }

        [TestMethod]
        [DataRow("Drama", "Drama AdminMenu Action")]
        public void GenreServicesDeleteMethod_IsCalled(string input, string inputParameters)
        {
            // Arrange
            var parameters = inputParameters.Split().ToList();

            var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();
            var genreServicesMock = new Mock<IGenreServices>();

            cinemaConsoleMock
                .Setup(c => c.ReadLineMiddle(It.IsAny<int>(), It.IsAny<int>()))
                .Returns("Drama");

            // Act
            var sut = new RemoveGenre(genreServicesMock.Object, cinemaConsoleMock.Object);
            var result = sut.Execute(parameters);

            //Assert
            genreServicesMock.Verify(services => services.DeleteGenre(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [DataRow("Drama", "Drama AdminMenu Action","AdminMenu")]
        public void InvalidClientInputException_WhenParameterIsInvalidMethod_IsCalled(string input, string inputParameters,string expected)
        {
            // Arrange
            var parameters = inputParameters.Split().ToList();

            var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();
            var genreServicesMock = new Mock<IGenreServices>();

            cinemaConsoleMock
                .Setup(c => c.ReadLineMiddle(It.IsAny<int>(), It.IsAny<int>()))
                .Returns("Drama");

            // Act
            var sut = new RemoveGenre(genreServicesMock.Object, cinemaConsoleMock.Object);
            var result = sut.Execute(parameters);

            //Assert
            Assert.AreEqual(expected, result.First());
        }
    }
}
