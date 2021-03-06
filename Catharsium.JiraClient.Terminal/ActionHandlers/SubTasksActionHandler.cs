﻿using Atlassian.Jira;
using Catharsium.JiraClient.Terminal._Configuration;
using Catharsium.Util.IO.Console.Interfaces;
using System.Threading.Tasks;

namespace Catharsium.JiraClient.Terminal.ActionHandlers
{
    public class SubTasksActionHandler : IActionHandler
    {
        private readonly Jira jira;
        private readonly IConsole console;
        private readonly JiraTerminalSettings settings;

        public string FriendlyName => "SubTasks";


        public SubTasksActionHandler(Jira jira, IConsole console, JiraTerminalSettings settings)
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

            while (true)
            {
                var issueSummary = this.console.AskForText("Enter the summary:");
                if (string.IsNullOrWhiteSpace(issueSummary))
                {
                    return;
                }

                var issue = this.jira.CreateIssue(this.settings.DefaultProjectKey, $"{this.settings.DefaultProjectKey}-{issueKey}");
                issue.Type = "5";
                issue.Summary = issueSummary;
                await issue.SaveChangesAsync();
                this.console.WriteLine($"Created {issue.Key}");
            }
        }
    }
}