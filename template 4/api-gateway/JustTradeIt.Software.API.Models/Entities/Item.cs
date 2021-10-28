using System.Collections.Generic;

namespace JustTradeIt.Software.API.Models.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public int ItemConditionId { get; set; }
        public int OwnerId { get; set; }
        public bool Deleted { get; set; }

        // Navigation properties
        // TODO: Find out if navigation properties are correct.
        public List<ItemImage> ItemImages { get; set; }
        public List<TradeItem> TradeItems { get; set; }
        public List<Trade> Trade { get; set; }
    }
}