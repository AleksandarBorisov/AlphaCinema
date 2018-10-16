using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using AlphaCinemaData.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace AlphaCinemaTests.AlphaCinemaData.RepositoryTests
{
    [TestClass]
    public class Update_Should
    {
        [TestMethod]
        public void MarkEntityAsModified_WhenCalled()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
                .UseInMemoryDatabase(databaseName: "MarkEntityAsModified_WhenCalled")
                .Options;

            var testMovie = new Movie { Name = "TestMovie" };

            //Act
            using (var context = new AlphaCinemaContext(contextOptions))
            {
                var movieRepo = new Repository<Movie>(context);
                movieRepo.Update(testMovie);
                EntityEntry entry = context.Entry(testMovie);
                //Assert
                Assert.AreEqual(EntityState.Modified, entry.State);
            }
        }

        [TestMethod]
        public void AttachNewEntityToContext_WhenCalled()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
                .UseInMemoryDatabase(databaseName: "MarkEntityAsModified_WhenCalled")
                .Options;

            var testMovie = new Movie { Name = "TestMovie" };

            //Act
            using (var context = new AlphaCinemaContext(contextOptions))
            {
                var movieRepo = new Repository<Movie>(context);
                movieRepo.Update(testMovie);
                //Assert
                Assert.IsTrue(context.Set<Movie>().Local.Any(e => e == testMovie));
            }
        }
    }
}
