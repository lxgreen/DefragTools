using System;
using System.Linq;
using System.Collections.Generic;

namespace DefragEngine
{
    public class CategoryCollection : Dictionary<Guid, ToolCategory>
    {
        public void Add(ToolCategory category)
        {
            Add(category.ID, category);
        }
        public bool Remove(ToolCategory tool)
        {
            return Remove(tool.ID);
        }

        public void Add(params ToolCategory[] tools)
        {
            foreach (var tool in tools)
            {
                Add(tool);
            }
        }

        public IEnumerable<ToolCategory> this[string name]
        {
            get
            {
                return from category in this.Values
                       where category.Name == name
                       select category;
            }
        }
    }
}