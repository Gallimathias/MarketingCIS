namespace TheMarketingPlatform.Core.JSON
{
    internal class Response
    {
        public string Query { get; set; }
        public Intent TopScoringIntent { get; set; }
        public Intent[] Intents { get; set; }
        public Entity[] Entities { get; set; }
    }

}
