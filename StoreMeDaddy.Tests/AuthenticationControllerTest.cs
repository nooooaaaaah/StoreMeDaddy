using StoreMeDaddy.Models;
using StoreMeDaddy.Controllers;
using Moq;
using StoreMeDaddy.Services;
using CoinMachine;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace StoreMeDaddy.Tests
{
    [TestClass]
    public class AuthenticationControllerTests
    {
        [TestMethod]
        public async Task Authenticate_ReturnsToken_WhenCredentialsAreValid()
        {
            // Arrange
            Mock<IUserService> userServiceMock = new();
            userServiceMock.Setup(x => x.Authenticate(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new UserModel("ValidUsername", "V@lidPassword", "user"));

            string secretKey = "your-secret-key-with-16bytes"; 
            string issuer = "your-issuer";
            int expiryMinutes = 60;
            ITokenService tokenService = new TokenService(secretKey, issuer, expiryMinutes, new List<string>() { "user" });

            AuthenticationController controller = new(tokenService, userServiceMock.Object);

            AuthenticationModel validCredentials = new("ValidUsername", "V@lidPassword");

            // Act
            IActionResult result = await controller.Authenticate(validCredentials);

            // Assert
            OkObjectResult okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            JObject tokenResponse = JObject.FromObject(okResult.Value);
            Assert.IsNotNull(tokenResponse["Token"]); // Make sure to use the correct property name for the token
        }


    }
}