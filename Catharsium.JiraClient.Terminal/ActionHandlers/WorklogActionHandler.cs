using Atlassian.Jira;
using Catharsium.Util.IO.Console.Interfaces;
using System;
using System.Threading.Tasks;

namespace Catharsium.JiraClient.Terminal.ActionHandlers
{
    public class WorklogActionHandler : IActionHandler
    {
        private readonly Jira jira;
        private readonly IConsole console;

        public string FriendlyName => "Worklog";


        public WorklogActionHandler(Jira jira, IConsole console)
        {
            this.jira = jira;
            this.console = console;
        }


        public async Task Run()
        {
            var issueKey = this.console.AskForText("Enter the issue (XXX-####):");
            var issue = await this.jira.Issues.GetIssueAsync(issueKey);
            var worklogs = await issue.GetWorklogsAsync();
            foreach (var worklog in worklogs)
            {
                this.console.WriteLine($"{worklog.Author} - {worklog.TimeSpent}");
            }

            var date = this.console.AskForDate("Enter the date (yyyy-MM-dd):", DateTime.Now);
            var timeSpent = this.console.AskForText("Enter time spent (Jira format):");
            await issue.AddWorklogAsync(new Worklog(timeSpent, date));
        }
    }
}