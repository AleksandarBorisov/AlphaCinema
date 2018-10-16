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

namespace AlphaCinemaTests.AlphaCinemaServices.GenreServicesTests
{
    [TestClass]
    public class GetID_Should
    {
        private Genre genre;
        private List<Genre> resultFromGenreRepo;
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IRepository<Genre>> genreRepoMock;
        private int testGenreId = 1;
        private string testGenreName = "Comedy";

        [TestInitialize]
        public void TestInitialize()
        {
            //Arrange 
            genre = new Genre
            {
                Id = testGenreId,
                Name = testGenreName,
                IsDeleted = false,
                MoviesGenres = new List<MovieGenre>(),
            };

            resultFromGenreRepo = new List<Genre>() { genre };
            unitOfWork = new Mock<IUnitOfWork>();
            genreRepoMock = new Mock<IRepository<Genre>>();
        }

        
        [TestMethod]
        public void ReturnCorrectGenreId_WhenGenreExists()
        {
            //Arrange 
            unitOfWork.Setup(x => x.Genres)
                .Returns(genreRepoMock.Object);

            genreRepoMock.Setup(repo => repo.AllAndDeleted())
                .Returns(resultFromGenreRepo.AsQueryable());
            
            //Act 
            var genreServices = new GenreServices(unitOfWork.Object);
            int result = genreServices.GetID(testGenreName);

            //Assert
            Assert.AreEqual(genre.Id, result);

        }

        [TestMethod]
        public void ThrowEntityDoesNotExistException_WhenGenreDoesNotExist()
        {
            //Act 
            var genreServices = new GenreServices(unitOfWork.Object);

            unitOfWork.Setup(x => x.Genres)
                .Returns(genreRepoMock.Object);

            genreRepoMock.Setup(repo => repo.AllAndDeleted())
                .Returns(resultFromGenreRepo.AsQueryable());

            //Assert
            Assert.ThrowsException<EntityDoesntExistException>(() 
                => genreServices.GetID("NonExistingGenre"));
        }

        [TestMethod]
        public void ThrowEntityDoesNotExistException_WhenGenreIsDeleted()
        {
            //Arrange
            genre.IsDeleted = true;

            unitOfWork.Setup(x => x.Genres)
                .Returns(genreRepoMock.Object);

            genreRepoMock.Setup(repo => repo.AllAndDeleted())
                .Returns(resultFromGenreRepo.AsQueryable());

            
            //Act 
            var genreServices = new GenreServices(unitOfWork.Object);

            
            //Assert
            Assert.ThrowsException<EntityDoesntExistException>(()
                => genreServices.GetID(genre.Name));
        }


    }
}
