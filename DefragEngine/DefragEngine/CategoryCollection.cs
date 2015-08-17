using System;
using System.Collections.Generic;
using System.Linq;

namespace DefragEngine
{
    public class CategoryCollection : List<ToolCategory>
    {
        

        public void Add(params ToolCategory[] tools)
        {
            AddRange(tools);
        }

        public IEnumerable<ToolCategory> this[string name]
        {
            get
            {
                return from category in this
                       where category.Name == name
                       select category;
            }
        }
    }
}