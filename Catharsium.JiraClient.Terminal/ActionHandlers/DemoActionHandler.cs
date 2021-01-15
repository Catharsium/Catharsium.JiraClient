using Atlassian.Jira;
using Catharsium.JiraClient.Terminal._Configuration;
using Catharsium.Util.IO.Console.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Catharsium.JiraClient.Terminal.ActionHandlers
{
    public class DemoActionHandler : IActionHandler
    {
        private readonly IConsole console;
        private readonly JiraTerminalSettings settings;

        public string FriendlyName => "Demo";


        public DemoActionHandler(IConsole console, JiraTerminalSettings settings)
        {
            this.console = console;
            this.settings = settings;
        }


        public async Task Run()
        {
            await new CredentialsActionHandler(this.console, this.settings).Run();
            var jira = Jira.CreateRestClient(this.settings.JiraServerUrl, this.settings.Credentials.Username, this.settings.Credentials.Password);
            var issues = from i in jira.Issues.Queryable
                         where i.Assignee == this.settings.Credentials.Username
                         orderby i.Created descending
                         select i;

            this.console.WriteLine();
            this.console.WriteLine($"{issues.Count()} assigned to me");
            foreach (var issue in issues)
            {
                this.console.WriteLine($"{issue.Key} - {issue.Summary}");
            }
        }
    }
}