using AlphaCinema.Core.Commands.BasicCommands;
using AlphaCinema.Core.Contracts;
using AlphaCinema.Core.Utilities;
using AlphaCinemaData.Models;
using AlphaCinemaServices.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinemaTests.AlphaCinema.Core.Commands.DisplayMenusTests.AllWatchedMoviesByUsersTests
{
    [TestClass]
    public class AllWatchedMoviesByUsers_Execute_Should
    {
        //[TestMethod]
        //[DataRow("AllWatchedMoviesByUsers PdfExport Menu BuyTickets LogAsAdmin ShowInfo PdfExport Exit 3 2",
        //    "Titanic", "ChooseHour")]
        //public void ReturnCorrectResult_WhenIsCalled(string input, string selectorResult, string expected)
        //{
        //    //Arrange
        //    var parameters = input.Split().ToList();

        //    var watchedMoviesServicesMock = new Mock<IWatchedMovieServices>();
        //    var userServicesMock = new Mock<IUserServices>();
        //    var pdfExporterMock = new Mock<IPdfExporter>();
        //    var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();

        //    var moviesMock = new HashSet<Movie>();

        //    SortedDictionary<string, HashSet<Movie>> watchedMoviesMock = new SortedDictionary<string, HashSet<Movie>>();
            
        //    //pdfExporterMock.Setup(pdfExp => pdfExp
        //    //    .ExportWatchedMoviesByUsers(watchedMoviesMock)).returns

            
        //    //Act
        //    var allWatchedMoviesByUsers = new AllWatchedMoviesByUsers(cinemaConsoleMock.Object,
        //    watchedMoviesServicesMock.Object, userServicesMock.Object, pdfExporterMock.Object);

        //    var result = allWatchedMoviesByUsers.Execute(parameters);

        //    //Assert
        //    Assert.AreEqual(expected, result.First());
        //}

    }
}
