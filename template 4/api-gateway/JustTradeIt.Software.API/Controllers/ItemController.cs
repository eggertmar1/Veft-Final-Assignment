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
    [Route("api/items")]
    [ApiController]
    public class ItemController : ControllerBase
    {

        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }
        //TODO: Require authentication FOR ALL ROUTES

        [HttpGet]
        [Route("")]
        public IActionResult GetItems([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1, [FromQuery] bool ascendingSortOrder = false)
        {
            throw new NotImplementedException();
        }
        
        [HttpGet]
        [Route("{identifier}", Name = "GetItemByIdentifier")]
        public IActionResult GetItemByIdentifier([FromRoute] string identifier)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("")]
        //TODO: string email, i'm not sure it's in the right place but no ERRORSS!!
        public IActionResult AddNewItem([FromBody] ItemInputModel item)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("{identifier}", Name = "RemoveItem")]
        public IActionResult RemoveItem([FromRoute] string identifier)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            throw new NotImplementedException();
        }


    }
}