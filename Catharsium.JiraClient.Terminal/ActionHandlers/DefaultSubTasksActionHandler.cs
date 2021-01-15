using Atlassian.Jira;
using Catharsium.JiraClient.Terminal._Configuration;
using Catharsium.Util.IO.Console.Interfaces;
using System.Threading.Tasks;

namespace Catharsium.JiraClient.Terminal.ActionHandlers
{
    public class DefaultSubTasksActionHandler : IActionHandler
    {
        private readonly Jira jira;
        private readonly IConsole console;
        private readonly JiraTerminalSettings settings;

        public string FriendlyName => "Default subtasks";


        public DefaultSubTasksActionHandler(Jira jira, IConsole console, JiraTerminalSettings settings)
        {
            this.jira = jira;
            this.console = console;
            this.settings = settings;
        }


        public async Task Run()
        {
            var issueKey = this.console.AskForInt("Enter the parent issue number (####):");
            if (!issueKey.HasValue)
            {
                return;
            }

            foreach (var subtask in this.settings.DefaultSubTasks)
            {
                var issue = this.jira.CreateIssue(this.settings.DefaultProjectKey, $"{this.settings.DefaultProjectKey}-{issueKey}");
                issue.Type = "5";
                issue.Summary = subtask.Summary;
                await issue.SaveChangesAsync();
                this.console.WriteLine($"Created {issue.Key} ({issue.Summary})");
            }
        }
    }
}