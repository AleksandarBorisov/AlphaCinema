using AlphaCinema.Core.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlphaCinema.Core.Commands.BasicCommands;
using Moq;
using System.Linq;
using AlphaCinemaServices.Contracts;

namespace AlphaCinemaTests.AlphaCinema.Core.Commands.BasicCommandsTests.RemoveMovieTests
{
    [TestClass]
    public class RemoveMovie_Execute_Should
    {
        [TestMethod]
        [DataRow("Titanic", "Titanic AdminMenu Action", "AdminMenu")]
        public void ReturnCorrectList_WhenAMovieIsRemoved(string input, string inputParameters, string expected)
        {
            // Arrange
            var parameters = inputParameters.Split().ToList();

            var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();
            var moveServicesMock = new Mock<IMovieServices>();

            cinemaConsoleMock
                .Setup(c => c.ReadLineMiddle(It.IsAny<int>(), It.IsAny<int>()))
                .Returns("Titanic");

            // Act
            var sut = new RemoveMovie(moveServicesMock.Object, cinemaConsoleMock.Object);
            var result = sut.Execute(parameters);

            //Assert
            Assert.AreEqual(expected, result.First());
        }

        [TestMethod]
        [DataRow("Titanic", "Titanic AdminMenu Action")]
        public void MovieServicesDeleteMethod_IsCalled(string input, string inputParameters)
        {
            // Arrange
            var parameters = inputParameters.Split().ToList();

            var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();
            var movieServicesMock = new Mock<IMovieServices>();

            cinemaConsoleMock
                .Setup(c => c.ReadLineMiddle(It.IsAny<int>(), It.IsAny<int>()))
                .Returns("Titanic");

            // Act
            var sut = new RemoveMovie(movieServicesMock.Object, cinemaConsoleMock.Object);
            var result = sut.Execute(parameters);

            //Assert
            movieServicesMock.Verify(services => services.DeleteMovie(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [DataRow("Titanic", "Titanic AdminMenu Action", "AdminMenu")]
        public void InvalidClientInputException_WhenParameterIsInvalid(string input, string inputParameters, string expected)
        {
            // Arrange
            var parameters = inputParameters.Split().ToList();

            var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();
            var movieServicesMock = new Mock<IMovieServices>();

            cinemaConsoleMock
                .Setup(c => c.ReadLineMiddle(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(" ");

            // Act
            var sut = new RemoveMovie(movieServicesMock.Object, cinemaConsoleMock.Object);
            var result = sut.Execute(parameters);

            //Assert
            Assert.AreEqual(expected, result.First());
        }

    }
}
