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

        public string ToXML()
        {
            var xml = new XDocument(
                new XElement("Bundle",
                    new XAttribute("ID", ID),
                    new XAttribute("Name", Name),
                    new XAttribute("Version", Version),
                        new XElement("Description", Description),
                        new XElement("Categories", from category in Categories select
                            new XElement("Category",
                            new XAttribute("ID", category.ID),
                            new XAttribute("Name", category.Name),
                            new XAttribute("Version", category.Version),
                             new XElement("Description", category.Description),
                             new XElement("Tools", from tool in category.Tools select
                                 new XElement("Tool",
                                     new XAttribute("ID", tool.ID),
                                     new XAttribute("Name", tool.Name),
                                     new XAttribute("Version", tool.Version),
                                     new XAttribute("Portable", tool.IsPortable),
                                         new XElement("Description", tool.Description),
                                         new XElement("UpdateURL", tool.CanUpdate ? tool.UpdateURL : "N/A"),
                                         new XElement("CommandLine", tool.CommandLine),
                                         new XElement("Properties", from property in tool.Properties select
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

            if(bundle == null) { return result; }

            var attributes = bundle.Attributes();

            var id = (from attr in attributes where attr.Name == "ID" select attr.Value).FirstOrDefault();
            var name = (from attr in attributes where attr.Name == "Name" select attr.Value).FirstOrDefault();
            var version = (from attr in attributes where attr.Name == "Version" select attr.Value).FirstOrDefault();

            if(string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(version)) { return result; }

            var description = (from element in bundle.Descendants() where element.Name == "Description" select element.Value).FirstOrDefault();

            result = new ToolBundle
            {
                ID = Guid.Parse(id),
                Name = name,
                Version = version,
                Description = description
            };

            var categories = from element in bundle.Descendants() where element.Name == "Category" select element;

            foreach (var categoryElement in categories)
            {
                attributes = categoryElement.Attributes();

                id = (from attr in attributes where attr.Name == "ID" select attr.Value).FirstOrDefault();
                name = (from attr in attributes where attr.Name == "Name" select attr.Value).FirstOrDefault();
                version = (from attr in attributes where attr.Name == "Version" select attr.Value).FirstOrDefault();

                if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(version)) { continue; }

                description = (from element in categoryElement.Descendants() where element.Name == "Description" select element.Value).FirstOrDefault();

                var category = new ToolCategory
                {
                    ID = Guid.Parse(id),
                    Name = name,
                    Version = version,
                    Description = description
                };

                var tools = from element in categoryElement.Descendants() where element.Name == "Tool" select element;


                foreach (var toolElement in tools)
                {
                    attributes = toolElement.Attributes();

                    id = (from attr in attributes where attr.Name == "ID" select attr.Value).FirstOrDefault();
                    name = (from attr in attributes where attr.Name == "Name" select attr.Value).FirstOrDefault();
                    version = (from attr in attributes where attr.Name == "Version" select attr.Value).FirstOrDefault();

                    if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(version)) { continue; }

                    description = (from element in toolElement.Descendants() where element.Name == "Description" select element.Value).FirstOrDefault();
                    var isPortable = (from attr in attributes where attr.Name == "Portable" select attr.Value).FirstOrDefault();
                    var commandLine = (from element in toolElement.Descendants() where element.Name == "CommandLine" select element.Value).FirstOrDefault();
                    var updateURL = (from element in toolElement.Descendants() where element.Name == "UpdateURL" select element.Value).FirstOrDefault();

                    var tool = new Tool
                    {
                        ID = Guid.Parse(id),
                        Name = name,
                        Version = version,
                        Description = description,
                        CommandLine = commandLine,
                        UpdateURL = updateURL,
                        CanUpdate = !string.IsNullOrEmpty(updateURL) && updateURL != "N/A",
                        IsPortable = bool.Parse(isPortable)
                    };

                    var properties = from element in toolElement.Descendants() where element.Name == "Property" select element;

                    foreach (var prop in properties)
                    {
                        attributes = prop.Attributes();
                        var propName = (from attr in attributes where attr.Name == "Name" select attr.Value).FirstOrDefault();
                        var propValue = (from attr in attributes where attr.Name == "Value" select attr.Value).FirstOrDefault();

                        if (string.IsNullOrEmpty(propName) || string.IsNullOrEmpty(propValue)) { continue; }

                        tool.Properties.Add(propName, propValue);
                    }

                    category.Tools.Add(tool);
                }

                result.Categories.Add(category);
            }

            return result;
        }
        
    }
}