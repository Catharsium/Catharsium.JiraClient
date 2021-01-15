using Atlassian.Jira;
using Catharsium.JiraClient.Terminal.ActionHandlers;
using Catharsium.Util.Configuration.Extensions;
using Catharsium.Util.IO.Console._Configuration;
using Catharsium.Util.IO.Console.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catharsium.JiraClient.Terminal._Configuration
{
    public static class JiraTerminalRegistration
    {
        public static IServiceCollection AddJiraClientTerminal(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.Load<JiraTerminalSettings>();
            services.AddSingleton<JiraTerminalSettings, JiraTerminalSettings>(_ => settings);

            services.AddConsoleIoUtilities(configuration);
            services.AddScoped<IActionHandler, ListActionHandler>();
            services.AddScoped<IActionHandler, WorklogActionHandler>();

            services.AddScoped(s => Jira.CreateRestClient(settings.JiraServerUrl, settings.Credentials.Username, settings.Credentials.Password));

            return services;
        }
    }
}