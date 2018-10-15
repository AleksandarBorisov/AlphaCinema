using AlphaCinema.Core.Contracts;
using AlphaCinema.Core.Utilities;
using AlphaCinemaServices.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaCinemaTests.AlphaCinema.Core.Commands.DisplayMenusTests.AllWatchedMoviesByUsersTests
{
    [TestClass]
    public class AllWatchedMoviesByUsers_Execute_Should
    {
        [TestMethod]
        [DataRow("AllWatchedMoviesByUsers PdfExport Menu BuyTickets LogAsAdmin ShowInfo PdfExport Exit 3 2",
            "Titanic", "ChooseHour")]
        public void ReturnCorrectResult_WhenIsCalled(string input, string selectorResult, string expected)
        {
            //Arrange
            var parameters = input.Split().ToList();

            var watchedMoviesServicesMock =  new Mock<IWatchedMovieServices>();
            var userServicesMock = new Mock<IUserServices>();
            var pdfExporterMock = new Mock<IPdfExporter>();
            //var cinemaConsoleMock = new Mock<IAlphaCinemaConsole>();

            
            //Act


            //Assert
        }

    }
}
