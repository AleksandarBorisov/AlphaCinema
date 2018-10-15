using AlphaCinemaData.Models;
using AlphaCinemaData.Repository;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices;
using AlphaCinemaServices.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaCinemaTests.AlphaCinemaServices.MovieServicesTests
{
    [TestClass]
    public class DeleteMovie_Should
    {
        [TestMethod]
        public void ThrowEntityDoesntExistException_WhenMovieDoesNotExist()
        {
            //Arrange
            var resultFromMovieRepo = new List<Movie>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var movieRepoMock = new Mock<IRepository<Movie>>();

            unitOfWork.Setup(x => x.Movies).Returns(movieRepoMock.Object);
            movieRepoMock.Setup(repo => repo.AllAndDeleted()).Returns(resultFromMovieRepo.AsQueryable());

            //Act and Assert
            var deleteMovieCommand = new MovieServices(unitOfWork.Object);
            Assert.ThrowsException<EntityDoesntExistException>(() => deleteMovieCommand.DeleteMovie("TestMovie"));
        }

        [TestMethod]
        public void ThrowEntityDoesntExistException_WhenMovieIsDeleted()
        {
            //Arrange
            var movieMock = new Mock<Movie>();
            movieMock.Object.IsDeleted = true;
            var resultFromMovieRepo = new List<Movie>() { movieMock.Object};
            var unitOfWork = new Mock<IUnitOfWork>();
            var movieRepoMock = new Mock<IRepository<Movie>>();

            unitOfWork.Setup(x => x.Movies).Returns(movieRepoMock.Object);
            movieRepoMock.Setup(repo => repo.AllAndDeleted()).Returns(resultFromMovieRepo.AsQueryable());

            //Act and Assert
            var deleteMovieCommand = new MovieServices(unitOfWork.Object);
            Assert.ThrowsException<EntityDoesntExistException>(() => deleteMovieCommand.DeleteMovie("TestMovie"));
        }

        [TestMethod]
        public void DeleteMovie_WhenMovieExists()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var movieMock = new Mock<Movie>();
            movieMock.Object.IsDeleted = false;
            var predifinedListOfMovies = new List<Movie>() { movieMock.Object };

            var movieRepoMock = new Mock<IRepository<Movie>>();
            movieRepoMock
                .Setup(repo => repo.Delete(It.IsAny<Movie>()))
                .Callback<Movie>((movie) =>
                {
                    predifinedListOfMovies.Remove(movieMock.Object);
                });


            unitOfWork.Setup(x => x.Movies).Returns(movieRepoMock.Object);
            movieRepoMock.Setup(repo => repo.AllAndDeleted()).Returns(predifinedListOfMovies.AsQueryable());

            var addMovieCommand = new MovieServices(unitOfWork.Object);
            addMovieCommand.DeleteMovie(movieMock.Object.Name);

            //Assert
            Assert.AreEqual(0, predifinedListOfMovies.Count);
        }
    }
}
