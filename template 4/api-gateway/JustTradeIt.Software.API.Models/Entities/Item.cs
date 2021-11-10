using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JustTradeIt.Software.API.Models.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string PublicIdentifier { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        // public int ItemConditionId { get; set; }
        public int OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public bool Deleted { get; set; }

        public int ItemConditionId { get; set; }
        [ForeignKey("OwnerId")]
        // Navigation properties
        // public List<ItemImage> ItemImages { get; set; }
        public User Owner { get; set; }
        public List<TradeItem> TradeItems { get; set; }
        public List<Trade> Trade { get; set; }
        public ItemCondition ItemCondition { get; set; }
    }
}