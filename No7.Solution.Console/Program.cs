using No7.Solution.Mappers;
using System.Configuration;
using System.Reflection;

namespace No7.Solution.Console
{
    class Program
    {
        private const string CONNECTION_DATA= "TradeData";
        private const string FILE_PATH = @"C:\Users\Zhikh Anastasya\Documents\ExtTraining.Summer.2018.3\No7.Solution.Console\trades.txt";

        static void Main(string[] args)
        {
            //var tradeStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("No7.Solution.Console.trades.txt");

            //var tradeProcessor = new TradeHandler();

            //tradeProcessor.HandleTrades(tradeStream);

            var repository = new TradeRepository(ConfigurationManager.ConnectionStrings[CONNECTION_DATA].ConnectionString);

            repository.InsertMany(TradeMappers.ToMany(StreamWorker.Read(FILE_PATH)));

            System.Console.ReadKey();
        }
    }
}