using System.Collections.Generic;
using System.Linq;

namespace DefragEngine
{
    public class ToolCollection : List<Tool>
    {
        public void Add(params Tool[] tools)
        {
            AddRange(tools);
        }

        public IEnumerable<Tool> this[string name] => from tool in this where tool.Name == name select tool;
    }
}