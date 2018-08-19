using System;
using System.Collections.Generic;
using No7.Solution.Interface;

namespace No7.Solution.Concrete
{
    public sealed class DataTransferService : IDataTransferService
    {
        #region Constants
        private const string LOG_FILE = "log.txt";
        #endregion

        #region Fields
        private static readonly ILogger _logger = new Logger(LOG_FILE);
        private IDataProvider<string> _provider;
        private IStorage<Trade> _storage;
        private IParser<Trade> _parser;
        #endregion

        #region Public API
        /// <summary>
        /// Initializes a new instance of the <see cref="DataTransferService"/>
        /// </summary>
        /// <param name="provider"> Data provider from a file </param>
        /// <param name="storage"> Database storage </param>
        /// <param name="parser"> Data parser </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="provider" /> is null.
        ///     <paramref name="storage" /> is null.
        ///     <paramref name="parser" /> is null.
        /// </exception>
        public DataTransferService(IDataProvider<string> provider, IStorage<Trade> storage, IParser<Trade> parser)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _parser = parser ?? throw new ArgumentNullException(nameof(parser));
        }

        /// <summary>
        /// Transfers data from one storage to another
        /// </summary>
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
                catch (ArgumentException ex)
                {
                    _logger.LogWarn(ex.Message + $"(on line {lineCount})");
                }

                lineCount++;
            }


            _logger.LogInfo($"(The {trades.Count}) trades processed.");

            _storage.Save(trades);
        }
        #endregion
    }
}
