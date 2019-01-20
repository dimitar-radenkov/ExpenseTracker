using System.Threading.Tasks;
using ExpenseTracker.Api.Controllers;
using ExpenseTracker.Api.Models.BindingModels;
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
            var bindingModel = new RegisterBindingModel
            {
                Email = "test@test.com",
                Password = "test123",
                ConfirmedPassword = "test123"
            };

            var authService = new Mock<IAuthService>();
            authService
                .Setup(x => x.RegisterAsync(
                    bindingModel.Email,
                    bindingModel.Password))
                .ReturnsAsync(new IdentityUser());

            var controller = new AuthorizationController(authService.Object);

            //act
            var result = controller.RegisterAsync(bindingModel)
                .ConfigureAwait(false)
                .GetAwaiter();

            //assert
            Assert.AreEqual(result, result);
        }
    }
}
