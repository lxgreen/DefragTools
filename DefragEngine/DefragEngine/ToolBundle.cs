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

        public ToolBundle(string name) : base(name)
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
                        new XElement("Categories", from category in Categories.Values select
                            new XElement("Category",
                            new XAttribute("ID", category.ID),
                            new XAttribute("Name", category.Name),
                            new XAttribute("Version", category.Version),
                             new XElement("Description", category.Description),
                             new XElement("Tools", from tool in category.Tools.Values select
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
    }
}