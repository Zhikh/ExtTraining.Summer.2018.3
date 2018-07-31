using System;
using System.Collections.Generic;

namespace No7.Solution.Mappers
{
    public static partial class TradeMappers
    {
        #region Constants
        private const float LotSize = 100000f;
        #endregion

        #region Fields
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
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

        public static List<Trade> ToMany(IEnumerable<string> data)
        {
            var trades = new List<Trade>();

            var lineCount = 1;
            foreach (var line in data)
            {
                var fields = line.Split(new char[] { ',' });

                if (fields.Length != 3)
                {
                    logger.Warn($"Line {lineCount} malformed. Only {fields.Length} field(s) found.");

                    continue;
                }

                try
                {
                    trades.Add(ToEntity(fields[0], fields[1], fields[2]));
                }
                catch (ArgumentException ex)
                {
                    logger.Warn(ex.Message + $"(on line {lineCount})");
                }

                lineCount++;
            }

            logger.Info($"(The {trades.Count}) trades processed.");

            return trades;
        }
        #endregion
    }
}
