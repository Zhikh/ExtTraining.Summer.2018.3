using System;
using System.Collections.Generic;
using No7.Solution.Interface;

namespace No7.Solution.Concrete
{
    public sealed class DataTransferService : IDataTransferService
    {
        private const string LOG_FILE = "log.txt";
        private static readonly ILogger _logger = new Logger(LOG_FILE);

        private IDataProvider<string> _provider;
        private IStorage<Trade> _storage;
        private IParser<Trade> _parser;

        public DataTransferService(IDataProvider<string> provider, IStorage<Trade> storage, IParser<Trade> parser)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _parser = parser ?? throw new ArgumentNullException(nameof(parser));
        }

        public void Transfer()
        {
            IEnumerable<string> data = _provider.GetAll();
            List<Trade> trades = new List<Trade>();

            int lineCount = 1;
            foreach (var element in data)
            {
                try
                {
                    trades.Add(_parser.Parse(element));

                }
                catch(ArgumentException ex)
                {
                    _logger.LogWarn(ex.Message + $"(on line {lineCount})");
                }

                lineCount++;
            }


            _logger.LogInfo($"(The {trades.Count}) trades processed.");

            _storage.Save(trades);
        }
    }
}
