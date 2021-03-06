﻿using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace DefragEngine
{
    public class Tool : DefragEngineBaseUnit
    {
        private PropertyCollection _properties;

        public bool CanUpdate { get; set; }

        public string UpdateURL { get; set; }

        public string CommandLine { get; set; }

        public bool IsPortable { get; set; }

        public PropertyCollection Properties => _properties ?? (_properties = new PropertyCollection());

        public Tool(string name, string version, string commandLine)
        {
            Name = name;
            Version = version;
            CommandLine = commandLine;
            ID = GenerateID();   // id = f(file contents)
        }

        internal Tool()
        {
        }

        internal Tool(string name, string version, string commandLine, string id) : base(name, version, id)
        {
            CommandLine = commandLine;
        }

        protected override Hash GenerateID()
        {
            Hash id = null;

            if (string.IsNullOrEmpty(CommandLine) || !File.Exists(Path.GetFullPath(CommandLine)))
            {
                id = base.GenerateID();
            }
            else
            {
                using (var stream = File.OpenRead(Path.GetFullPath(CommandLine)))
                {
                    id = new Hash(stream);
                }
            }

            return id;
        }

        public override bool Parse(XElement xmlElement)
        {
            var parseOK = false;
            if (base.Parse(xmlElement))
            {
                var attributes = xmlElement.Attributes();
                var portable = (from attr in attributes where attr.Name == "Portable" select attr.Value).FirstOrDefault();
                CommandLine = (from element in xmlElement.Descendants() where element.Name == "CommandLine" select element.Value).FirstOrDefault();
                UpdateURL = (from element in xmlElement.Descendants() where element.Name == "UpdateURL" select element.Value).FirstOrDefault();
                CanUpdate = !string.IsNullOrEmpty(UpdateURL) && !string.IsNullOrWhiteSpace(UpdateURL);

                bool isPortable;
                IsPortable = bool.TryParse(portable, out isPortable) ? isPortable : true; // portable by default?

                var properties = from element in xmlElement.Descendants() where element.Name == "Property" select element;

                foreach (var prop in properties)
                {
                    attributes = prop.Attributes();
                    var propName = (from attr in attributes where attr.Name == "Name" select attr.Value).FirstOrDefault();
                    var propValue = (from attr in attributes where attr.Name == "Value" select attr.Value).FirstOrDefault();

                    if (string.IsNullOrEmpty(propName) || string.IsNullOrEmpty(propValue)) { continue; }

                    Properties.Add(propName, propValue);
                }

                parseOK = true;
            }

            return parseOK;
        }

        public override XElement ToXML(string name)
        {
            var toolElement = base.ToXML(name);
            toolElement.Add(
                new XAttribute("Portable", IsPortable),
                new XElement("UpdateURL", CanUpdate ? UpdateURL : ""),
                new XElement("CommandLine", CommandLine),
                new XElement("Properties", from property in Properties
                                           select new XElement("Property",
                                           new XAttribute("Name", property.Key),
                                           new XAttribute("Value", property.Value))));
            return toolElement;
        }
    }
}