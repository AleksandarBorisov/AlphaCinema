using AlphaCinema.Core.Commands.Factory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using AlphaCinema.Core.Utilities;

namespace AlphaCinemaTests.Core.Utilities.CommandProcessorTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void CreateInstance_WhenParametersAreCorrect()
        {
            //Arrange
            var factoryMock = new Mock<ICommandFactory>();

            try
            {
                //Act
                var parser = new CommandProcessor(factoryMock.Object);
            }
            catch (Exception)
            {
                //Assert
                Assert.Fail("CommandProcessor constructor should not throw when parameters are correct!");
                throw;
            }
        }

        [TestMethod]
        public void ThrowException_WhenFactoryIsNull()
        {
            Assert.ThrowsException<NullReferenceException>(() => new CommandProcessor(null));
        }
    }
}
