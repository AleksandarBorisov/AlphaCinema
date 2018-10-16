using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using AlphaCinemaData.Models.Associative;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinemaTests.AlphaCinemaServices.CityServicesTests
{
    [TestClass]
    public class GetGenreNamesByCityId_Should
    {
        private City testCity;
        private int testCityId = 1;
        private string testCityName = "TestCity";
        private MovieGenre testMovieGenre;
        private Movie testMovie;
        private int testMovieId = 1;
        private string testMovieName = "TestMovie";
        private Genre testGenre;
        private int testGenreId = 1;
        private string testGenreName = "TestGenre";
        private Projection testProjection;

        [TestInitialize]
        public void TestInitialize()//Този метод се изпълнява преди извикване на всеки един от другите методи в този клас
        {
            testCity = new City { Id = testCityId, Name = testCityName };
            testMovieGenre = new MovieGenre { MovieId = testMovieId, GenreId = testGenreId };
            testMovie = new Movie
            {
                Id = testMovieId,
                Name = testMovieName,
                MovieGenres = new List<MovieGenre>() { testMovieGenre },
            };
            testGenre = new Genre { Id = testGenreId, Name = testGenreName };
            testProjection = new Projection
            {
                City = testCity,
                Movie = testMovie
            };
            
        }

        [TestMethod]
        public void ReturnCollectionOfGenreNames_WhenTheyExist()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
                .UseInMemoryDatabase(databaseName: "ReturnCollectionOfGenreNames_WhenTheyExist")
                .Options;

            using (var actContext = new AlphaCinemaContext(contextOptions))
            {
                actContext.Cities.Add(testCity);
                actContext.MoviesGenres.Add(testMovieGenre);
                actContext.Movies.Add(testMovie);
                actContext.Genres.Add(testGenre);
                actContext.Projections.Add(testProjection);
                actContext.SaveChanges();
            }

            //Act and Assert
            using (var assertContext = new AlphaCinemaContext(contextOptions))
            {
                var unitOfWork = new UnitOfWork(assertContext);
                var cityService = new CityServices(unitOfWork);
                var result = cityService.GetGenreNames(testCityId);
                Assert.IsTrue(result.Count == 1);
                Assert.AreEqual(result.First(), testGenreName);
            }
        }
    }
}
