using AlphaCinemaData.Models;
using AlphaCinemaData.Repository;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinemaTests.AlphaCinemaServices.MovieServicesTests
{
    [TestClass]
    public class AddNewMovie_Should
    {
        [TestMethod]
        [DataRow("TestMovie" , "Test Description", 1990, 200)]
        public void AddNewMovie_WhenParametersAreCorrect(string name, string description, int releaseYear, int duration)
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var movieRepoMock = new Mock<IRepository<Movie>>();

            var predifinedListOfMovies = new List<Movie>();
            var movieMock = new Mock<Movie>();

            unitOfWork.Setup(x => x.Movies).Returns(movieRepoMock.Object);
            movieRepoMock
                .Setup(repo => repo.Add(It.IsAny<Movie>()))
                .Callback<Movie>((movie) =>
                {
                    predifinedListOfMovies.Add(movieMock.Object);
                });

            var addMovieCommand = new MovieServices(unitOfWork.Object);
            addMovieCommand.AddNewMovie(name, description, releaseYear, duration);

            //Assert
            Assert.AreEqual(1, predifinedListOfMovies.Count);
        }
    }
}
