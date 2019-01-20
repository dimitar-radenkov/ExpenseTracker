using System.Threading.Tasks;
using ExpenseTracker.Api.Controllers;
using ExpenseTracker.Api.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ExpenseTracker.Api.Integration.Tests
{
    [TestClass]
    public class AuthorizationControllerTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            //arrange
            var authService = new Mock<IAuthService>();
            authService
                .Setup(x => x.RegisterAsync("", ""))
                .ReturnsAsync(new IdentityUser());

            var controller = new AuthorizationController(authService.Object);

            //act

            //assert
        }
    }
}
