namespace DefragEngine
{
    public class ToolCategory : DefragEngineBaseUnit
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ToolCollection Tools { get; set; }
    }
}