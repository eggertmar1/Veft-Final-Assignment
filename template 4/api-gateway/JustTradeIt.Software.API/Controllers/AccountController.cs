using System.Linq;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustTradeIt.Software.API.Controllers
{
    [ApiController]
    [Route("api/account")]
    
        // TODO: Setup routes

        [Authorize]
        [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountService accountService, ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }

        //TODO: Fix register so that it fits with model
        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public IActionResult CreateUse([FromBody] RegisterInputModel register)
        {
            var user = _accountService.CreateUser(register);
            if (user == null) { return Unauthorized(); }
            return Ok(_tokenService.GenerateJwtToken(user));
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult AuthenticateUser([FromBody] LoginInputModel login)
        {
            var user = _accountService.AuthenticateUser(login);
            if (user == null) { return Unauthorized(); }
            return Ok(_tokenService.GenerateJwtToken(user));
        }


        //TODO: Fix get and put calls to profile 
        [HttpGet]
        [Route("profile")]
        public IActionResult GetProfileInformation()
        {
            var claims = User.Claims.Select(c => new
            {
                Type = c.Type,
                Value = c.Value
            });
            return Ok(claims);
        }

        [HttpPut]
        [Route("profile")]
        public IActionResult UpdateProfile()
        {
            var claims = User.Claims.Select(c => new
            {
                Type = c.Type,
                Value = c.Value
            });
            return Ok(claims);
        }


        [HttpGet]
        [Route("logout")]
        public IActionResult LogOut()
        {
            int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "tokenId").Value, out var tokenId);
            _accountService.Logout(tokenId);
            return NoContent();
        }

    }
    
}
