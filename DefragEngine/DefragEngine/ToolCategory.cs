namespace DefragEngine
{
    public class ToolCategory : DefragEngineBaseUnit
    {
        private ToolCollection _tools;

        public ToolCollection Tools
        {
            get
            {
                return _tools ?? (_tools = new ToolCollection());
            }
        }

        public ToolCategory(string name) : base(name)
        {
        }
    }
}