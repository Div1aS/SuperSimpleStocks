namespace SuperSimpleStocksApp
{
    public class TradeParams
    {
        public Enums.TradeType Type { get; set; }
        public string Symbol { get; set; }
        public decimal TradePrice { get; set; }
        public decimal Quantity { get; set; }
    }
}
