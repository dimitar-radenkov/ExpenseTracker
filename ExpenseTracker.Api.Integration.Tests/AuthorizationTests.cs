using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using ExpenseTracker.Api.Attributes;
using ExpenseTracker.Api.Controllers;
using ExpenseTracker.Api.Extensions;
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
    public class AuthorizationTests
    {
        private const string REGISTER_ENDPOINT = "api/authorization/register";
        private const string LOGIN_ENDPOINT = "api/authorization/login";

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
            //arrange
            this.mockAuthService
                .Setup(x => x.RegisterAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new IdentityUser());

            var bindingModel = new RegisterBindingModel
            {
                Email = "test@test.com",
                Password = "test123",
                ConfirmedPassword = "test123"
            };

            //act
            var response = this.testClient.PostAsJsonAsync(REGISTER_ENDPOINT, bindingModel).Result;

            //assert
            response.EnsureSuccessStatusCode();
        }

        [TestMethod]
        public void RegisterAsync_WithInvalidBindingModel_ShouldReturnBadRequest()
        {
            //arrange
            this.mockAuthService
                .Setup(x => x.RegisterAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new IdentityUser());

            var bindingModel = new RegisterBindingModel
            {
                Email = "",
                Password = "",
                ConfirmedPassword = ""
            };

            //act
            var response = this.testClient.PostAsJsonAsync(REGISTER_ENDPOINT, bindingModel).Result;

            //assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void RegisterAsync_WhenAuthorizationServiceIsDown_ShouldReturnBadRequest()
        {
            //arrange
            this.mockAuthService
                .Setup(x => x.RegisterAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var bindingModel = new RegisterBindingModel
            {
                Email = "",
                Password = "",
                ConfirmedPassword = ""
            };

            //act
            var response = this.testClient.PostAsJsonAsync(REGISTER_ENDPOINT, bindingModel).Result;

            //assert
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

        [TestMethod]
        public void LoginAsync_ShouldContainsValidateModelAttribute()
        {
            //arrange
            var authService = new Mock<IAuthService>();
            var controller = new AuthorizationController(authService.Object);

            //act
            var method = controller.GetType()
                .GetMethods()
                .Where(m => m.Name == nameof(controller.LoginAsync))
                .Where(m => m.CustomAttributes.Any(a => a.AttributeType == typeof(ValidateModelAttribute)))
                .FirstOrDefault();

            //assert
            Assert.IsNotNull(method);
        }

        [TestMethod]
        public void LoginAsync_WhenBindingModelIsInvalid_ShouldReturnBadRequest()
        {
            //arrange
            this.mockAuthService
                .Setup(x => x.LoginAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("token");

            var bindingModel = new LoginBindingModel
            {
                Email = "",
                Password = ""
            };

            //act
            var response = this.testClient.PostAsJsonAsync(LOGIN_ENDPOINT, bindingModel).Result;

            //assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void LoginAsync_WhenBindingModelIsValid_ShouldReturnToken()
        {
            //arrange
            var token = "token";
            this.mockAuthService
                .Setup(x => x.LoginAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(token);

            var bindingModel = new LoginBindingModel
            {
                Email = "test@testov.com",
                Password = "test123"
            };

            //act
            var response = this.testClient.PostAsJsonAsync(LOGIN_ENDPOINT, bindingModel).Result;
            var responeToken = response.Content.ReadAsStringAsync().Result;

            //assert
            response.EnsureSuccessStatusCode();
            Assert.IsTrue(responeToken.Contains(token));
        }
    }
}
