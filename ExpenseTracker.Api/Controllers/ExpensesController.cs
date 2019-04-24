using ExpenseTracker.Api.Attributes;
using ExpenseTracker.Api.Services.Contracts;
using ExpenseTracker.Common.Models.BindingModels.Expenses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExpenseTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpensesService expensesService;
        private readonly IUserResolverService userResolverService;

        public ExpensesController(
            IExpensesService expensesService,
            IUserResolverService userResolverService)
        {
            this.expensesService = expensesService;
            this.userResolverService = userResolverService;
        }

        [HttpGet("getall")]
        [ValidateModel]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var userId = this.userResolverService.User.FindFirst(ClaimTypes.Name).Value;
                var expenses = await this.expensesService.GetAllAsync(userId);

                return this.Ok(expenses);
            }
            catch (Exception e)
            {
                return this.BadRequest("Unable to get expenses, please contact support");
            }
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> AddAsync(AddExpenseBindingModel bindingModel)
        {
            try
            {
                var userId = this.userResolverService.User.FindFirst(ClaimTypes.Name).Value;

                var expense = await this.expensesService.AddAsync(
                    bindingModel.Amount,
                    bindingModel.Description,
                    bindingModel.CategoryId,
                    userId);

                return this.Created("", expense);
            }
            catch (Exception e)
            {
                return this.BadRequest("Unable to add expense, please contact support");
            }
        }

        [HttpPut]
        [ValidateModel]
        public async Task<IActionResult> UpdateAsync(UpdateExpenseBindingModel bindingModel)
        {
            try
            {
                var userId = this.userResolverService.User.FindFirst(ClaimTypes.Name).Value;

                await this.expensesService.UpdateAsync(
                    bindingModel.ExpenseId,
                    bindingModel.Amount,
                    bindingModel.Description,
                    bindingModel.CategoryId,
                    userId);

                return this.NoContent();
            }
            catch (Exception e)
            {
                return this.BadRequest("Unable to update expense, please contact support");
            }
        }

        [HttpDelete("{expenseId}")]
        [ValidateModel]
        public async Task<IActionResult> DeleteAsync(long expenseId)
        {
            try
            {
                await this.expensesService.DeleteAsync(expenseId);

                return this.NoContent();
            }
            catch (Exception e)
            {
                return this.BadRequest("Unable to delete expense, please contact support");
            }
        }
    }
}
