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
        public static IServiceCollection AddJiraTerminal(this IServiceCollection services, IConfiguration configuration)
        {
            var trelloCoreConfiguration = configuration.Load<JiraTerminalSettings>();
            services.AddSingleton<JiraTerminalSettings, JiraTerminalSettings>(_ => trelloCoreConfiguration);

            services.AddConsoleIoUtilities(configuration);
            services.AddScoped<IActionHandler, DemoActionHandler>();

            return services;
        }
    }
}