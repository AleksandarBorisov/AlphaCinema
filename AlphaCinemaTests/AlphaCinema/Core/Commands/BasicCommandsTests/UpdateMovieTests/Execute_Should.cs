using AlphaCinema.Core.Commands.BasicCommands;
using AlphaCinema.Core.Contracts;
using AlphaCinemaData.Models;
using AlphaCinemaServices.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaCinemaTests.AlphaCinema.Core.Commands.BasicCommandsTests
{
	[TestClass]
	public class Execute_Should
	{
		[TestMethod]
		[DataRow("Venom|AdminMenu|Venom description|2018|143", "AdminMenu",
			"Venom | Venom description | 2018 | 143",
			"AdminMenu")]
		public void ReturnCorrectList_WhenALinkIsRemoved(string inputParameters,
				string selectorResult, string readFromSelector, string expected)
		{
			// Arrange
			var parameters = inputParameters.Split('|').ToList();
			var name = parameters[0];
			var description = parameters[2];
			var year = int.Parse(parameters[3]);
			var duration = int.Parse(parameters[4]);

			var movieMock = new Mock<Movie>();
			var selectorMock = new Mock<IItemSelector>();
			var movieServicesMock = new Mock<IMovieServices>();

			selectorMock
				.Setup(s => s.DisplayItems(It.IsAny<List<string>>()))
				.Returns(selectorResult);

			selectorMock
				.Setup(s => s.ReadAtPosition(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
				.Returns(readFromSelector);

			movieServicesMock
				.Setup(ms => ms.UpdateMovie(name, description, year, duration));

			// Act
			var sut = new UpdateMovie(selectorMock.Object, movieServicesMock.Object);
			var result = sut.Execute(parameters);

			// Assert
			Assert.AreEqual(expected, result.First());
		}

		[TestMethod]
		[DataRow("Venom|AdminMenu|Venom description|2018|143", "AdminMenu",
			"Venom | Venom description | 2018 | 143",
			"AdminMenu")]
		public void EnsureUpdateMethod_IsCalled(string inputParameters,
				string selectorResult, string readFromSelector, string expected)
		{
			// Arrange
			var parameters = inputParameters.Split('|').ToList();
			var name = parameters[0];
			var description = parameters[2];
			var year = int.Parse(parameters[3]);
			var duration = int.Parse(parameters[4]);

			var movieMock = new Mock<Movie>();
			var selectorMock = new Mock<IItemSelector>();
			var movieServicesMock = new Mock<IMovieServices>();
			selectorMock
				.Setup(s => s.DisplayItems(It.IsAny<List<string>>()))
				.Returns(selectorResult);
			selectorMock
				.Setup(s => s.ReadAtPosition(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
				.Returns(readFromSelector);

			// Act
			var sut = new UpdateMovie(selectorMock.Object, movieServicesMock.Object);
			var result = sut.Execute(parameters);

			// Assert
			movieServicesMock.Verify(ms => ms.UpdateMovie(It.IsAny<string>(), It.IsAny<string>(),
				It.IsAny<int>(), It.IsAny<int>()), Times.Once);
		}

		[TestMethod]
		[DataRow("Venom|AdminMenu|Venom description|2018|143", "Retry",
			"Venom | Venom description | releaseYear | 143")]
		public void ReturnCorrectList_WhenRetryIsSelected(string inputParameters,
				string selectorResult, string readFromSelector)
		{
			// Arrange
			var parameters = inputParameters.Split('|').ToList();
			var name = parameters[0];
			var description = parameters[2];
			var year = int.Parse(parameters[3]);
			var duration = int.Parse(parameters[4]);

			var movieMock = new Mock<Movie>();
			var selectorMock = new Mock<IItemSelector>();
			var movieServicesMock = new Mock<IMovieServices>();

			selectorMock
				.Setup(s => s.DisplayItems(It.IsAny<List<string>>()))
				.Returns(selectorResult);

			selectorMock
				.Setup(s => s.ReadAtPosition(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
				.Returns(readFromSelector);

			movieServicesMock
				.Setup(ms => ms.UpdateMovie(name, description, year, duration));

			// Act
			var sut = new UpdateMovie(selectorMock.Object, movieServicesMock.Object);
			var result = sut.Execute(parameters);

			// Assert
			CollectionAssert.AreEqual(parameters, result.ToList());
		}

		[TestMethod]
		[DataRow("Venom|AdminMenu|Venom description|2018|143", "Back",
				"Venom | Venom description | releaseYear | 143", "AdminMenu")]
		public void ReturnCorrectList_WhenBackIsSelected(string inputParameters,
					string selectorResult, string readFromSelector, string expected)
		{
			// Arrange
			var parameters = inputParameters.Split('|').ToList();
			var name = parameters[0];
			var description = parameters[2];
			var year = int.Parse(parameters[3]);
			var duration = int.Parse(parameters[4]);

			var movieMock = new Mock<Movie>();
			var selectorMock = new Mock<IItemSelector>();
			var movieServicesMock = new Mock<IMovieServices>();

			selectorMock
				.Setup(s => s.DisplayItems(It.IsAny<List<string>>()))
				.Returns(selectorResult);

			selectorMock
				.Setup(s => s.ReadAtPosition(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
				.Returns(readFromSelector);

			movieServicesMock
				.Setup(ms => ms.UpdateMovie(name, description, year, duration));

			// Act
			var sut = new UpdateMovie(selectorMock.Object, movieServicesMock.Object);
			var result = sut.Execute(parameters);

			// Assert
			Assert.AreEqual(expected, result.First());
		}

		[TestMethod]
		[DataRow("Venom|AdminMenu|Menu|Venom description|2018|143", "Home",
				"Venom | Venom description | releaseYear | 143", "Menu")]
		public void ReturnCorrectList_WhenHomeIsSelected(string inputParameters,
					string selectorResult, string readFromSelector, string expected)
		{
			// Arrange
			var parameters = inputParameters.Split('|').ToList();
			var name = parameters[0];
			var description = parameters[2];
			var year = int.Parse(parameters[4]);
			var duration = int.Parse(parameters[5]);

			var movieMock = new Mock<Movie>();
			var selectorMock = new Mock<IItemSelector>();
			var movieServicesMock = new Mock<IMovieServices>();

			selectorMock
				.Setup(s => s.DisplayItems(It.IsAny<List<string>>()))
				.Returns(selectorResult);

			selectorMock
				.Setup(s => s.ReadAtPosition(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
				.Returns(readFromSelector);

			movieServicesMock
				.Setup(ms => ms.UpdateMovie(name, description, year, duration));

			// Act
			var sut = new UpdateMovie(selectorMock.Object, movieServicesMock.Object);
			var result = sut.Execute(parameters);

			// Assert
			Assert.AreEqual(expected, result.First());
		}
	}
}
