using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace No7.Solution
{
    public sealed class TradeRepository : IRepository<Trade>
    {
        public TradeRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; }

        public IEnumerable<Trade> GetAll()
        {
            throw new NotImplementedException();
        }

        public void InsertMany(IEnumerable<Trade> trades)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    foreach (var trade in trades)
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

                    transaction.Commit();
                }

                connection.Close();
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
