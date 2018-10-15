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

namespace AlphaCinemaTests.AlphaCinemaServices.MovieServicesTests
{
    [TestClass]
    public class UpdateMovie_Should
    {
        private Movie movie;
        private Mock<IUnitOfWork> unitOfWork;
        private List<Movie> predifinedListOfMovies;
        private Mock<IRepository<Movie>> movieRepoMock;
        private int testMovieId = 1;
        private string testMovieName = "TestMovie";
        private string testMovieDescription = "It is a movie for testing";
        private int testMovieReleaseYear = 2000;
        private int testMovieDuration = 1990;

        [TestInitialize]
        public void TestInitialize()//Този метод се изпълнява преди извикване на всеки един от другите методи в този клас
        {
            //Arrange 
            movie = new Movie
            {
                Id = testMovieId,
                Name = testMovieName,
                Description = testMovieDescription,
                Duration = testMovieDuration,
                ReleaseYear = testMovieReleaseYear,
                Projections = new List<Projection>(),
                MovieGenres = new List<MovieGenre>(),
                IsDeleted = false
            };
            predifinedListOfMovies = new List<Movie>() { movie };
            unitOfWork = new Mock<IUnitOfWork>();
            movieRepoMock = new Mock<IRepository<Movie>>();
        }
        [TestMethod]
        public void UpdateMovie_WhenParametersAreCorrect()
        {
            var updatedMovie = new Movie();

            unitOfWork.Setup(x => x.Movies).Returns(movieRepoMock.Object);

            movieRepoMock
                .Setup(repo => repo.AllAndDeleted())
                .Returns(predifinedListOfMovies.AsQueryable());
            movieRepoMock
                .Setup(repo => repo.Update(It.IsAny<Movie>()))
                .Callback<Movie>((movie) =>
                {
                    predifinedListOfMovies.Remove(movie);
                    predifinedListOfMovies.Add(updatedMovie);
                });

            //Act
            var command = new MovieServices(unitOfWork.Object);
            command.UpdateMovie(testMovieName, testMovieDescription, testMovieReleaseYear, testMovieDuration);

            //Assert
            Assert.AreEqual(1, predifinedListOfMovies.Count);
            Assert.AreSame(updatedMovie, predifinedListOfMovies.First());
            Assert.IsFalse(updatedMovie.IsDeleted);
        }

        [TestMethod]
        public void ThrowEntityDoesntExistException_WhenMovieDoesNotExist()
        {
            //Arrange
            unitOfWork.Setup(x => x.Movies).Returns(movieRepoMock.Object);

            //Act
            var command = new MovieServices(unitOfWork.Object);

            //Assert
            Assert.ThrowsException<EntityDoesntExistException>(() => command.UpdateMovie(testMovieName, testMovieDescription,
                testMovieReleaseYear, testMovieDuration));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenMovieNameLengthIsNotInRange()
        {
            //Arrange
            int maxMovieNameLength = 50;
            var longMovieName = new string('m', maxMovieNameLength + 1);
            var movieService = new MovieServices(unitOfWork.Object);

            //Act and Assert
            Assert.ThrowsException<ArgumentException>(() => movieService.UpdateMovie(longMovieName, testMovieDescription, 
                testMovieReleaseYear, testMovieDuration));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenMovieDescriptionIsNotInRange()
        {
            //Arrange
            int maxMovieNameLength = 150;
            var longMovieDescription = new string('m', maxMovieNameLength + 1);

            var unitOfWork = new Mock<IUnitOfWork>();

            var movieService = new MovieServices(unitOfWork.Object);
            //Act and Assert
            Assert.ThrowsException<ArgumentException>(() => movieService.UpdateMovie(testMovieName, longMovieDescription, 
                testMovieReleaseYear, testMovieDuration));
        }
    }
}
