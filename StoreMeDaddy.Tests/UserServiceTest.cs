using Microsoft.EntityFrameworkCore;
using Moq;
using StoreMeDaddy.Models;
using StoreMeDaddy.Services;

namespace StoreMeDaddy.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        [TestMethod]
        public async Task Authenticate_ValidCredentials_ReturnsUser()
        {
            // Arrange
            UserModel user = new ("testUser", "p@ssword");

            IQueryable<UserModel> users = new List<UserModel> { user }.AsQueryable();

            Mock<DbSet<UserModel>> mockSet = new();
            mockSet.As<IQueryable<UserModel>>().Setup(m => m.Provider).Returns(users.Provider);
            mockSet.As<IQueryable<UserModel>>().Setup(m => m.Expression).Returns(users.Expression);
            mockSet.As<IQueryable<UserModel>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockSet.As<IQueryable<UserModel>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            var mockContext = new Mock<StoreMeDaddyContext>();
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var service = new UserService(mockContext.Object);

            // Act
            var result = await service.Authenticate("testUser", "p@ssword");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("testUser", result.Username);
        }
    }
}
