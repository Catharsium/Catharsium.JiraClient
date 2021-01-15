using Catharsium.JiraClient.Terminal._Configuration;
using Catharsium.Util.IO.Console.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Threading.Tasks;

namespace Catharsium.JiraClient.Terminal
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, false);
            var configuration = builder.Build();

            var serviceCollection = new ServiceCollection()
                .AddJiraTerminal(configuration);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var chooseOperationActionHandler = serviceProvider.GetService<IChooseActionHandler>();
            await chooseOperationActionHandler.Run();
        }
    }
}