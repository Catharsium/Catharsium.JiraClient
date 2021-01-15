using Catharsium.JiraClient.Terminal._Configuration;
using Catharsium.JiraClient.Terminal.ActionHandlers;
using Catharsium.Util.IO.Console.Interfaces;
using Catharsium.Util.Testing.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Catharsium.JiraClient.Terminal.Tests._Configuration
{
    [TestClass]
    public class JiraClientTerminalRegistrationTests
    {
        [TestMethod]
        public void AddJiraClientTerminal_RegistersDependencies()
        {
            var serviceCollection = Substitute.For<IServiceCollection>();
            var configuration = Substitute.For<IConfiguration>();

            serviceCollection.AddJiraClientTerminal(configuration);
            serviceCollection.ReceivedRegistration<IActionHandler, ListActionHandler>();
            serviceCollection.ReceivedRegistration<IActionHandler, WorklogActionHandler>();
            serviceCollection.ReceivedRegistration<IActionHandler, SubTasksActionHandler>();
            serviceCollection.ReceivedRegistration<IActionHandler, DefaultSubTasksActionHandler>();
        }


        [TestMethod]
        public void AddJiraClientTerminal_RegistersPackages()
        {
            var serviceCollection = Substitute.For<IServiceCollection>();
            var configuration = Substitute.For<IConfiguration>();

            serviceCollection.AddJiraClientTerminal(configuration);
            serviceCollection.ReceivedRegistration<IConsole>();
        }
    }
}