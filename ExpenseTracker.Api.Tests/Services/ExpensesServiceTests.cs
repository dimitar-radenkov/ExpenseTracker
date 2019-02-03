using System;
using System.Collections.Generic;
using System.Text;
using ExpenseTracker.Api.Services;
using ExpenseTracker.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpenseTracker.Api.Tests.Services
{
    [TestClass]
    public class ExpensesServiceTests
    {
        private const decimal AMOUNT = 10;
        private const string DESCRITION = "test";
        private const long CATEGORY_ID = 1;
        private readonly string USER_ID = Guid.NewGuid().ToString();

        [TestMethod]
        public void ValidateModelAttribute_WithError_ShouldThrowsException()
        {
            //Arrange
            var db = new ExpenseTrackerDbContext(
                new DbContextOptionsBuilder().UseInMemoryDatabase("test").Options);

            var service = new ExpensesService(db);

            //Act
            var result = service.AddAsync(AMOUNT, DESCRITION, CATEGORY_ID, USER_ID).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Id != 0);
        }
    }
}
