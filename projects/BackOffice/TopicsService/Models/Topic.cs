namespace TopicsService
{
    public class Topic
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsAvailable { get; set; }
    }
}