using AlphaCinema.Core.Commands.BasicCommands;
using AlphaCinema.Core.Contracts;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace AlphaCinemaTests.AlphaCinema.Core.Commands.BasicCommandsTests.AddCityTests
{
    [TestClass]
    public class AddCity_Execute_Should
    {
        [TestMethod]
        [DataRow("Menu ChooseHour LogAsAdmin ShowInfo PdfExport ChooseHour ChooseGenre Exit", "1235", "ChooseHour")]
        [DataRow("Menu ChooseHour LogAsAdmin ShowInfo PdfExport ChooseHour ChooseGenre Exit", "", "ChooseHour")]
        public void ReturnCorrectList_WhenCityNameIsInvalid(string input, string consoleResult, string expected)
        {
            //Arrange
            var parameters = input.Split().ToList();

            var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();
            cinemaConsoleMock.Setup(console => console.ReadLineMiddle(It.IsAny<int>(), It.IsAny<int>())).Returns(consoleResult);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var cityServiceMock = new Mock<ICityServices>();
            //Act
            var addCity = new AddCity(cityServiceMock.Object, cinemaConsoleMock.Object);
            var result = addCity.Execute(parameters);

            //Arrange
            Assert.AreEqual(expected, result.First());
        }

        [TestMethod]
        [DataRow("Menu ChooseHour LogAsAdmin ShowInfo PdfExport ChooseHour ChooseGenre Exit", "Sofia", "ChooseHour")]
        public void ReturnCorrectList_WhenCityNameIsValid(string input, string consoleResult, string expected)
        {
            //Arrange
            var parameters = input.Split().ToList();

            var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();
            cinemaConsoleMock.Setup(console => console.ReadLineMiddle(It.IsAny<int>(), It.IsAny<int>())).Returns(consoleResult);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var cityServiceMock = new Mock<ICityServices>();
            //Act
            var addCity = new AddCity(cityServiceMock.Object, cinemaConsoleMock.Object);
            var result = addCity.Execute(parameters);

            //Arrange
            Assert.AreEqual(expected, result.First());
        }

        [TestMethod]
        [DataRow("Menu ChooseHour LogAsAdmin ShowInfo PdfExport ChooseHour ChooseGenre Exit", "")]
        [DataRow("Menu ChooseHour LogAsAdmin ShowInfo PdfExport ChooseHour ChooseGenre Exit", "Sofia")]
        public void ReadInTheMiddleIsCalled_EveryTime(string input, string consoleResult)
        {
            //Arrange
            var parameters = input.Split().ToList();

            var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();
            cinemaConsoleMock.Setup(console => console.ReadLineMiddle(It.IsAny<int>(), It.IsAny<int>())).Returns(consoleResult);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var cityServiceMock = new Mock<ICityServices>();
            //Act
            var addCity = new AddCity(cityServiceMock.Object, cinemaConsoleMock.Object);
            var result = addCity.Execute(parameters);

            //Arrange
            cinemaConsoleMock.Verify(console => console.ReadLineMiddle(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        [DataRow("Menu ChooseHour LogAsAdmin ShowInfo PdfExport ChooseHour ChooseGenre Exit", "Sofia")]
        public void AddNewCityIsCalled_WhenParametersAreCorrect(string input, string consoleResult)
        {
            //Arrange
            var parameters = input.Split().ToList();

            var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();
            cinemaConsoleMock.Setup(console => console.ReadLineMiddle(It.IsAny<int>(), It.IsAny<int>())).Returns(consoleResult);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var cityServiceMock = new Mock<ICityServices>();
            //Act
            var addCity = new AddCity(cityServiceMock.Object, cinemaConsoleMock.Object);
            var result = addCity.Execute(parameters);

            //Arrange
            cityServiceMock.Verify(cityService => cityService.AddNewCity(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [DataRow("Menu ChooseHour LogAsAdmin ShowInfo PdfExport ChooseHour ChooseGenre Exit", "")]
        public void AddNewMovieIsNeverCalled_WhenParametersAreNotCorrect(string input, string consoleResult)
        {
            //Arrange
            var parameters = input.Split().ToList();

            var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();
            cinemaConsoleMock.Setup(console => console.ReadLineMiddle(It.IsAny<int>(), It.IsAny<int>())).Returns(consoleResult);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var cityServiceMock = new Mock<ICityServices>();
            //Act
            var addCity = new AddCity(cityServiceMock.Object, cinemaConsoleMock.Object);
            var result = addCity.Execute(parameters);

            //Arrange
            cityServiceMock.Verify(genreService => genreService.AddNewCity(It.IsAny<string>()), Times.Never);
        }
    }
}
