namespace SuperSimpleStocksApp
{
    public abstract class Stock
    {
        public abstract Enums.StockType Type { get; }
        public string Symbol;
        public decimal LastDividend;
        public decimal? FixedDividend;
        public decimal ParValue;
        private IStockTrade _stockTrade;
        public decimal Price => _stockTrade.GetPrice(Symbol);

        protected Stock()
        {
        }

        public void SetStockProperties(string symbol, decimal lastDividend, decimal fixedDividend, int parValue, IStockTrade stockTrade)
        {
            Symbol = symbol;
            LastDividend = lastDividend;
            FixedDividend = fixedDividend;
            ParValue = parValue;
            _stockTrade = stockTrade;
        }

        public abstract decimal GetCalculateDividendYield();
        // ReSharper disable once InconsistentNaming
        public virtual decimal GetCalculatePERatio()
        {
            return Price/LastDividend; //?
        }
    }
}
