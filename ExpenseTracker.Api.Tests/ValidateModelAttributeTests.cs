using System.Collections.Generic;
using ExpenseTracker.Api.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ExpenseTracker.Api.Tests
{
    [TestClass]
    public class AuthTests
    {
        [TestMethod]
        public void ValidateModelAttribute_WithError_ShouldThrowsException()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var context = new ActionExecutingContext(
                new ActionContext
                {
                    HttpContext = httpContext,
                    RouteData = new RouteData(),
                    ActionDescriptor = new ActionDescriptor(),
                },
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                new Mock<Controller>().Object);

            context.ModelState.AddModelError("", "Error");

            var filter = new ValidateModelAttribute();

            //Act
            filter.OnActionExecuting(context);

            //Assert
            Assert.IsNotNull(context.Result);
            Assert.IsInstanceOfType(context.Result, typeof(IActionResult));
        }
    }
}
