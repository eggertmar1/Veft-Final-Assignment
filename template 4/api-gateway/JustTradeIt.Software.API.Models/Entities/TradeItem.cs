using System.Collections.Generic;

namespace JustTradeIt.Software.API.Models.Entities
{
    public class TradeItem
    {
        public int TradeId { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }

        //TODO: Check for navigation links
        
    }
}