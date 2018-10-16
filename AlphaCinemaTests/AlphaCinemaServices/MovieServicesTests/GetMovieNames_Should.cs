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
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IRepository<Movie>> movieRepoMock;
        private Mock<Movie> firstMovieMock;
        private string firstMovieName = "TestMovieOne";
        private Mock<Movie> secondMovieMock;
        private string secondMovieName = "TestMovieTwo";

        [TestInitialize]
        public void TestInitialize()//Този метод се изпълнява преди извикване на всеки един от другите методи в този клас
        {
            unitOfWork = new Mock<IUnitOfWork>();
            movieRepoMock = new Mock<IRepository<Movie>>();
            firstMovieMock = new Mock<Movie>();
			firstMovieMock
				.Setup(m => m.Name)
				.Returns("TestMovieOne");
            secondMovieMock = new Mock<Movie>();
			secondMovieMock
				.Setup(m => m.Name)
				.Returns("TestMovieTwo");
		}

        [TestMethod]
        public void ReturnCollectionOfMovies()
        {
            //Arrange
            var resultFromMovieRepo = new List<Movie>() { firstMovieMock.Object, secondMovieMock.Object };
            var expectedList = new List<string>() { "TestMovieOne", "TestMovieTwo" };

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
