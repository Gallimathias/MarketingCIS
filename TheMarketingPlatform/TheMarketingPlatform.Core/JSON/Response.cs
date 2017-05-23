namespace TheMarketingPlatform.Core.JSON
{
    public class Response
    {
        public string Query { get; set; }
        public Intent TopScoringIntent { get; set; }
        public Intent[] Intents { get; set; }
        public Entity[] Entities { get; set; }
    }

}
