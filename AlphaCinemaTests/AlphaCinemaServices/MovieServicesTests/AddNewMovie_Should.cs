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
using AlphaCinemaServices.Exceptions;

namespace AlphaCinemaTests.AlphaCinemaServices.MovieServicesTests
{
    [TestClass]
    public class AddNewMovie_Should
    {
        private Movie movie;
        private List<Movie> predifinedListOfMovies;
        private Mock<IUnitOfWork> unitOfWork;
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
        public void AddNewMovie_WhenParametersAreCorrect()
        {
            //Arrange
            var movieMock = new Mock<Movie>();

            unitOfWork.Setup(x => x.Movies).Returns(movieRepoMock.Object);

            movieRepoMock
                .Setup(repo => repo.Add(It.IsAny<Movie>()))
                .Callback<Movie>((movie) =>
                {
                    predifinedListOfMovies.Add(movieMock.Object);
                });

            //Act
            var addMovieCommand = new MovieServices(unitOfWork.Object);
            addMovieCommand.AddNewMovie(testMovieName, testMovieDescription,
                testMovieReleaseYear, testMovieDuration);

            //Assert
            Assert.AreEqual(2, predifinedListOfMovies.Count);
        }

        [TestMethod]
        public void ThrowArgumentException_WhenMovieNameLengthIsNotInRange()
        {
            //Arrange
            int maxMovieNameLength = 50;
            var longMovieName = new string('m', maxMovieNameLength + 1);

            var movieService = new MovieServices(unitOfWork.Object);

            //Act and Assert
            Assert.ThrowsException<ArgumentException>(() => movieService.AddNewMovie(longMovieName, testMovieDescription, 
                testMovieReleaseYear, testMovieDuration));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenMovieNameIsEmpty()
        {
            //Arrange
            int maxMovieNameLength = 50;
            var longMovieName = new string('m', maxMovieNameLength + 1);

            var movieService = new MovieServices(unitOfWork.Object);

            //Act and Assert
            Assert.ThrowsException<NullReferenceException>(() => movieService.AddNewMovie("", testMovieDescription,
                testMovieReleaseYear, testMovieDuration));
        }


        [TestMethod]
        public void ThrowArgumentException_WhenMovieDescriptionIsNotInRange()
        {
            //Arrange
            int maxMovieNameLength = 150;
            var longMovieDescription = new string('m', maxMovieNameLength + 1);

            var movieService = new MovieServices(unitOfWork.Object);

            //Act and Assert
            Assert.ThrowsException<ArgumentException>(() => movieService.AddNewMovie(testMovieName, longMovieDescription,
                testMovieReleaseYear, testMovieDuration));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenMovieDescriptionIsEmpty()
        {
            //Arrange
            int maxMovieNameLength = 50;
            var longMovieName = new string('m', maxMovieNameLength + 1);

            var movieService = new MovieServices(unitOfWork.Object);

            //Act and Assert
            Assert.ThrowsException<NullReferenceException>(() => movieService.AddNewMovie(testMovieName, "",
                testMovieReleaseYear, testMovieDuration));
        }

        [TestMethod]
        public void UnmarkMovieAsDeleted_WhenMovieExistButIsDeleted()
        {
            //Arrange 
            movie.IsDeleted = true;

            unitOfWork.Setup(x => x.Movies).Returns(movieRepoMock.Object);
            movieRepoMock.Setup(repo => repo.AllAndDeleted()).Returns(predifinedListOfMovies.AsQueryable());

            //Act
            var addMovieCommand = new MovieServices(unitOfWork.Object);
            addMovieCommand.AddNewMovie(testMovieName, testMovieDescription, testMovieReleaseYear, testMovieDuration);

            //Assert
            Assert.IsFalse(movie.IsDeleted);
        }

        [TestMethod]
        public void ThrowEntityAlreadyExistsException_WhenMovieExist()
        {
            //Arrange 
            unitOfWork.Setup(x => x.Movies).Returns(movieRepoMock.Object);
            movieRepoMock.Setup(repo => repo.AllAndDeleted()).Returns(predifinedListOfMovies.AsQueryable());

            //Act and Assert
            var addMovieCommand = new MovieServices(unitOfWork.Object);
            Assert.ThrowsException<EntityAlreadyExistsException>(() => addMovieCommand.AddNewMovie(testMovieName, testMovieDescription,
                testMovieReleaseYear, testMovieDuration));
        }
    }
}
