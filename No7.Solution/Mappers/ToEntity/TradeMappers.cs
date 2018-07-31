using System;
using System.Collections.Generic;

namespace No7.Solution.Mappers
{
    public static partial class TradeMappers
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private const int VALUE_LENGTH = 6;
        private const float LotSize = 100000f;

        public static Trade ToEntity(string currencyTypes, string amount, string price)
        {
            if (currencyTypes.Length != VALUE_LENGTH)   
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

            var sourceCurrencyCode = currencyTypes.Substring(0, 3);
            var destinationCurrencyCode = currencyTypes.Substring(3, 3);

            var trade = new Trade
            {
                SourceCurrency = sourceCurrencyCode,
                DestinationCurrency = destinationCurrencyCode,
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

            return trades;
        }
    }
}
