using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using AlphaCinemaData.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlphaCinemaTests.AlphaCinemaData.RepositoryTests
{
    [TestClass]
    public class SaveChanges_Should
    {
        [TestMethod]
        public void ChangeStateToUnchanged_WhenEntityIsAdded()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
                .UseInMemoryDatabase(databaseName: "ChangeStateToAdded_WhenEntityIsModified")
                .Options;

            var testMovie = new Movie { Name = "TestMovie" };

            //Act
            using (var context = new AlphaCinemaContext(contextOptions))
            {
                var movieRepo = new Repository<Movie>(context);
                movieRepo.Add(testMovie);
                context.SaveChanges();
                EntityEntry entry = context.Entry(testMovie);
                //Assert
                Assert.AreEqual(EntityState.Unchanged, entry.State);
            }
        }
    }
}
