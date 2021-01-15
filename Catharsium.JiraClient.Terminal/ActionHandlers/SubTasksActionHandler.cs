using Atlassian.Jira;
using Catharsium.Util.IO.Console.Interfaces;
using System.Threading.Tasks;

namespace Catharsium.JiraClient.Terminal.ActionHandlers
{
    public class SubTasksActionHandler : IActionHandler
    {
        private readonly Jira jira;
        private readonly IConsole console;

        public string FriendlyName => "SubTasks";


        public SubTasksActionHandler(Jira jira, IConsole console)
        {
            this.jira = jira;
            this.console = console;
        }


        public async Task Run()
        {
            var projectKey = this.console.AskForText("Enter the project key (XXX):");
            var issueKey = this.console.AskForText("Enter the parent issue number (####):");

            while (true)
            {
                var issueSummary = this.console.AskForText("Enter the summary:");
                if (string.IsNullOrWhiteSpace(issueSummary))
                {
                    return;
                }

                var issue = this.jira.CreateIssue(projectKey, $"{projectKey}-{issueKey}");
                issue.Type = "5";
                issue.Summary = issueSummary;

                await issue.SaveChangesAsync();
                this.console.WriteLine($"Created {issue.Key}");
            }
        }
    }
}