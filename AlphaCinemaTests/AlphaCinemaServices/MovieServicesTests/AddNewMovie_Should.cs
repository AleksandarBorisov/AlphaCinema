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
using AlphaCinemaData.Context;
using Microsoft.EntityFrameworkCore;

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
        private Projection projection;
        private MovieGenre movieGenre;

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
            projection = new Projection();
            movieGenre = new MovieGenre();
        }

        [TestMethod]
        public void CallAddMethodOnMovie_WhenParametersAreCorrect()
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
            unitOfWork.Setup(p => p.Projections.AllAndDeleted()).Returns(new List<Projection>().AsQueryable());
            unitOfWork.Setup(mg => mg.MovieGenres.AllAndDeleted()).Returns(new List<MovieGenre>().AsQueryable());
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

        [TestMethod]
        public void AddAllPreviousProjections_WhenMovieIsRestored()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
                .UseInMemoryDatabase(databaseName: "AddAllPreviousProjections_WhenMovieIsRestored")
                .Options;

            movie.IsDeleted = true;
            projection.IsDeleted = true;
            movie.Projections.Add(projection);

            //Act
            using (var actContext = new AlphaCinemaContext(contextOptions))
            {
                actContext.Movies.Add(movie);
                actContext.SaveChanges();
                var unitOfWork = new UnitOfWork(actContext);
                var addMovieCommand = new MovieServices(unitOfWork);
                addMovieCommand.AddNewMovie(testMovieName, testMovieDescription,
                    testMovieReleaseYear, testMovieDuration);
            }

            //Assert
            using (var assertContext = new AlphaCinemaContext(contextOptions))
            {
                Assert.IsFalse(movie.Projections.First().IsDeleted);
            }
        }

        [TestMethod]
        public void AddAllPreviousMovieGenres_WhenMovieIsRestored()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
                .UseInMemoryDatabase(databaseName: "AddAllPreviousMovieGenres_WhenMovieIsRestored")
                .Options;

            movie.IsDeleted = true;
            movieGenre.IsDeleted = true;
            movie.MovieGenres.Add(movieGenre);

            //Act
            using (var actContext = new AlphaCinemaContext(contextOptions))
            {
                actContext.Movies.Add(movie);
                actContext.SaveChanges();
                var unitOfWork = new UnitOfWork(actContext);
                var addMovieCommand = new MovieServices(unitOfWork);
                addMovieCommand.AddNewMovie(testMovieName, testMovieDescription,
                    testMovieReleaseYear, testMovieDuration);
            }

            //Assert
            using (var assertContext = new AlphaCinemaContext(contextOptions))
            {
                Assert.IsFalse(movie.MovieGenres.First().IsDeleted);
            }
        }
    }
}
