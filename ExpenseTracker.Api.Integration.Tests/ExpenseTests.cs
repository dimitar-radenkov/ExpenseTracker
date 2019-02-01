using System.Net.Http;
using ExpenseTracker.Api.Models.BindingModels;
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
        private const string REGISTER_ENDPOINT = "api/authorization/register";
        private const string LOGIN_ENDPOINT = "api/authorization/login";

        private const string GETALL_ENDPOINT = "api/expenses/getall";

        private TestServer testServer;
        private HttpClient testClient;

        private string token;

        private Mock<IExpensesService> mockExpenseService;

        [TestInitialize]
        public void Initialize()
        {
            this.mockExpenseService = new Mock<IExpensesService>();

            this.testServer = new TestServer(
                new WebHostBuilder()
                    .UseStartup<Startup>()
                    .ConfigureTestServices(services =>
                    {
                        services.AddDbContext<ExpenseTrackerDbContext>(options => 
                        {
                            options.UseInMemoryDatabase("test");
                        });
                    }));

            this.testClient = this.testServer.CreateClient();
            //this.Register();
            //this.token = this.Login();          
        }

        [TestMethod]
        public void GetAll_WhenInvoked_ShouldCallGetAllASync()
        {        
            //act
            var response = this.testClient.GetAsync(GETALL_ENDPOINT).Result;

            //assert
            response.EnsureSuccessStatusCode();
        }

        private void Register()
        {
            var bindingModel = new RegisterBindingModel
            {
                Email = "test@test.com",
                Password = "test123",
                ConfirmedPassword = "test123"
            };
           
            var response = this.testClient.PostAsJsonAsync(REGISTER_ENDPOINT, bindingModel).Result;
            response.EnsureSuccessStatusCode();
        }

        private string Login()
        {
            var bindingModel = new LoginBindingModel
            {
                Email = "test@testov.com",
                Password = "test123"
            };

            //act
            var response = this.testClient.PostAsJsonAsync(LOGIN_ENDPOINT, bindingModel).Result;
            var responeToken = response.Content.ReadAsStringAsync().Result;

            return responeToken;
        }
    }
}

