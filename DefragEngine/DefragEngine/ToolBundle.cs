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

        public override string ToString()
        {
            var xml = new XDocument(ToXML("Bundle"));
            return xml.ToString();
        }

        public static ToolBundle Parse(string xml)
        {
            ToolBundle result = null;

            var xDoc = XDocument.Parse(xml);

            if (xDoc == null) { return result; }

            var bundleElement = xDoc.Root;

            if (bundleElement == null) { return result; }

            result = new ToolBundle();

            result.Parse(bundleElement);

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

        public override XElement ToXML(string name)
        {
            var bundleElement = base.ToXML(name);
            bundleElement.Add(new XElement("Categories", from category in Categories select category.ToXML("Category")));
            return bundleElement;
        }
    }
}