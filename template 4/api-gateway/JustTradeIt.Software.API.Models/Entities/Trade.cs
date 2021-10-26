using System;
using System.Collections.Generic;

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
        public int ReceiverId { get; set; }  // TODO: Maybe: ReceiverIdentifier
        public int SenderId { get; set; }  // TODO: Maybe: SenderIdentifier
                                           // This is according to diagram

        // Navigation properties
        // TODO: Find out if navigation properties are correct.
        public List<TradeItem> TradeItems { get; set; }
    }
}