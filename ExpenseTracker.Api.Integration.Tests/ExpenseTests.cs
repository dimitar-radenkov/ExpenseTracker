using ExpenseTracker.Api.Services.Contracts;
using ExpenseTracker.Common.Models.BindingModels;
using ExpenseTracker.Common.Models.BindingModels.Expenses;
using ExpenseTracker.Common.Models.Responses;
using ExpenseTracker.Storage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;

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
                        services.AddSingleton(this.mockExpenseService.Object);
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
            var response = this.testClient.GetAsync(TestContants.EXPENSES_GETALL_ENDPOINT).Result;

            //assert
            response.EnsureSuccessStatusCode();
        }

        [TestMethod]
        public void Post_WhenInvoked_ShouldCallAddAsync()
        {
            //arrange
            this.mockUserResolverService
                .Setup(x => x.User)
                .Returns(new ClaimsPrincipal(new GenericIdentity(Guid.NewGuid().ToString())));

            var bindindModel = new AddExpenseBindingModel
            {
                Amount = 100,
                CategoryId = 1,
                Description = "test1"
            };

            //act
            var response = this.testClient
                .PostAsJsonAsync(TestContants.EXPENSES_ENDPOINT, bindindModel).Result;

            //assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            this.mockExpenseService.Verify(x =>
                x.AddAsync(It.IsAny<decimal>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<string>()), 
                Times.Once);
        }

        [TestMethod]
        public void Put_WhenInvoked_ShouldCallUpdateAsync()
        {
            //arrange
            this.mockUserResolverService
                .Setup(x => x.User)
                .Returns(new ClaimsPrincipal(new GenericIdentity(Guid.NewGuid().ToString())));

            var bindindModel = new UpdateExpenseBindingModel
            {
                ExpenseId = 1,
                Amount = 100,
                CategoryId = 1,
                Description = "test1"
            };

            //act
            var response = this.testClient
                .PutAsJsonAsync(TestContants.EXPENSES_ENDPOINT, bindindModel).Result;

            //assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
            this.mockExpenseService.Verify(x =>
                x.UpdateAsync(It.IsAny<long>(), It.IsAny<decimal>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<string>()),
                Times.Once);              
        }

        [TestMethod]
        public void Delete_WhenInvoked_ShouldCallDeleteAsync()
        {
            //arrange
            this.mockUserResolverService
                .Setup(x => x.User)
                .Returns(new ClaimsPrincipal(new GenericIdentity(Guid.NewGuid().ToString())));

            long expenseId = 1;
            //act
            var response = this.testClient
                .DeleteAsync($"{TestContants.EXPENSES_ENDPOINT}{expenseId}").Result;

            //assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
            this.mockExpenseService.Verify(x =>
                x.DeleteAsync(It.IsAny<long>()),
                Times.Once);
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

