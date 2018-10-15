using AlphaCinemaData.Models;
using AlphaCinemaData.Repository;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices;
using AlphaCinemaServices.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinemaTests.AlphaCinemaServices.MovieServicesTests
{
    [TestClass]
    public class DeleteMovie_Should
    {
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IRepository<Movie>> movieRepoMock;
        private string testMovieName = "TestMovie";
        private Mock<Movie> movieMock;
        private List<Movie> predifinedListOfMovies;

        [TestInitialize]
        public void TestInitialize()//Този метод се изпълнява преди извикване на всеки един от другите методи в този клас
        {
            //Arrange
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
        public void DeleteMovie_WhenMovieExists()
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
            movieRepoMock.Setup(repo => repo.AllAndDeleted()).Returns(predifinedListOfMovies.AsQueryable());

            //Act
            var command = new MovieServices(unitOfWork.Object);
            command.DeleteMovie(movieMock.Object.Name);

            //Assert
            Assert.AreEqual(0, predifinedListOfMovies.Count);
        }
    }
}
