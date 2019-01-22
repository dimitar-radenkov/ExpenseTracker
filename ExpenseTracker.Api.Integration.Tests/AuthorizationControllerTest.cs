using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ExpenseTracker.Api.Attributes;
using ExpenseTracker.Api.Controllers;
using ExpenseTracker.Api.Models.BindingModels;
using ExpenseTracker.Api.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ExpenseTracker.Api.Integration.Tests
{
    [TestClass]
    public class AuthorizationControllerTest
    {
        [TestMethod]
        public void RegisterAsync_ShouldContainsValidateModelAttribute()
        {
            //arrange
            var authService = new Mock<IAuthService>();
            var controller = new AuthorizationController(authService.Object);

            //act
            var method = controller.GetType()
                .GetMethods()
                .Where(m => m.Name == nameof(controller.RegisterAsync))
                .Where(m => m.CustomAttributes.Any(a => a.AttributeType == typeof(ValidateModelAttribute)))
                .FirstOrDefault();

            //assert
            Assert.IsNotNull(method);
        }

        [TestMethod]
        public void RegisterAsync_WithValidBidingModel_ShouldReturnOk()
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
            var result = controller.RegisterAsync(bindingModel).Result as OkResult;

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
        }
    }
}
