using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using AlphaCinemaData.Models.Associative;
using AlphaCinemaData.Repository;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices;
using AlphaCinemaServices.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinemaTests.AlphaCinemaServices.MovieServicesTests
{
    [TestClass]
    public class DeleteMovie_Should
    {
        private Movie movie;
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IRepository<Movie>> movieRepoMock;
        private int testMovieId = 1;
        private string testMovieName = "TestMovie";
        private string testMovieDescription = "It is a movie for testing";
        private int testMovieReleaseYear = 2000;
        private int testMovieDuration = 1990;
        private Mock<Movie> movieMock;
        private List<Movie> predifinedListOfMovies;

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
            movieMock = new Mock<Movie>();
            unitOfWork = new Mock<IUnitOfWork>();
            movieRepoMock = new Mock<IRepository<Movie>>();
            predifinedListOfMovies = new List<Movie>() { movieMock.Object };
        }

        [TestMethod]
        public void ThrowEntityDoesntExistException_WhenMovieDoesNotExist()
        {
            //Arrange
            unitOfWork.Setup(x => x.Movies).Returns(movieRepoMock.Object);

            //Act and Assert
            var deleteMovieCommand = new MovieServices(unitOfWork.Object);
            Assert.ThrowsException<EntityDoesntExistException>(() => deleteMovieCommand.DeleteMovie(testMovieName));
        }

        [TestMethod]
        public void ThrowEntityDoesntExistException_WhenMovieIsDeleted()
        {
            //Arrange
            movieMock.Object.IsDeleted = true;

            unitOfWork.Setup(x => x.Movies).Returns(movieRepoMock.Object);
            movieRepoMock.Setup(repo => repo.AllAndDeleted()).Returns(predifinedListOfMovies.AsQueryable());

            //Act and Assert
            var deleteMovieCommand = new MovieServices(unitOfWork.Object);
            Assert.ThrowsException<EntityDoesntExistException>(() => deleteMovieCommand.DeleteMovie(testMovieName));
        }

        [TestMethod]
        public void CallDeleteMethodOnMovie_WhenMovieExists()
        {
            //Arrange
            movieMock.Object.IsDeleted = false;

            movieRepoMock
                .Setup(repo => repo.Delete(It.IsAny<Movie>()))
                .Callback<Movie>((movie) =>
                {
                    predifinedListOfMovies.Remove(movieMock.Object);
                });

            unitOfWork.Setup(x => x.Movies).Returns(movieRepoMock.Object);
            unitOfWork.Setup(p => p.Projections.All()).Returns(new List<Projection>().AsQueryable());
            unitOfWork.Setup(mg => mg.MovieGenres.All()).Returns(new List<MovieGenre>().AsQueryable());
            movieRepoMock.Setup(repo => repo.AllAndDeleted()).Returns(predifinedListOfMovies.AsQueryable());

            //Act
            var command = new MovieServices(unitOfWork.Object);
            command.DeleteMovie(movieMock.Object.Name);

            //Assert
            Assert.AreEqual(0, predifinedListOfMovies.Count);
        }

        [TestMethod]
        public void DeleteAllMovieProjections_WhenMovieIsDeleted()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
                .UseInMemoryDatabase(databaseName: "DeleteAllMovieProjections_WhenMovieIsDeleted")
                .Options;

            var projection = new Projection();
            movie.Projections.Add(projection);

            //Act
            using (var actContext = new AlphaCinemaContext(contextOptions))
            {
                actContext.Movies.Add(movie);
                actContext.SaveChanges();
                var unitOfWork = new UnitOfWork(actContext);
                var command = new MovieServices(unitOfWork);
                command.DeleteMovie(testMovieName);
            }

            //Assert
            using (var assertContext = new AlphaCinemaContext(contextOptions))
            {
                Assert.IsTrue(movie.Projections.First().IsDeleted);
            }
        }

        [TestMethod]
        public void DeleteAllMovieGenres_WhenMovieIsDeleted()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
                .UseInMemoryDatabase(databaseName: "DeleteAllMovieGenres_WhenMovieIsDeleted")
                .Options;

            var movieGenre = new MovieGenre();
            movie.MovieGenres.Add(movieGenre);

            //Act
            using (var actContext = new AlphaCinemaContext(contextOptions))
            {
                actContext.Movies.Add(movie);
                actContext.SaveChanges();
                var unitOfWork = new UnitOfWork(actContext);
                var command = new MovieServices(unitOfWork);
                command.DeleteMovie(testMovieName);
            }

            //Assert
            using (var assertContext = new AlphaCinemaContext(contextOptions))
            {
                Assert.IsTrue(movie.MovieGenres.First().IsDeleted);
            }
        }
    }
}
