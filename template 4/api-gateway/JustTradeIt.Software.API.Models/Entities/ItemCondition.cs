using System.Collections.Generic;

namespace JustTradeIt.Software.API.Models.Entities
{
    public class ItemCondition
    {
        public int Id { get; set; }
        public string ConditionCode { get; set; }
        public string Description { get; set; }

        // Navigation properties
        // TODO: Find out if navigation properties are correct.
        public List<Item> Items { get; set; }
    }
}