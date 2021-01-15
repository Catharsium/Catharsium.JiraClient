namespace Catharsium.JiraClient.Terminal._Configuration
{
    public class JiraTerminalSettings
    {
        public string JiraServerUrl { get; set; }
        public string DefaultProjectKey { get; set; }
        public SubTask[] DefaultSubTasks { get; set; }
        public Credentials Credentials { get; set; }
    }
}