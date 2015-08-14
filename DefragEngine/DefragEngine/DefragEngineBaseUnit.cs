using System;

namespace DefragEngine
{
    public class DefragEngineBaseUnit
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid ID { get; set; }

        public string Version { get; set; }

        public DefragEngineBaseUnit(string name)
        {
            Name = name;
            ID = Guid.NewGuid();
        }
    }
}