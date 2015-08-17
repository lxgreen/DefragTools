using System;

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
    }
}