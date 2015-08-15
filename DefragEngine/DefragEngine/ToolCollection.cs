using System;
using System.Collections.Generic;

namespace DefragEngine
{
    public class ToolCollection : Dictionary<Guid, Tool>
    {
        public void Add(Tool tool)
        {
            Add(tool.ID, tool);
        }
    }
}