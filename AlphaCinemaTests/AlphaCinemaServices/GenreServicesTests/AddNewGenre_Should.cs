using AlphaCinemaData.Models;
using AlphaCinemaData.Repository;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinemaTests.AlphaCinemaServices.GenreServicesTests
{
    [TestClass]
    public class AddNewGenre_Should
    {
        [TestMethod]
        [DataRow("Comedy")]
        public void AddNewGenre_WhenParametersAreCorrect(string genreName)
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var genreRepoMock = new Mock<IRepository<Genre>>();

            var predifinedListOfGenres = new List<Genre>();
            var genreMock = new Mock<Genre>();

            //Setup
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.Genres)
                .Returns(genreRepoMock.Object);

            genreRepoMock.Setup(repo => repo.Add(It.IsAny<Genre>()))
                .Callback<Genre>((genre) =>
                {
                    predifinedListOfGenres.Add(genreMock.Object);
                });

            //Act
            var genreServices = new GenreServices(unitOfWorkMock.Object);
            genreServices.AddNewGenre(genreName);

            //Assert
            Assert.AreEqual(1, predifinedListOfGenres.Count);
        }



    }
}
