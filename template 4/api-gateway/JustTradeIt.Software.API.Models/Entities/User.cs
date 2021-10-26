using System.Collections.Generic;

namespace JustTradeIt.Software.API.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string  PublicIdentifier { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ProfileImageUrl { get; set; }
        public string HashedPassword { get; set; }

        // Navigation properties
        // TODO: Find out if navigation properties are correct.
        public List<Item> Items { get; set; }
        public List<TradeItem> TradeItems { get; set; }
        public List<Trade> Trades { get; set; }
    }
}