using System;

namespace SuperSimpleStocksApp
{
    public class StockTrade
    {
        public Enums.TradeType Type { get; set; }
        public decimal TradePrice { get; set; }
        public decimal Quantity { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Symbol { get; set; }
    }
}
