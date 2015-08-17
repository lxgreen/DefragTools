using System;

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

        public ToolCategory(string name, string version) : base(name, version)
        {
        }

        internal ToolCategory(string name, string version, Guid id) : base(name, version, id)
        {
        }

        internal ToolCategory()
        {
        }
    }
}