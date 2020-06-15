namespace Post
{
    using CoreTweet;
    using System;
    using System.Configuration;

    class Program
    {
        static void Main(string[] args)
        {
            string apiKey = ConfigurationManager.AppSettings["apiKey"];
            string apiKeySecret = ConfigurationManager.AppSettings["apiKeySecret"];
            string accessToken = ConfigurationManager.AppSettings["accessToken"];
            string accessTokenSecret = ConfigurationManager.AppSettings["accessTokenSecret"];
            var tokens = Tokens.Create(apiKey, apiKeySecret, accessToken, accessTokenSecret);
            string message = "hello";
            tokens.Statuses.Update(status => message + "\n" + Guid.NewGuid().ToString());
        }
    }
}
