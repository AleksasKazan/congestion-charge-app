using Microsoft.Extensions.DependencyInjection;

namespace CongestionApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            var startup = new Startup();

            var serviceProvider = startup.ConfigureServices();

            var congestionApp = serviceProvider.GetService<CongestionApp>();

            congestionApp.Start();    
        }
    }
}
