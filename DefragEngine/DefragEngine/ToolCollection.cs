using System;
using System.Collections.Generic;
using System.Linq;

namespace DefragEngine
{
    public class ToolCollection : Dictionary<Guid, Tool>
    {
        public void Add(Tool tool)
        {
            Add(tool.ID, tool);
        }

        public bool Remove(Tool tool)
        {
            return Remove(tool.ID);
        }

        public void Add(params Tool[] tools)
        {
            foreach (var tool in tools)
            {
                Add(tool);
            }
        }

        public IEnumerable<Tool> this[string name]
        {
            get
            {
                return from tool in this.Values
                       where tool.Name == name
                       select tool;
            }
        }
    }
}