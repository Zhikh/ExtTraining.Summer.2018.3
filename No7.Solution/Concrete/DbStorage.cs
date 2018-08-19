using System.Collections.Generic;
using System.Data.SqlClient;
using No7.Solution.Interface;

namespace No7.Solution.Concrete
{
    public sealed class DbStorage : IStorage<Trade>
    {
        #region Public API
        /// <summary>
        /// Initializes a new instance of the <see cref="DbStorage"/>.
        /// </summary>
        /// <param name="connectionString"> Database connection string. </param>
        /// <exception cref="ArgumentException">
        ///     <paramref name="connectionString"> is null or empty. </paramref>
        /// </exception>
        public DbStorage(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new System.ArgumentException($"The parameter {nameof(connectionString)} can't be null or empty!");
            }

            ConnectionString = connectionString;
        }

        /// <summary>
        /// Database connection string
        /// </summary>
        public string ConnectionString { get; }

        /// <summary>
        /// Saves data to database
        /// </summary>
        /// <param name="entities"> Collection of the <see cref="Trade"/> elements. </param>
        public void Save(IEnumerable<Trade> entities)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                LoadInOneTransaction(entities, connection);

                connection.Close();
            }
        }
        #endregion

        #region Additional methods
        private static void LoadInOneTransaction(IEnumerable<Trade> trades, SqlConnection connection)
        {
            using (var transaction = connection.BeginTransaction())
            {
                foreach (var trade in trades)
                {
                    CreateCommand(connection, transaction, trade);
                }

                transaction.Commit();
            }
        }

        private static void CreateCommand(SqlConnection connection, SqlTransaction transaction, Trade trade)
        {
            var command = connection.CreateCommand();

            command.Transaction = transaction;
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.CommandText = "dbo.Insert_Trade";

            command.Parameters.AddWithValue("@sourceCurrency", trade.SourceCurrency);
            command.Parameters.AddWithValue("@destinationCurrency", trade.DestinationCurrency);
            command.Parameters.AddWithValue("@lots", trade.Lots);
            command.Parameters.AddWithValue("@price", trade.Price);

            command.ExecuteNonQuery();
        }
        #endregion
    }
}
