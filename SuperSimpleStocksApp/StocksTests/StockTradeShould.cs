using System;
using FluentAssertions;
using NUnit.Framework;
using SuperSimpleStocksApp;

namespace StocksTests
{
    [TestFixture]
    public class StockTradeShould
    {
        private IStockTrade _stockTrades;
        private const string Symbol = "TEA";
        private TradeParams _tradeParams;

        [SetUp]
        public void Setup()
        {
            _stockTrades = StockTradesSingleton.Instance;
            _tradeParams = new TradeParams
            {
                Quantity = 100,
                Symbol = Symbol,
                TradePrice = 100,
                Type = Enums.TradeType.Sell
            };
        }

        [Test]
        public void CalculateGeometricMeanReturnResult()
        {
            //Act
            _stockTrades.MakeTrade(_tradeParams);
            _stockTrades.MakeTrade(_tradeParams);
            _stockTrades.MakeTrade(_tradeParams);
            _stockTrades.MakeTrade(_tradeParams);
            var geometricMean = _stockTrades.GetGBCEAllShareIndex();
            var equals = Math.Abs(geometricMean - 100d) <= Math.Abs(geometricMean * .00001);
            equals.Should().BeTrue();
        }

        [Test]
        public void GetCalculatedPriceReturnResult()
        {
            //Act
            _stockTrades.MakeTrade(_tradeParams);
            _stockTrades.MakeTrade(_tradeParams);
            _stockTrades.MakeTrade(_tradeParams);
            _stockTrades.MakeTrade(_tradeParams);
            var calculatedPrice = _stockTrades.GetCalculatedPrice(Symbol);
            //Assert
            calculatedPrice.Should().Be(100M);
        }
    }
}
