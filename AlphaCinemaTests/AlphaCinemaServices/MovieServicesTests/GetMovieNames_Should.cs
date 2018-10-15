using AlphaCinemaData.Models;
using AlphaCinemaData.Repository;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaCinemaTests.AlphaCinemaServices.MovieServicesTests
{
    [TestClass]
    public class GetMovieNames_Should
    {
        [TestMethod]
        public void ReturnCollectionOfMovies()
        {
            //Arrange
            var firstMovieMock = new Mock<Movie>();
            firstMovieMock.Object.Name = "TestMovieOne";
            var secondMovieMock = new Mock<Movie>();
            secondMovieMock.Object.Name = "TestMovieTwo";

            var resultFromMovieRepo = new List<Movie>() { firstMovieMock.Object, secondMovieMock.Object };
            var expectedList = new List<string>() { "TestMovieOne", "TestMovieTwo" };

            var unitOfWork = new Mock<IUnitOfWork>();
            var movieRepoMock = new Mock<IRepository<Movie>>();

            unitOfWork.Setup(x => x.Movies).Returns(movieRepoMock.Object);
            movieRepoMock.Setup(repo => repo.All()).Returns(resultFromMovieRepo.AsQueryable());

            //Act 
            var movieService = new MovieServices(unitOfWork.Object);
            var result = movieService.GetMovieNames();

            //Assert
            CollectionAssert.AreEquivalent(expectedList, result.ToList());
        }
    }
}
