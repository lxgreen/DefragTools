using System;
using System.Linq;
using System.Xml.Linq;

namespace DefragEngine
{
    public class ToolBundle : DefragEngineBaseUnit
    {
        private CategoryCollection _categories;

        public CategoryCollection Categories
        {
            get
            {
                return _categories ?? (_categories = new CategoryCollection());
            }
        }

        public ToolBundle(string name, string version) : base(name, version)
        {
        }

        internal ToolBundle(string name, string version, Guid id) : base(name, version, id)
        {
        }

        internal ToolBundle()
        {
        }

        public override string ToXML()
        {
            var xml = new XDocument(
                new XElement("Bundle",
                    new XAttribute("ID", ID),
                    new XAttribute("Name", Name),
                    new XAttribute("Version", Version),
                        new XElement("Description", Description),
                        new XElement("Categories", from category in Categories
                                                   select
new XElement("Category",
new XAttribute("ID", category.ID),
new XAttribute("Name", category.Name),
new XAttribute("Version", category.Version),
 new XElement("Description", category.Description),
 new XElement("Tools", from tool in category.Tools
                       select
     new XElement("Tool",
         new XAttribute("ID", tool.ID),
         new XAttribute("Name", tool.Name),
         new XAttribute("Version", tool.Version),
         new XAttribute("Portable", tool.IsPortable),
             new XElement("Description", tool.Description),
             new XElement("UpdateURL", tool.CanUpdate ? tool.UpdateURL : "N/A"),
             new XElement("CommandLine", tool.CommandLine),
             new XElement("Properties", from property in tool.Properties
                                        select
              new XElement("Property",
                  new XAttribute("Name", property.Key),
                  new XAttribute("Value", property.Value)))))))));

            return xml.ToString();
        }

        // TODO: refactoring
        public static ToolBundle Parse(string xml)
        {
            ToolBundle result = null;

            var xDoc = XDocument.Parse(xml);

            if (xDoc == null) { return result; }

            var bundle = xDoc.Root;

            if (bundle == null) { return result; }

            var attributes = bundle.Attributes();

            var id = (from attr in attributes where attr.Name == "ID" select attr.Value).FirstOrDefault();
            var name = (from attr in attributes where attr.Name == "Name" select attr.Value).FirstOrDefault();
            var version = (from attr in attributes where attr.Name == "Version" select attr.Value).FirstOrDefault();

            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(version)) { return result; }

            var description = (from element in bundle.Descendants() where element.Name == "Description" select element.Value).FirstOrDefault();

            result = new ToolBundle
            {
                ID = Guid.Parse(id),
                Name = name,
                Version = version,
                Description = description
            };

            return result;
        }

        public override bool Parse(XElement xmlElement)
        {
            bool parseOK = false;
            if (base.Parse(xmlElement))
            {
                var categories = from element in xmlElement.Descendants() where element.Name == "Category" select element;

                foreach (var categoryElement in categories)
                {
                    var category = new ToolCategory();

                    if (category.Parse(categoryElement))
                    {
                        Categories.Add(category);
                    }
                }

                parseOK = true; 
            }

            return parseOK;
        }
    }
}