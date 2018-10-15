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
                Name = "TestMovie",
                Description = "It is a movie for testing",
                Duration = 100,
                ReleaseYear = 2000,
                Projections = new List<Projection>(),
                MovieGenres = new List<MovieGenre>(),
                IsDeleted = false
            };

            var movieRepoMock = new Mock<IRepository<Movie>>();
            //movieRepoMock.Object.Add(movie);

            var unitOfWork = new Mock<IUnitOfWork>();
            var resultFromMovieRepo = new List<Movie>() { movie };

            unitOfWork.Setup(x => x.Movies).Returns(movieRepoMock.Object);
            movieRepoMock.Setup(repo => repo.All()).Returns(resultFromMovieRepo.AsQueryable());
            movieRepoMock.Setup(repo => repo.AllAndDeleted()).Returns(resultFromMovieRepo.AsQueryable());

            //Act 
            var movieService = new MovieServices(unitOfWork.Object);
            var result = movieService.GetID("TestMovie");

            //Assert
            Assert.AreEqual(0, result);//Очевидно си ги създава с ID-та които започват от 0
        }

        public void ThrowEntityDoesNotExistException_WhenMovieDoesNotExist()
        {
            //Arrange
            var movieRepoMock = new Mock<IRepository<Movie>>();
            var unitOfWork = new Mock<IUnitOfWork>();

        }

    }
}
