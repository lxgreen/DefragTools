using System;
using System.Linq;
using System.Xml.Linq;

namespace DefragEngine
{
    public class ToolCategory : DefragEngineBaseUnit
    {
        private ToolCollection _tools;

        public ToolCollection Tools => _tools ?? (_tools = new ToolCollection());

        public ToolCategory(string name, string version) : base(name, version)
        {
        }

        internal ToolCategory(string name, string version, Guid id) : base(name, version, id)
        {
        }

        internal ToolCategory()
        {
        }

        public override bool Parse(XElement xmlElement)
        {
            bool parseOK = false;

            if (base.Parse(xmlElement))
            {
                var tools = from element in xmlElement.Descendants() where element.Name == "Tool" select element;

                foreach (var toolElement in tools)
                {
                    var tool = new Tool();

                    if (tool.Parse(toolElement))
                    {
                        Tools.Add(tool);
                    }
                }

                parseOK = true;
            }
            return parseOK;
        }

        public override XElement ToXML(string name)
        {
            var categoryElement = base.ToXML(name);
            categoryElement.Add(new XElement("Tools", from tool in Tools select tool.ToXML("Tool")));
            return categoryElement;
        }
    }
}