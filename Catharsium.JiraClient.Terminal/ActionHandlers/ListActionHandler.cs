using Atlassian.Jira;
using Catharsium.JiraClient.Terminal._Configuration;
using Catharsium.Util.IO.Console.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Catharsium.JiraClient.Terminal.ActionHandlers
{
    public class ListActionHandler : IActionHandler
    {
        private readonly Jira jira;
        private readonly IConsole console;
        private readonly JiraTerminalSettings settings;

        public string FriendlyName => "List";


        public ListActionHandler(Jira jira, IConsole console, JiraTerminalSettings settings)
        {
            this.jira = jira;
            this.console = console;
            this.settings = settings;
        }


        public async Task Run()
        {
            var issues = from i in this.jira.Issues.Queryable
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