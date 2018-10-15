using AlphaCinemaData.Models;
using AlphaCinemaData.Models.Associative;
using AlphaCinemaData.Repository;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlphaCinemaServices.Exceptions;

namespace AlphaCinemaTests.AlphaCinemaServices.MovieServicesTests
{
    [TestClass]
    public class AddNewMovie_Should
    {
        [TestMethod]
        [DataRow("TestMovie", "Test Description", 1990, 200)]
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

        [TestMethod]
        public void ThrowArgumentException_WhenMovieLengthIsNotInRange()
        {
            //Arrange
            int maxMovieNameLength = 50;
            var testMovieName = new string('m', maxMovieNameLength + 1);

            var unitOfWork = new Mock<IUnitOfWork>();

            var movieService = new MovieServices(unitOfWork.Object);
            //Act and Assert
            Assert.ThrowsException<ArgumentException>(() => movieService.AddNewMovie(testMovieName, "Test Description", 1990, 2000));
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
            Assert.ThrowsException<ArgumentException>(() => movieService.AddNewMovie("TestMovie", testMovieDescription, 1990, 2000));
        }

        [TestMethod]
        [DataRow("TestMovie", "Test Description", 1990, 200)]
        public void UnmarkMovieAsDeleted_WhenMovieExistButIsDeleted(string name, string description, int releaseYear, int duration)
        {
            //Arrange 
            var movie = new Movie
            {
                Id = 1,
                Name = name,
                Description = description,
                Duration = releaseYear,
                ReleaseYear = duration,
                Projections = new List<Projection>(),
                MovieGenres = new List<MovieGenre>(),
                IsDeleted = true
            };

            var resultFromMovieRepo = new List<Movie>() { movie };

            var unitOfWork = new Mock<IUnitOfWork>();
            var movieRepoMock = new Mock<IRepository<Movie>>();

            unitOfWork.Setup(x => x.Movies).Returns(movieRepoMock.Object);
            movieRepoMock.Setup(repo => repo.AllAndDeleted()).Returns(resultFromMovieRepo.AsQueryable());

            //Act
            var addMovieCommand = new MovieServices(unitOfWork.Object);
            addMovieCommand.AddNewMovie(name, description, releaseYear, duration);

            //Assert
            Assert.IsFalse(movie.IsDeleted);
        }

        [TestMethod]
        [DataRow("TestMovie", "Test Description", 1990, 200)]
        public void ThrowEntityAlreadyExistsException_WhenMovieExist(string name, string description, int releaseYear, int duration)
        {
            //Arrange 
            var movie = new Movie
            {
                Id = 1,
                Name = name,
                Description = description,
                Duration = releaseYear,
                ReleaseYear = duration,
                Projections = new List<Projection>(),
                MovieGenres = new List<MovieGenre>(),
                IsDeleted = false
            };

            var resultFromMovieRepo = new List<Movie>() { movie };

            var unitOfWork = new Mock<IUnitOfWork>();
            var movieRepoMock = new Mock<IRepository<Movie>>();

            unitOfWork.Setup(x => x.Movies).Returns(movieRepoMock.Object);
            movieRepoMock.Setup(repo => repo.AllAndDeleted()).Returns(resultFromMovieRepo.AsQueryable());

            //Act and Assert
            var addMovieCommand = new MovieServices(unitOfWork.Object);
            Assert.ThrowsException<EntityAlreadyExistsException>(() => addMovieCommand.AddNewMovie(name, description, releaseYear, duration));

        }
    }
}
