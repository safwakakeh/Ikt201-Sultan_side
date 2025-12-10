using System.Collections.Generic;

namespace Ikt201_Sultan_side.Models
{
    public class ReceiptItem
    {
        public string Name { get; set; } = "";
        public long Quantity { get; set; }
        public long Amount { get; set; } // i øre
    }

    public class ReceiptViewModel
    {
        public string SessionId { get; set; } = "";
        public long TotalAmount { get; set; } // i øre
        public string Currency { get; set; } = "nok";
        public List<ReceiptItem> Items { get; set; } = new();
    }
}