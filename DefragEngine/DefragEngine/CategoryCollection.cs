using System;
using System.Collections.Generic;

namespace DefragEngine
{
    public class CategoryCollection : Dictionary<Guid, ToolCategory>
    {
        public void Add(ToolCategory category)
        {
            Add(category.ID, category);
        }
    }
}