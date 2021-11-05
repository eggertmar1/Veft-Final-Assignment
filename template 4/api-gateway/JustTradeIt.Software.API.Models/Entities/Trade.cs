using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JustTradeIt.Software.API.Models.Entities
{
    public class Trade
    {
        public int Id { get; set; }
        public string PublicIdentifier { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string TradeStatus { get; set; }
        public int ReceiverId { get; set; }  
        public int SenderId { get; set; } 

        // Navigation properties
        // [ForeignKey("SenderId")]
        // public virtual User Sender { get; set; }
        // [ForeignKey("ReceiverId")] 
        // public virtual User Receiver { get; set; }
        public List<TradeItem> TradeItems { get; set; }
    }
}