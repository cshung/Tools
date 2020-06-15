namespace Alert
{
    using System;
    using System.Configuration;
    using System.Windows.Forms;

    internal static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            string apiKey = ConfigurationManager.AppSettings["apiKey"];
            string apiKeySecret = ConfigurationManager.AppSettings["apiKeySecret"];
            string accessToken = ConfigurationManager.AppSettings["accessToken"];
            string accessTokenSecret = ConfigurationManager.AppSettings["accessTokenSecret"];
            TwitterPoller poller = new TwitterPoller(apiKey, apiKeySecret, accessToken, accessTokenSecret);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(poller));
        }
    }
}
