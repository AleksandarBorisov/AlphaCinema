using AlphaCinema.Core.Commands.Factory;
using AlphaCinema.Core.Contracts;
using AlphaCinema.Core.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinemaTests.Core.Utilities.CommandProcessorTests
{
    [TestClass]
    public class ExecuteCommand_Should
    {
        [TestMethod]
        [DataRow("Menu")]
        [DataRow("Login")]
        [DataRow("ChooseMovie")]
        [DataRow("AddMovie")]
        [DataRow("RemoveMovie")]
        public void CallExecuteMethod_WhenCommandIsValid(string commandName)
        {
            //Arrange
            var factoryMock = new Mock<ICommandFactory>();
            var commandMock = new Mock<ICommand>();

            var processor = new CommandProcessor(factoryMock.Object);
            factoryMock.Setup(f => f.GetCommand(commandName)).Returns(commandMock.Object);

            var parameters = new List<string>() { commandName };
            //Act
            processor.ExecuteCommand(parameters);
            //Assert
            commandMock.Verify(command => command.Execute(parameters), Times.Once);
        }
    }
}
