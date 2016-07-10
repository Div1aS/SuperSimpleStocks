namespace SuperSimpleStocksApp
{
    public class PreferredStock : Stock
    {
        public override Enums.StockType Type => Enums.StockType.Preferred;

        public override decimal GetCalculateDividendYield()
        {
            if (!FixedDividend.HasValue)
            {
                return 0;
            }
            if (FixedDividend.Value == 0 || ParValue == 0 || Price == 0)
            {
                return 0;
            }
            return FixedDividend.Value * ParValue / Price;
        }

    }
}
