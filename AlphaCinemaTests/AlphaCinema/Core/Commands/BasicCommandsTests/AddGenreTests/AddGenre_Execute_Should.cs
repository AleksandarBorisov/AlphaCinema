using AlphaCinema.Core.Commands.BasicCommands;
using AlphaCinema.Core.Contracts;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaCinemaTests.AlphaCinema.Core.Commands.BasicCommandsTests.AddGenreTests
{
    [TestClass]
    public class AddGenre_Execute_Should
    {
        [TestMethod]
        [DataRow("Menu ChooseHour LogAsAdmin ShowInfo PdfExport ChooseHour ChooseGenre Exit", "1235", "ChooseHour")]
        [DataRow("Menu ChooseHour LogAsAdmin ShowInfo PdfExport ChooseHour ChooseGenre Exit", "", "ChooseHour")]
        public void ReturnCorrectList_WhenGenreNameIsInvalid(string input, string consoleResult, string expected)
        {
            //Arrange
            var parameters = input.Split().ToList();

            var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();
            cinemaConsoleMock.Setup(console => console.ReadLineMiddle(It.IsAny<int>(), It.IsAny<int>())).Returns(consoleResult);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var genreServiceMock = new Mock<IGenreServices>();
            //Act
            var addGenre = new AddGenre(genreServiceMock.Object, cinemaConsoleMock.Object);
            var result = addGenre.Execute(parameters);

            //Arrange
            Assert.AreEqual(expected, result.First());
        }

        [TestMethod]
        [DataRow("Menu ChooseHour LogAsAdmin ShowInfo PdfExport ChooseHour ChooseGenre Exit", "Sofia", "ChooseHour")]
        public void ReturnCorrectList_WhenGenreNameIsValid(string input, string consoleResult, string expected)
        {
            //Arrange
            var parameters = input.Split().ToList();

            var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();
            cinemaConsoleMock.Setup(console => console.ReadLineMiddle(It.IsAny<int>(), It.IsAny<int>())).Returns(consoleResult);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var genreServiceMock = new Mock<IGenreServices>();
            //Act
            var addGenre = new AddGenre(genreServiceMock.Object, cinemaConsoleMock.Object);
            var result = addGenre.Execute(parameters);

            //Arrange
            Assert.AreEqual(expected, result.First());
        }

        [TestMethod]
        [DataRow("Menu ChooseHour LogAsAdmin ShowInfo PdfExport ChooseHour ChooseGenre Exit", "")]
        [DataRow("Menu ChooseHour LogAsAdmin ShowInfo PdfExport ChooseHour ChooseGenre Exit", "Disturbed")]
        public void ReadInTheMiddleIsCalled_EveryTime(string input, string consoleResult)
        {
            //Arrange
            var parameters = input.Split().ToList();

            var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();
            cinemaConsoleMock.Setup(console => console.ReadLineMiddle(It.IsAny<int>(), It.IsAny<int>())).Returns(consoleResult);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var genreServiceMock = new Mock<IGenreServices>();
            //Act
            var addGenre = new AddGenre(genreServiceMock.Object, cinemaConsoleMock.Object);
            var result = addGenre.Execute(parameters);

            //Arrange
            cinemaConsoleMock.Verify(console => console.ReadLineMiddle(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        [DataRow("Menu ChooseHour LogAsAdmin ShowInfo PdfExport ChooseHour ChooseGenre Exit", "Disturbed")]
        public void AddNewGenreIsCalled_WhenParametersAreCorrect(string input, string consoleResult)
        {
            //Arrange
            var parameters = input.Split().ToList();

            var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();
            cinemaConsoleMock.Setup(console => console.ReadLineMiddle(It.IsAny<int>(), It.IsAny<int>())).Returns(consoleResult);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var genreServiceMock = new Mock<IGenreServices>();
            //Act
            var addGenre = new AddGenre(genreServiceMock.Object, cinemaConsoleMock.Object);
            var result = addGenre.Execute(parameters);

            //Arrange
            genreServiceMock.Verify(genreService => genreService.AddNewGenre(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [DataRow("Menu ChooseHour LogAsAdmin ShowInfo PdfExport ChooseHour ChooseGenre Exit", "")]
        public void AddNewGenreIsNeverCalled_WhenParametersAreNotCorrect(string input, string consoleResult)
        {
            //Arrange
            var parameters = input.Split().ToList();

            var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();
            cinemaConsoleMock.Setup(console => console.ReadLineMiddle(It.IsAny<int>(), It.IsAny<int>())).Returns(consoleResult);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var genreServiceMock = new Mock<IGenreServices>();
            //Act
            var addGenre = new AddGenre(genreServiceMock.Object, cinemaConsoleMock.Object);
            var result = addGenre.Execute(parameters);

            //Arrange
            genreServiceMock.Verify(genreService => genreService.AddNewGenre(It.IsAny<string>()), Times.Never);
        }
    }
}
