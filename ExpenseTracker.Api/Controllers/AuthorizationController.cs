using System;
using System.Threading.Tasks;
using ExpenseTracker.Api.Models.BindingModels;
using ExpenseTracker.Api.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly ILoginService loginService;

        public AuthorizationController(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterBindingModel registerBindingModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            try
            {
                await this.loginService.RegisterAsync(
                    registerBindingModel.Email,
                    registerBindingModel.Password);

                var routeObject = new LoginBindingModel
                {
                    Email = registerBindingModel.Email,
                    Password = registerBindingModel.Password
                };

  
                return this.Ok();
            }
            catch (Exception e)
            {
                return this.BadRequest("Wrong username or password");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginBindingModel loginBindingModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            try
            {
                var token = await this.loginService.LoginAsync(
                    loginBindingModel.Email,
                    loginBindingModel.Password);

                return this.Ok(new { Token = token });
            }
            catch (Exception)
            {
                return this.BadRequest("Wrong username or password");
            }
        }
    }
}