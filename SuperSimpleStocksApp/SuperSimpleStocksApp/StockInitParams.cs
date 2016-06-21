namespace SuperSimpleStocksApp
{
    public class StockInitParams
    {
        public string Symbol { get; set; }
        public decimal LastDividend { get; set; }
        public decimal FixedDividend { get; set; }
        public int ParValue { get; set; }
        public Enums.StockType Type { get; set; }
    }
}
