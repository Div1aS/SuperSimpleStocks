using FluentAssertions;
using NUnit.Framework;
using SuperSimpleStocksApp;

namespace StocksTests
{
    [TestFixture]
    public class CommonStockShould
    {
        private StockFactory _stockFactory;
        private IStockTrade _stockTrades;
        private Stock _commonStock;
        private const string Symbol = "TEA";
        private TradeParams _tradeParams;

        [SetUp]
        public void Setup()
        {
            _stockFactory = new ConcreteStockFactory();
            _stockTrades = StockTradesSingleton.Instance;
            _commonStock = new CreateStock().Get(_stockFactory, new StockInitParams { FixedDividend = 0, LastDividend = 10, ParValue = 100, Symbol = Symbol, Type = Enums.StockType.Common }, _stockTrades);
            _tradeParams = new TradeParams
            {
                Quantity = 100,
                Symbol = Symbol,
                TradePrice = 100,
                Type = Enums.TradeType.Sell
            };
            _stockTrades.MakeTrade(_tradeParams);
        }

        [Test]
        public void InitializeStockCorrectly()
        {
            //Assert
            _commonStock.Symbol.Should().Be(Symbol);
            _commonStock.FixedDividend.HasValue.Should().BeTrue();
            _commonStock.FixedDividend.Value.Should().Be(0M);
            _commonStock.ParValue.Should().Be(100M);
            _commonStock.Type.Should().Be(Enums.StockType.Common);
        }

        [Test]
        public void SetPrice()
        {
            //Assert
            _stockTrades.GetPrice("TEA").Should().Be(100);
        }

        [Test]
        public void DividendYieldReturnResult()
        {
            //Act
            _stockTrades.MakeTrade(_tradeParams);
            var dividendYield = _commonStock.GetCalculateDividendYield();
            //Assert
            dividendYield.Should().Be(0.1M);
        }

        [Test]
        public void CalculatePERatioReturnResult()
        {
            //Act
            _stockTrades.MakeTrade(_tradeParams);
            var PERatio = _commonStock.GetCalculatePERatio();
            //Assert
            PERatio.Should().Be(10);
        }

        [Test]
        public void CalculateGeometricMeanReturnsResult()
        {
            //Act
            _stockTrades.MakeTrade(_tradeParams);
            _stockTrades.MakeTrade(_tradeParams);
            _stockTrades.MakeTrade(_tradeParams);
            _stockTrades.MakeTrade(_tradeParams);
            var geometricMean = _stockTrades.GetGBCEAllShareIndex();
            geometricMean.Should().Be(100);
        }
    }
}
