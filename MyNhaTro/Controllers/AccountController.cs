using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNhaTro.Contracts;
using MyNhaTro.Models;
using MyNhaTro.Repositories;

namespace MyNhaTro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository AccountRepository;

        public AccountController(IAccountRepository repo) 
        {
            AccountRepository = repo;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult>SignUp(SignUpModel signUpModel)
        {
            var result = await AccountRepository.SignUpAsync(signUpModel);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }

            return Unauthorized();
        }
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInModel signInModel)
        {
            var result = await AccountRepository.SignInAsync(signInModel);
            if(string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }
            return Ok(result);
        }

       
    }
}
