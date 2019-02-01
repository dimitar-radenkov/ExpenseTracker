using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using ExpenseTracker.Api.Models.BindingModels;
using ExpenseTracker.Api.Models.Responses;
using ExpenseTracker.Api.Services.Contracts;
using ExpenseTracker.Storage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ExpenseTracker.Api.Integration.Tests
{
    [TestClass]
    public class ExpenseTests
    {
        private TestServer testServer;
        private HttpClient testClient;

        private string token;

        private Mock<IExpensesService> mockExpenseService;
        private Mock<IUserResolverService> mockUserResolverService;

        [TestInitialize]
        public void Initialize()
        {
            this.mockExpenseService = new Mock<IExpensesService>();
            this.mockUserResolverService = new Mock<IUserResolverService>();

            this.testServer = new TestServer(
                new WebHostBuilder()
                    .UseStartup<Startup>()
                    .ConfigureServices(services =>
                    {
                        services.AddDbContext<ExpenseTrackerDbContext>(options =>
                        {
                            options.UseInMemoryDatabase("test");
                        });
                    })
                    .ConfigureTestServices(services => 
                    {
                        services.AddSingleton(this.mockUserResolverService.Object);
                    }));

            this.testClient = this.testServer.CreateClient();
            this.Register();
            this.Login();
            this.testClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {this.token}");
        }

        [TestMethod]
        public void GetAll_WhenInvoked_ShouldCallGetAllASync()
        {
            //arrange
            this.mockUserResolverService
                .Setup(x => x.User)
                .Returns(new ClaimsPrincipal(new GenericIdentity(Guid.NewGuid().ToString())));

            //act
            var response = this.testClient.GetAsync(TestContants.GETALL_ENDPOINT).Result;

            //assert
            response.EnsureSuccessStatusCode();
        }

        private void Register()
        {
            var bindingModel = new RegisterBindingModel
            {
                Email = TestContants.USER_EMAIL,
                Password = TestContants.USER_PASS,
                ConfirmedPassword = TestContants.USER_PASS
            };
           
            var response = this.testClient.PostAsJsonAsync(TestContants.REGISTER_ENDPOINT, bindingModel).Result;
            response.EnsureSuccessStatusCode();
        }

        private void Login()
        {
            var bindingModel = new LoginBindingModel
            {
                Email = TestContants.USER_EMAIL,
                Password = TestContants.USER_PASS
            };

            //act
            var response = this.testClient.PostAsJsonAsync(TestContants.LOGIN_ENDPOINT, bindingModel).Result;
            var responeToken = response.Content.ReadAsAsync<LoginResponse>().Result;

            this.token = responeToken.Token;
        }
    }
}

