using System;
using System.Threading.Tasks;
using ExpenseTracker.Api.Attributes;
using ExpenseTracker.Api.Models.BindingModels;
using ExpenseTracker.Api.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpensesService expensesService;

        public ExpensesController(IExpensesService expensesService)
        {
            this.expensesService = expensesService;
        }

        [HttpPost()]
        [ValidateModel]
        public async Task<IActionResult> AddAsync(AddExpenseBindingModel registerBindingModel)
        {
            try
            {
                //await this.expensesService.AddAsync(
                //    registerBindingModel.Email,
                //    registerBindingModel.Password);

                return this.Ok();
            }
            catch (Exception e)
            {
                return this.BadRequest("Unable to add expense, please contact support");
            }
        }
    }
}