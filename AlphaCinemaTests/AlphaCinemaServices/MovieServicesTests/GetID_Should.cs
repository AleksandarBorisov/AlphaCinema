using AlphaCinemaData.Models;
using AlphaCinemaData.Models.Associative;
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
    public class GetID_Should
    {
        private Movie movie;
        private List<Movie> resultFromMovieRepo;
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
            resultFromMovieRepo = new List<Movie>() { movie };
            unitOfWork = new Mock<IUnitOfWork>();
            movieRepoMock = new Mock<IRepository<Movie>>();
        }

        [TestMethod]
        public void ReturnCorrectMovieId_WhenMovieExists()
        {
            //Arrange 
            unitOfWork.Setup(x => x.Movies).Returns(movieRepoMock.Object);
            movieRepoMock.Setup(repo => repo.AllAndDeleted()).Returns(resultFromMovieRepo.AsQueryable());

            //Act 
            var movieService = new MovieServices(unitOfWork.Object);
            var result = movieService.GetID(movie.Name);

            //Assert
            Assert.AreEqual(movie.Id, result);
        }

        [TestMethod]
        public void ThrowEntityDoesNotExistException_WhenMovieDoesNotExist()
        {
            //Act 
            var movieService = new MovieServices(unitOfWork.Object);

            unitOfWork.Setup(x => x.Movies).Returns(movieRepoMock.Object);
            movieRepoMock.Setup(repo => repo.AllAndDeleted()).Returns(resultFromMovieRepo.AsQueryable());

            //Assert
            Assert.ThrowsException<EntityDoesntExistException>(() => movieService.GetID("NonExistingMovie"));
        }

        [TestMethod]
        public void ThrowEntityDoesNotExistException_WhenMovieIsDeleted()
        {
            //Arrange 
            movie.IsDeleted = true;
            unitOfWork.Setup(x => x.Movies).Returns(movieRepoMock.Object);
            movieRepoMock.Setup(repo => repo.AllAndDeleted()).Returns(resultFromMovieRepo.AsQueryable());

            //Act 
            var movieService = new MovieServices(unitOfWork.Object);

            //Assert
            Assert.ThrowsException<EntityDoesntExistException>(() => movieService.GetID(movie.Name));
        }
    }
}
