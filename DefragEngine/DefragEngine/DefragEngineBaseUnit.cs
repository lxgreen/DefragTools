using System;
using System.Linq;
using System.Xml.Linq;

namespace DefragEngine
{
    public class DefragEngineBaseUnit
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid ID { get; internal set; }

        public string Version { get; set; }

        public DefragEngineBaseUnit(string name, string version, Guid id)
        {
            Name = name;
            Version = version;
            ID = id;
        }

        public DefragEngineBaseUnit(string name, string version) : this(name, version, Guid.NewGuid())
        {
        }

        internal DefragEngineBaseUnit()
        {
        }

        public virtual bool Parse(XElement xmlElement)
        {
            var parseOK = false;

            var attributes = xmlElement.Attributes();

            var id = (from attr in attributes where attr.Name == "ID" select attr.Value).FirstOrDefault();
            var name = (from attr in attributes where attr.Name == "Name" select attr.Value).FirstOrDefault();
            var version = (from attr in attributes where attr.Name == "Version" select attr.Value).FirstOrDefault();

            Guid guid;
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(version) || !Guid.TryParse(id, out guid)) { return parseOK; }

            ID = guid;
            Name = name;
            Version = version;
            Description = (from element in xmlElement.Descendants() where element.Name == "Description" select element.Value).FirstOrDefault();
            parseOK = true;

            return parseOK;
        }

        public virtual string ToXML()
        {
        }
    }
}