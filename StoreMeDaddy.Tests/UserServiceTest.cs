using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreMeDaddy.Models;
using StoreMeDaddy.Services;
using System.Threading.Tasks;

namespace StoreMeDaddy.Tests
{
    public class TestStoreMeDaddyContext : StoreMeDaddyContext
    {
        public TestStoreMeDaddyContext(DbContextOptions<StoreMeDaddyContext> options)
            : base(options)
        { }

        public TestStoreMeDaddyContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Override the SQLite configuration and use the In-Memory provider
            optionsBuilder.UseInMemoryDatabase("TestDatabase");
        }
    }

    [TestClass]
    public class UserServiceTests
    {
        [TestMethod]
        public async Task Authenticate_ValidCredentials_ReturnsUser()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TestStoreMeDaddyContext>()
                .UseInMemoryDatabase(databaseName: "Authenticate_ValidCredentials_ReturnsUser")
                .Options;

            using (var context = new TestStoreMeDaddyContext(options))
            {
                UserModel user = new ( "testuser", "p@ssword" );
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }

            using (var context = new TestStoreMeDaddyContext(options))
            {
                var service = new UserService(context);

                // Act
                var result = await service.Authenticate("testuser", "p@ssword");

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual("testuser", result.Username);
            }
        }

    }
}
