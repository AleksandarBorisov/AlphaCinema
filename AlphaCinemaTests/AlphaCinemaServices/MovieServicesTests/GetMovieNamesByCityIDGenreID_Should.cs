using AlphaCinemaData.Models;
using AlphaCinemaData.Models.Associative;
using AlphaCinemaData.Repository;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinemaTests.AlphaCinemaServices.MovieServicesTests
{
    [TestClass]
    public class GetMovieNamesByCityIDGenreID_Should
    {
        private Mock<IUnitOfWork> unitOfWork;
        private List<Movie> predifinedListOfMovieNames;
        private Mock<IRepository<Movie>> movieRepoMock;
        private string testMovieName = "TestMovieOne";
        private Movie testMovie;

        [TestInitialize]
        public void TestInitialize()//Този метод се изпълнява преди извикване на всеки един от другите методи в този клас
        {
            unitOfWork = new Mock<IUnitOfWork>();
            movieRepoMock = new Mock<IRepository<Movie>>();
            testMovie = new Movie
            {
                Name = testMovieName,
                MovieGenres = new List<MovieGenre>() { new MovieGenre { GenreId = 0 } },
                Projections = new List<Projection>() { new Projection { CityId = 0 } }
            };
            predifinedListOfMovieNames = new List<Movie>() { testMovie };
        }

        [TestMethod]
        public void ReturnCollectionOfMovieNames_WhenTheyExist()
        {
            //Arrange
            unitOfWork.Setup(x => x.Movies).Returns(movieRepoMock.Object);
            movieRepoMock.Setup(repo => repo.All()).Returns(predifinedListOfMovieNames.AsQueryable());

            //Act
            var command = new MovieServices(unitOfWork.Object);
            var result = command.GetMovieNamesByCityIDGenreID(0, 0);

            //Assert
            Assert.AreEqual(testMovieName, result.First());
        }
    }
}
