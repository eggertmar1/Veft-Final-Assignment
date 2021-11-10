using System.ComponentModel.DataAnnotations.Schema;

namespace JustTradeIt.Software.API.Models.Entities
{
    public class TradeItem
    {
        public int TradeId { get; set; }
        [ForeignKey("TradeId")]
        public virtual Trade Trade { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public int ItemId { get; set; }
        [ForeignKey("ItemId")]
        public virtual Item Item { get; set; }
    }
}