namespace SuperSimpleStocksApp
{
    public abstract class Stock
    {
        public abstract Enums.StockType Type { get; }
        public readonly string Symbol;
        public readonly decimal LastDividend;
        public decimal? FixedDividend { get; private set; }
        public decimal ParValue { get; private set; }
        private readonly IStockTrade _stockTrade;
        public decimal Price => _stockTrade.GetPrice(Symbol);

        protected Stock(string symbol, decimal lastDividend, decimal fixedDividend, int parValue, IStockTrade stockTrade)
        {
            Symbol = symbol;
            LastDividend = lastDividend;
            FixedDividend = fixedDividend;
            ParValue = parValue;
            _stockTrade = stockTrade;
        }

        public abstract decimal GetCalculateDividendYield();
        // ReSharper disable once InconsistentNaming
        public virtual decimal GetCalculatePERatio(IStockTrade stockTrade)
        {
            return Price/LastDividend; //?
        }
    }
}
