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
        [TestMethod]
        public void ReturnCollectionOfMovieNames_WhenTheyExist()
        {
            //Arrange
            var testMovieOne = new Movie
            {
                Name = "TestMovieOne",
                MovieGenres = new List<MovieGenre>() { new MovieGenre { GenreId = 0} },
                Projections = new List<Projection>() { new Projection { CityId = 0} }
            };

            var unitOfWork = new Mock<IUnitOfWork>();
            var movieRepoMock = new Mock<IRepository<Movie>>();

            var predifinedListOfMovieNames = new List<Movie>() { testMovieOne };
            var movieMock = new Mock<Movie>();

            unitOfWork.Setup(x => x.Movies).Returns(movieRepoMock.Object);
            movieRepoMock.Setup(repo => repo.All()).Returns(predifinedListOfMovieNames.AsQueryable());

            //Act
            var getMovieNames = new MovieServices(unitOfWork.Object);
            var result = getMovieNames.GetMovieNamesByCityIDGenreID(0, 0);

            //Assert
            Assert.AreEqual("TestMovieOne", result.First());
        }
    }
}
