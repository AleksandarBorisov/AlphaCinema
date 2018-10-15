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
        [TestMethod]
        [DataRow("TestMovie", "Test Description", 1990, 200)]
        public void UpdateMovie_WhenParametersAreCorrect(string name, string description, int releaseYear, int duration)
        {
            var alreadyAddedMovie = new Movie
            {
                Id = 1,
                Name = "TestMovie",
                Description = "It is a movie for testing",
                Duration = 100,
                ReleaseYear = 2000,
                Projections = new List<Projection>(),
                MovieGenres = new List<MovieGenre>(),
                IsDeleted = true
            };
            var updatedMovie = new Movie();

            var unitOfWork = new Mock<IUnitOfWork>();
            var movieRepoMock = new Mock<IRepository<Movie>>();

            var predifinedListOfMovies = new List<Movie>() { alreadyAddedMovie };
            var movieMock = new Mock<Movie>();

            unitOfWork.Setup(x => x.Movies).Returns(movieRepoMock.Object);
            movieRepoMock
                .Setup(repo => repo.AllAndDeleted())
                .Returns(predifinedListOfMovies.AsQueryable());
            movieRepoMock
                .Setup(repo => repo.Update(It.IsAny<Movie>()))
                .Callback<Movie>((movie) =>
                {
                    predifinedListOfMovies.Remove(alreadyAddedMovie);
                    predifinedListOfMovies.Add(updatedMovie);
                });

            //Act
            var addMovieCommand = new MovieServices(unitOfWork.Object);
            addMovieCommand.UpdateMovie(name, description, releaseYear, duration);

            //Assert
            Assert.AreEqual(1, predifinedListOfMovies.Count);
            Assert.AreSame(updatedMovie, predifinedListOfMovies.First());
            Assert.IsFalse(updatedMovie.IsDeleted);
        }

        [TestMethod]
        [DataRow("TestMovie", "Test Description", 1990, 200)]
        public void ThrowEntityDoesntExistException_WhenMovieDoesNotExist(string name, string description, int releaseYear, int duration)
        {
            var alreadyAddedMovie = new Movie();
            var updatedMovie = new Movie();

            var unitOfWork = new Mock<IUnitOfWork>();
            var movieRepoMock = new Mock<IRepository<Movie>>();

            var predifinedListOfMovies = new List<Movie>() { alreadyAddedMovie };
            var movieMock = new Mock<Movie>();

            unitOfWork.Setup(x => x.Movies).Returns(movieRepoMock.Object);
            movieRepoMock
                .Setup(repo => repo.AllAndDeleted())
                .Returns(predifinedListOfMovies.AsQueryable());
            movieRepoMock
                .Setup(repo => repo.Update(It.IsAny<Movie>()))
                .Callback<Movie>((movie) =>
                {
                    predifinedListOfMovies.Remove(alreadyAddedMovie);
                    predifinedListOfMovies.Add(updatedMovie);
                });

            //Act
            var addMovieCommand = new MovieServices(unitOfWork.Object);

            //Assert
            Assert.ThrowsException<EntityDoesntExistException>(() => addMovieCommand.UpdateMovie(name, description, releaseYear, duration));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenMovieLengthIsNotInRange()
        {
            //Arrange
            int maxMovieNameLength = 50;
            var testMovieName = new string('m', maxMovieNameLength + 1);

            var unitOfWork = new Mock<IUnitOfWork>();

            var movieService = new MovieServices(unitOfWork.Object);
            //Act and Assert
            Assert.ThrowsException<ArgumentException>(() => movieService.UpdateMovie(testMovieName, "Test Description", 1990, 2000));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenMovieDescriptionIsNotInRange()
        {
            //Arrange
            int maxMovieNameLength = 150;
            var testMovieDescription = new string('m', maxMovieNameLength + 1);

            var unitOfWork = new Mock<IUnitOfWork>();

            var movieService = new MovieServices(unitOfWork.Object);
            //Act and Assert
            Assert.ThrowsException<ArgumentException>(() => movieService.UpdateMovie("TestMovie", testMovieDescription, 1990, 2000));
        }
    }
}
