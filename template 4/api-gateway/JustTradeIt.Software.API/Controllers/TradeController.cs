using System;
using System.Linq;
using System.Security.Claims;
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
        public IActionResult GetTrades([FromQuery] bool onlyCompleted, [FromQuery] bool onlyIncludeActive)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("")]
        public IActionResult CreateTradeRequest([FromBody] TradeInputModel tradeRequest)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var trade = _tradeservice.CreateTradeRequest(email, tradeRequest);
            return CreatedAtRoute("GetItemByIdentifier", new {identifier = trade}, null);
        }

        [HttpGet]
        [Route("{tradeIdentifier}", Name = "GetTradeByIdentifier")]
        public IActionResult GetTradeByIdentifier([FromRoute] string tradeIdentifier)
        {
            var trade = _tradeservice.GetTradeByIdentifer(tradeIdentifier);
            return Ok(trade);
        }

        //TODO: FromBody placed in the right place? CONFIRM

        [HttpPut]
        [Route("{tradeIdentifier}", Name = "UpdateTradeRequest")]
        public IActionResult UpdateTradeRequest([FromRoute] string tradeIdentifier, [FromBody] string email, string status)
        {
            throw new NotImplementedException();
        }
    }
}