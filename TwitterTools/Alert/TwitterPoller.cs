namespace Alert
{
    using CoreTweet;

    public class TwitterPoller : IPoller
    {
        private bool initialized;
        private int baseLine;
        private Tokens tokens;

        public TwitterPoller(string apiKey, string apiKeySecret, string accessToken, string accessTokenSecret)
        {
            this.initialized = false;
            this.tokens = Tokens.Create(apiKey, apiKeySecret, accessToken, accessTokenSecret);
        }

        public bool Poll()
        {
            if (!this.initialized)
            {
                this.baseLine = tokens.Statuses.HomeTimeline().Count;
                this.initialized = true;
            }
            if (tokens.Statuses.HomeTimeline().Count != this.baseLine)
            {
                this.initialized = false;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
