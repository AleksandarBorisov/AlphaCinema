using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using AlphaCinemaData.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace AlphaCinemaTests.AlphaCinemaData.RepositoryTests
{
    [TestClass]
    public class Add_Should
    {
        [TestMethod]
        public void AddEntityToBase_WhenEntityIsCorrect()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
                .UseInMemoryDatabase(databaseName: "AddEntityToBase_WhenEntityIsCorrect")
                .Options;

            var testMovie = new Movie { Name = "TestMovie"};
            //Act
            using (var actContext = new AlphaCinemaContext(contextOptions))
            {
                var movieRepo = new Repository<Movie>(actContext);
                movieRepo.Add(testMovie);
                movieRepo.Save();
            }

            //Assert
            using (var assertContext = new AlphaCinemaContext(contextOptions))
            {
                Assert.IsTrue(assertContext.Movies.Count() == 1);
                Assert.IsTrue(assertContext.Movies.Contains(testMovie));
            }
        }
    }
}
