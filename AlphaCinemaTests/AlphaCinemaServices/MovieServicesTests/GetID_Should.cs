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
using System.Text;

namespace AlphaCinemaTests.AlphaCinemaServices.MovieServicesTests
{
    [TestClass]
    public class GetID_Should
    {
        [TestMethod]
        public void ReturnCorrectMovie_WhenMovieExists()
        {
            //Arrange 
            var movie = new Movie
            {
                Id = 1,
                Name = "TestMovie",
                Description = "It is a movie for testing",
                Duration = 100,
                ReleaseYear = 2000,
                Projections = new List<Projection>(),
                MovieGenres = new List<MovieGenre>(),
                IsDeleted = false
            };

            var resultFromMovieRepo = new List<Movie>() { movie };

            var unitOfWork = new Mock<IUnitOfWork>();
            var movieRepoMock = new Mock<IRepository<Movie>>();

            unitOfWork.Setup(x => x.Movies).Returns(movieRepoMock.Object);
            movieRepoMock.Setup(repo => repo.AllAndDeleted()).Returns(resultFromMovieRepo.AsQueryable());

            //Act 
            var movieService = new MovieServices(unitOfWork.Object);
            var result = movieService.GetID("TestMovie");

            //Assert
            Assert.AreEqual(1, result);//Очевидно си ги създава с ID-та които започват от 0
        }

        [TestMethod]
        public void ThrowEntityDoesNotExistException_WhenMovieDoesNotExist()
        {
            //Arrange
            var movie = new Movie
            {
                Id = 1,
                Name = "TestMovie",
                Description = "It is a movie for testing",
                Duration = 100,
                ReleaseYear = 2000,
                Projections = new List<Projection>(),
                MovieGenres = new List<MovieGenre>(),
                IsDeleted = false
            };

            var resultFromMovieRepo = new List<Movie>() { movie };

            var unitOfWork = new Mock<IUnitOfWork>();
            var movieRepoMock = new Mock<IRepository<Movie>>();

            //Act 
            var movieService = new MovieServices(unitOfWork.Object);

            unitOfWork.Setup(x => x.Movies).Returns(movieRepoMock.Object);
            movieRepoMock.Setup(repo => repo.AllAndDeleted()).Returns(resultFromMovieRepo.AsQueryable());

            //Assert
            Assert.ThrowsException<EntityDoesntExistException>(() => movieService.GetID("NonExistingMovie"));
        }

    }
}
