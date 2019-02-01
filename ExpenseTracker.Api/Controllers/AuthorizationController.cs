using System;
using System.Threading.Tasks;
using ExpenseTracker.Api.Attributes;
using ExpenseTracker.Api.Models.BindingModels;
using ExpenseTracker.Api.Models.Responses;
using ExpenseTracker.Api.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthorizationController(IAuthService loginService)
        {
            this.authService = loginService;
        }

        [HttpPost("register")]
        [ValidateModel]
        public async Task<IActionResult> RegisterAsync(RegisterBindingModel registerBindingModel)
        {
            try
            {
                await this.authService.RegisterAsync(
                    registerBindingModel.Email,
                    registerBindingModel.Password);

                return this.Ok();
            }
            catch (Exception e)
            {
                return this.BadRequest("Unable to register, please contact support");
            }
        }

        [HttpPost("login")]
        [ValidateModel]
        public async Task<IActionResult> LoginAsync(LoginBindingModel loginBindingModel)
        {
            try
            {
                var token = await this.authService.LoginAsync(
                    loginBindingModel.Email,
                    loginBindingModel.Password);

                return this.Ok(new LoginResponse { Token = token });
            }
            catch (Exception)
            {
                return this.BadRequest("Wrong username or password");
            }
        }
    }
}