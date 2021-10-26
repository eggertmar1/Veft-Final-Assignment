using System;
using System.Linq;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustTradeIt.Software.API.Controllers
{
    [Authorize]
    [Route("api/trades")]
    [ApiController]
    public class TradeController : ControllerBase
    {
        private readonly ITradeService _tradeservice;
        private readonly IUserService _userService;

        public TradeController(ITradeService tradeService, IUserService userService)
        {
            _tradeservice = tradeService;
            _userService = userService; // might not have to be here idk..
        }
        //TODO: Require authentication FOR ALL ROUTES
        [HttpGet]
        [Route("")]
        public IActionResult GetTrades([FromBody] string email)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("")]
        public IActionResult CreateTradeRequest(string email, [FromBody] TradeInputModel tradeRequest)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{tradeIdentifier}", Name = "GetTradeByIdentifier")]
        public IActionResult GetTradeByIdentifier(string tradeIdentifier)
        {
            throw new NotImplementedException();
        }

        //TODO: FromBody placed in the right place? CONFIRM

        [HttpPut]
        [Route("{tradeIdentifier}", Name = "UpdateTradeRequest")]
        public IActionResult UpdateTradeRequest(string tradeIdentifier, [FromBody] string email, string status)
        {
            throw new NotImplementedException();
        }
    }
}