using Catharsium.JiraClient.Terminal._Configuration;
using Catharsium.JiraClient.Terminal.ActionHandlers;
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
                .AddJiraClientTerminal(configuration);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var settings = serviceProvider.GetService<JiraTerminalSettings>();
            if (settings.Credentials == null)
            {
                await new CredentialsActionHandler(serviceProvider.GetService<IConsole>(), settings).Run();
            }

            var chooseOperationActionHandler = serviceProvider.GetService<IChooseActionHandler>();
            await chooseOperationActionHandler.Run();
        }
    }
}