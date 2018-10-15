using AlphaCinema.Core.Commands.Factory;
using AlphaCinema.Core.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using AlphaCinema.Core.Contracts;

namespace AlphaCinemaTests.AlphaCinema.Core.Utilities.CommandProcessorTests
{
    [TestClass]
    public class ParseCommand_Should
    {
        [TestMethod]
        [DataRow("Menu")]
        [DataRow("Login")]
        [DataRow("ChooseMovie")]
        [DataRow("AddMovie")]
        [DataRow("RemoveMovie")]
        public void ReturnCommand_WhenParametersIsCorrect(string commandName)
        {
            //Arrange
            var factoryMock = new Mock<ICommandFactory>();
            var commandMock = new Mock<ICommand>();

            factoryMock.Setup(f => f.GetCommand(commandName)).Returns(commandMock.Object);
            var processor = new CommandProcessor(factoryMock.Object);

            //Act
            var command = processor.ParseCommand(commandName);

            //Assert
            Assert.AreEqual(commandMock.Object, command);
        }
    }
}
