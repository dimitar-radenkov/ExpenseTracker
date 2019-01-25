using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using ExpenseTracker.Api.Attributes;
using ExpenseTracker.Api.Controllers;
using ExpenseTracker.Api.Models.BindingModels;
using ExpenseTracker.Api.Services.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ExpenseTracker.Api.Integration.Tests
{
    [TestClass]
    public class AuthorizationControllerTest
    {
        private TestServer testServer;
        private HttpClient testClient;

        private Mock<IAuthService> mockAuthService;

        [TestInitialize]
        public void Initialize()
        {
            this.mockAuthService = new Mock<IAuthService>();

            this.testServer = new TestServer(
                new WebHostBuilder()
                    .UseStartup<Startup>()
                    .ConfigureTestServices(services =>
                    {
                        services.AddSingleton(this.mockAuthService.Object);
                    }));

            this.testClient = this.testServer.CreateClient();
        }

        [TestMethod]
        public void RegisterAsync_WithValidBindingModel_ShouldReturnOk()
        {
            this.mockAuthService
                .Setup(x => x.RegisterAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new IdentityUser());

            var bindingModel = new RegisterBindingModel
            {
                Email = "test@test.com",
                Password = "test123",
                ConfirmedPassword = "test123"
            };

            var response = this.testClient.PostAsJsonAsync("api/authorization/register", bindingModel).Result;
            response.EnsureSuccessStatusCode();
        }

        [TestMethod]
        public void RegisterAsync_WithInvalidBindingModel_ShouldReturnBadRequest()
        {
            this.mockAuthService
                .Setup(x => x.RegisterAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new IdentityUser());

            var bindingModel = new RegisterBindingModel
            {
                Email = "",
                Password = "",
                ConfirmedPassword = ""
            };

            var response = this.testClient.PostAsJsonAsync("api/authorization/register", bindingModel).Result;

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void RegisterAsync_WhenAuthorizationServiceIsDown_ShouldReturnBadRequest()
        {
            this.mockAuthService
                .Setup(x => x.RegisterAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var bindingModel = new RegisterBindingModel
            {
                Email = "",
                Password = "",
                ConfirmedPassword = ""
            };

            var response = this.testClient.PostAsJsonAsync("api/authorization/register", bindingModel).Result;

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

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
    }
}
