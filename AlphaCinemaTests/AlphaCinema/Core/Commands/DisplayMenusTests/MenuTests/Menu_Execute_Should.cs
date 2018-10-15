using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlphaCinema.Core.Contracts;
using AlphaCinema.Core.Commands.DisplayMenus;

namespace AlphaCinemaTests.AlphaCinema.Core.Commands.DisplayMenusTests.MenuTests
{
    [TestClass]
    public class Menu_Execute_Should
    {
        [TestMethod]
        [DataRow( "Menu BuyTickets LogAsAdmin ShowInfo PdfExport Exit",          "BuyTickets",          "ChooseCity")]
        [DataRow( "Menu BuyTickets LogAsAdmin ShowInfo PdfExport Exit", "LogAsAdmin", "Login")]
        public void ReturnCorrectList_WhenParametersAreValid(string input, string selectorResult, string expected)
        {
            //Arrange
            var parameters = input.Split().ToList();
            var selectorMock = new Mock<IItemSelector>();
            selectorMock.Setup(selector => selector.DisplayItems(It.IsAny<List<string>>())).Returns(selectorResult);

            //Act
            var menu = new Menu(selectorMock.Object);
            var result = menu.Execute(parameters);
            //Assert
            Assert.AreEqual(expected, result.First());
        }
    }
}
