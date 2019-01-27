using System;
using System.Threading.Tasks;
using ExpenseTracker.Api.Attributes;
using ExpenseTracker.Api.Models.BindingModels;
using ExpenseTracker.Api.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpensesService expensesService;
        private readonly UserManager<IdentityUser> userManager;

        public ExpensesController(
            IExpensesService expensesService,
            UserManager<IdentityUser> userManager)
        {
            this.expensesService = expensesService;
            this.userManager = userManager;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> AddAsync(AddExpenseBindingModel bindingModel)
        {
            try
            {
                var userId = this.userManager.GetUserId(this.User);

                var expense = await this.expensesService.AddAsync(
                    bindingModel.Amount,
                    bindingModel.Description,
                    bindingModel.CategoryId,
                    userId);

                return this.Ok();
            }
            catch (Exception e)
            {
                return this.BadRequest("Unable to add expense, please contact support");
            }
        }
    }
}