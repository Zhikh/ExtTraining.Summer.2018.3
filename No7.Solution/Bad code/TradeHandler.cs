using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace No7.Solution.Console
{
    public class TradeHandler
    {
        private static float LotSize = 100000f;     // const - нигде не меняется и дальше этого класса не прорастает, + название поле должно быть lower-case

        // перегружен обязанностями
        public void HandleTrades(Stream stream)
        {
            #region Обязанность для класса, который работает с файлами
            /* 
             * можно вынести в статический класс и передавать путь к файлу: 
             * + : не привязываемся к конкретному файлу
             * - : может поменяться тип хранилища, например на бд
             * либо создать репозиторий 
             * + : гибкость относительно типа хранилища
             * - : объект класса хранилища заточенный только для работы с ним (не очень удобно в контексте файлов)
            */
            var lines = new List<string>();

            using (var reader = new StreamReader(stream))   // отдельный метод для чтения
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            #endregion

            #region Необходимо выделить в отдельную сущность для создания Trade на основе строк 
            /*
             * Решение: создать класс маппера для trade - позволит добавлять новые методы для маппинг на(с) разные сущности
             */
            var trades = new List<TradeRecord>();

            var lineCount = 1;
            foreach (var line in lines)
            {
                var fields = line.Split(new char[] { ',' });

                #region Ответственность логировщика
                /*
                 * Решение: использовать NLog (гибкий и легконастраиваемый)
                 */
                if (fields.Length != 3)  // const!
                {
                    System.Console.WriteLine("WARN: Line {0} malformed. Only {1} field(s) found.", lineCount, fields.Length);
                    continue;
                }

                if(fields[0].Length != 6)   // const!
                {
                    System.Console.WriteLine("WARN: Trade currencies on line {0} malformed: '{1}'", lineCount, fields[0]);
                    continue;
                }

                if(!int.TryParse(fields[1], out var tradeAmount))
                {
                    System.Console.WriteLine("WARN: Trade amount on line {0} not a valid integer: '{1}'", lineCount, fields[1]);
                }

                if(!decimal.TryParse(fields[2], out var tradePrice))
                {
                    System.Console.WriteLine("WARN: Trade price on line {0} not a valid decimal: '{1}'", lineCount, fields[2]);
                }
                #endregion

                var sourceCurrencyCode = fields[0].Substring(0, 3);
                var destinationCurrencyCode = fields[0].Substring(3, 3);

                var trade = new TradeRecord
                {
                    SourceCurrency = sourceCurrencyCode,
                    DestinationCurrency = destinationCurrencyCode,
                    Lots = tradeAmount / LotSize,
                    Price = tradePrice
                };

                trades.Add(trade);

                lineCount++;
            }
            #endregion

            #region Обязанность класса, который взаимодействет с бд
            /*
             * Решение создать класс репозитория - исключит зависимость от базы данных
             */
            string connectionString = ConfigurationManager.ConnectionStrings["TradeData"].ConnectionString; // передаётся во время создания репозитория
            /*
             * последующий код в scope мне совершенно не нравиться: мы в одном месте открываем соединение с бд, пишем в бд и сохраняем
             * но у меня нет идей как это можно аккуратно исправить
             */
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using(var transaction = connection.BeginTransaction())
                {
                    foreach(var trade in trades)
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
            #endregion

            System.Console.WriteLine("INFO: {0} trades processed", trades.Count); // logger
        }
    }
}
