using No7.Solution.Concrete;
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
            
            var service = new DataTransferService(new DataProvider(tradeStream), 
                new DbStorage(ConfigurationManager.ConnectionStrings[CONNECTION_DATA].ConnectionString),
                new Parser());

            service.Transfer();

            System.Console.ReadKey();
        }
    }
}