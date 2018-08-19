using System;
namespace No7.Solution.Mappers
{
    public static partial class TradeMapper
    {
        #region Constants
        private const float LotSize = 100000f;
        #endregion

        #region public methods
        public static Trade ToEntity(string currencyTypes, string amount, string price)
        {
            if (currencyTypes.Length != Trade.DEFAULT_SIZE * 2)   
            {
                throw new ArgumentException($"Invalid length of {(nameof(currencyTypes))}!");
            }

            if (!int.TryParse(amount, out var tradeAmount))
            {
                throw new ArgumentException($"Trade {nameof(amount)} not a valid integer!");
            }

            if (!decimal.TryParse(price, out var tradePrice))
            {
                throw new ArgumentException($"Trade {nameof(price)} not a valid integer!");
            }
            
            var trade = new Trade
            {
                SourceCurrency = currencyTypes.Substring(0, Trade.DEFAULT_SIZE),
                DestinationCurrency = currencyTypes.Substring(Trade.DEFAULT_SIZE, Trade.DEFAULT_SIZE),
                Lots = tradeAmount / LotSize,
                Price = tradePrice
            };

            return trade;
        }
        #endregion
    }
}
