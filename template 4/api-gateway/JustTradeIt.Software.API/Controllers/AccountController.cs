using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustTradeIt.Software.API.Controllers
{
    [ApiController]
    [Route("api/account")]
    [Authorize]
    /// <summary> Authorized for all endpoints except for the login and register endpoint. </summary>
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountService accountService, ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public IActionResult CreateUser([FromBody] RegisterInputModel register)
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

        [HttpGet]
        [Route("profile")]
        public IActionResult GetProfileInformation()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            
            return Ok(_accountService.GetProfileInformation(email));
        }

        [HttpPut]
        [Route("profile")]
        public async Task<IActionResult> UpdateProfile([FromForm] ProfileInputModel profile)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            await _accountService.UpdateProfile(email, profile);
            return Ok();
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
