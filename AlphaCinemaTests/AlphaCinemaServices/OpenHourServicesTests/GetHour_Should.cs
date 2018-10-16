using AlphaCinemaData.Models;
using AlphaCinemaData.Models.Associative;
using AlphaCinemaData.Repository;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices;
using AlphaCinemaServices.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaCinemaTests.AlphaCinemaServices.OpenHourServicesTests
{
	[TestClass]
	public class GetHour_Should
	{
		private OpenHour openHour;
		private List<OpenHour> resultFromOpenHourRepo;
		private Mock<IUnitOfWork> unitOfWork;
		private Mock<IRepository<OpenHour>> openHourRepoMock;
		private int testOpenHourId = 1;
		private string testStartHour = "19:30h";

		[TestInitialize]
		public void TestInitialize()
		{
			openHour = new OpenHour
			{
				Id = testOpenHourId,
				StartHour = testStartHour,
				Projections = new List<Projection>(),
				IsDeleted = false
			};
			resultFromOpenHourRepo = new List<OpenHour>() { openHour };
			unitOfWork = new Mock<IUnitOfWork>();
			openHourRepoMock = new Mock<IRepository<OpenHour>>();
		}

		[TestMethod]
		public void ReturnCorrectStartHourString_When_StartHourWithIdExists()
		{
			//Arrange 
			unitOfWork.Setup(x => x.OpenHours)
				.Returns(openHourRepoMock.Object);

			openHourRepoMock.Setup(repo => repo.AllAndDeleted())
				.Returns(resultFromOpenHourRepo.AsQueryable());

			//Act 
			var sut = new OpenHourServices(unitOfWork.Object);
			var result = sut.GetHour(openHour.Id);

			//Assert
			Assert.AreEqual(openHour.StartHour, result);
		}

		[TestMethod]
		public void ReturnNull_When_StartHourDoesNotExist()
		{
			// Arrange
			var sut = new OpenHourServices(unitOfWork.Object);

			unitOfWork.Setup(x => x.OpenHours)
				.Returns(openHourRepoMock.Object);

			openHourRepoMock.Setup(repo => repo.AllAndDeleted())
				.Returns(resultFromOpenHourRepo.AsQueryable());
			// Act
			var result = sut.GetHour(2);
			//Assert
			Assert.AreEqual(null, result);
		}
	}
}
