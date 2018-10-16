using AlphaCinema.Core.Commands.DisplayMenus;
using AlphaCinema.Core.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinemaTests.AlphaCinema.Core.Commands.DisplayMenusTests.LoginTests
{
    [TestClass]
    public class Login_Execute_Should
    {
        [TestMethod]
        [DataRow("Menu 1 BuyTickets 5 LogAsAdmin ShowInfo 1 2", "123", "AdminMenu")]
        public void ReturnCorrectList_WhenPasswordIsCorrect(string input, string selectorResult, string expected)
        {
            //Arrange
            var parameters = input.Split().ToList();

            var itemSelectorMock = new Mock<IItemSelector>();
            itemSelectorMock.Setup(itemSelector => itemSelector.ReadAtPosition(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>())).Returns(selectorResult);

            //Act
            var login = new Login(itemSelectorMock.Object);
            var result = login.Execute(parameters);

            //Arrange
            Assert.AreEqual(expected, result.First());
        }

        [TestMethod]
        [DataRow("Menu 1 BuyTickets 5 LogAsAdmin ShowInfo 1 2", "123", "AdminMenu")]
        public void SelecterReadAtPositionIsCalled_Always(string input, string selectorResult, string expected)
        {
            //Arrange
            var parameters = input.Split().ToList();

            var itemSelectorMock = new Mock<IItemSelector>();
            itemSelectorMock.Setup(itemSelector => itemSelector.ReadAtPosition(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>())).Returns(selectorResult);

            //Act
            var login = new Login(itemSelectorMock.Object);
            var result = login.Execute(parameters);

            //Arrange
            itemSelectorMock.Verify(selector => selector.ReadAtPosition(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        [DataRow("Menu 1 BuyTickets 5 LogAsAdmin ShowInfo 1 2", "password", "Back","1")]
        public void ReturnCorrectList_WhenBackIsSelected(string input, string selectorReadResult, string selectorDisplayResult, string expected)
        {
            //Arrange
            var parameters = input.Split().ToList();

            var itemSelectorMock = new Mock<IItemSelector>();
            itemSelectorMock.Setup(itemSelector => itemSelector.ReadAtPosition(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>())).Returns(selectorReadResult);
            itemSelectorMock.Setup(itemSelector => itemSelector.DisplayItems(It.IsAny<List<string>>())).Returns(selectorDisplayResult);

            //Act
            var login = new Login(itemSelectorMock.Object);
            var result = login.Execute(parameters);

            //Arrange
            Assert.AreEqual(expected, result.First());
        }

        [TestMethod]
        [DataRow("Menu 1 BuyTickets 5 LogAsAdmin ShowInfo 1 2", "password", "Home", "1")]
        public void ReturnCorrectList_WhenHomeIsSelected(string input, string selectorReadResult, string selectorDisplayResult, string expected)
        {
            //Arrange
            var parameters = input.Split().ToList();

            var itemSelectorMock = new Mock<IItemSelector>();
            itemSelectorMock.Setup(itemSelector => itemSelector.ReadAtPosition(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>())).Returns(selectorReadResult);
            itemSelectorMock.Setup(itemSelector => itemSelector.DisplayItems(It.IsAny<List<string>>())).Returns(selectorDisplayResult);

            //Act
            var login = new Login(itemSelectorMock.Object);
            var result = login.Execute(parameters);

            //Arrange
            Assert.AreEqual(expected, result.First());
        }

        [TestMethod]
        [DataRow("Menu 1 BuyTickets 5 LogAsAdmin ShowInfo 1 2", "password", "Home", "1")]
        public void SelecterDisplayItemsIsCalled_WhenPasswordNotCorrect(string input, string selectorReadResult, string selectorDisplayResult, string expected)
        {
            //Arrange
            var parameters = input.Split().ToList();

            var itemSelectorMock = new Mock<IItemSelector>();
            itemSelectorMock.Setup(itemSelector => itemSelector.ReadAtPosition(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>())).Returns(selectorReadResult);
            itemSelectorMock.Setup(itemSelector => itemSelector.DisplayItems(It.IsAny<List<string>>())).Returns(selectorDisplayResult);

            //Act
            var login = new Login(itemSelectorMock.Object);
            var result = login.Execute(parameters);

            //Arrange
            itemSelectorMock.Verify(selector => selector.DisplayItems(It.IsAny<List<string>>()), Times.Once);
        }

    }
}
