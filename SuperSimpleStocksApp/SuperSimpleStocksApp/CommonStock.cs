namespace SuperSimpleStocksApp
{
    public class CommonStock : Stock
    {
        public override Enums.StockType Type => Enums.StockType.Common;

        public override decimal GetCalculateDividendYield()
        {
            if (LastDividend == 0 || Price == 0)
            {
                return 0;
            }

            return LastDividend / Price;
        }
    }
}
