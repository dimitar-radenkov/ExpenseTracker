using System.Threading.Tasks;
using ExpenseTracker.Api.Controllers;
using ExpenseTracker.Api.Models.BindingModels;
using ExpenseTracker.Api.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ExpenseTracker.Api.Tests.Integration
{
    [TestClass]
    public class AuthorizationTests
    {
        [TestMethod]
        public async void TestMethod1()
        {
            //arrange
            var authService = new Mock<IAuthService>();
            authService
                .Setup(x => x.RegisterAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var controller = new AuthorizationController(authService.Object);
            var bindingModel = new RegisterBindingModel
            {
                Email = "test@test.com",
                Password = "123456",
                ConfirmedPassword = "123456",
            };


            //act
            var result = await controller.RegisterAsync(bindingModel);


            //assert

        }
    }
}
