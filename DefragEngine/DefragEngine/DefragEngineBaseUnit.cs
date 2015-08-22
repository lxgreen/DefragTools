using System.Linq;
using System.Xml.Linq;

namespace DefragEngine
{
    public class DefragEngineBaseUnit
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Hash ID { get; internal set; }

        public string Version { get; set; }

        public DefragEngineBaseUnit(string name, string version, string id)
        {
            Name = name;
            Version = version;
            ID = id;
        }

        public DefragEngineBaseUnit(string name, string version)
        {
            Name = name;
            Version = version;
            ID = GenerateID();    // by default id = f(name, version)
        }

        internal DefragEngineBaseUnit()
        {
        }

        protected virtual Hash GenerateID() => new Hash(string.Format("{0}{1}", Name, Version));

        public virtual bool Parse(XElement xmlElement)
        {
            var parseOK = false;

            var attributes = xmlElement.Attributes();

            var id = (from attr in attributes where attr.Name == "ID" select attr.Value).FirstOrDefault();
            var name = (from attr in attributes where attr.Name == "Name" select attr.Value).FirstOrDefault();
            var version = (from attr in attributes where attr.Name == "Version" select attr.Value).FirstOrDefault();

            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(version)) { return parseOK; }

            ID = id;
            Name = name;
            Version = version;
            Description = (from element in xmlElement.Descendants() where element.Name == "Description" select element.Value).FirstOrDefault();
            parseOK = true;

            return parseOK;
        }

        public virtual XElement ToXML(string name) => new XElement(name,
                                                            new XAttribute("ID", ID),
                                                            new XAttribute("Name", Name),
                                                            new XAttribute("Version", Version),
                                                            new XElement("Description", Description));
    }
}