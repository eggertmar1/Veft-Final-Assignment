using System;
using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustTradeIt.Software.API.Controllers
{
    [Authorize]
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        [Route("{identifier}", Name = "GetUserInformation")]
        public IActionResult GetUserInformation([FromRoute] string identifier)
        {
            return Ok(_userService.GetUserInformation(identifier));
        }

        [HttpGet]
        [Route("{identifier}/trades", Name = "GetUserTrades")]
        public IActionResult GetUserTrades([FromRoute] string identifier)
        {
            return Ok(_userService.GetUserTrades(identifier));
        }
    }
}