using Catharsium.JiraClient.Terminal._Configuration;
using Catharsium.Util.IO.Console.Interfaces;
using System.Threading.Tasks;

namespace Catharsium.JiraClient.Terminal.ActionHandlers
{
    public class CredentialsActionHandler : IActionHandler
    {
        private readonly IConsole console;
        private readonly JiraTerminalSettings settings;

        public string FriendlyName => "Credentials";


        public CredentialsActionHandler(IConsole console, JiraTerminalSettings settings)
        {
            this.console = console;
            this.settings = settings;
        }


        public async Task Run()
        {
            this.settings.Credentials = new Credentials
            {
                Username = this.console.AskForText("Enter your username:"),
                Password = this.console.AskForText("Enter your password:")
            };
        }
    }
}