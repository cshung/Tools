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

        public string Poll()
        {
            var timeline = tokens.Statuses.HomeTimeline();
            if (!this.initialized)
            {
                this.baseLine = timeline.Count;
                this.initialized = true;
                return null;
            }
            else
            {
                if (timeline.Count != this.baseLine)
                {
                    this.baseLine = timeline.Count;
                    return timeline[0].Text;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
