using No7.Solution.Mappers;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace No7.Solution.Console
{
    class Program
    {
        private const string CONNECTION_DATA= "TradeData";

        static void Main(string[] args)
        {
            Stream tradeStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("No7.Solution.Console.trades.txt");

            var repository = new TradeRepository(ConfigurationManager.ConnectionStrings[CONNECTION_DATA].ConnectionString);

            repository.InsertMany(TradeMappers.ToMany(StreamWorker.ReadAll(tradeStream)));

            System.Console.ReadKey();
        }
    }
}