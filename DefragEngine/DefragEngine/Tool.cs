using System;

namespace DefragEngine
{
    public class Tool : DefragEngineBaseUnit
    {

        public bool CanUpdate { get; set; }

        public string UpdateURL { get; set; }

        public string FilePath { get; set; }

        public string CommandLine { get; set; }

        public bool IsPortable { get; set; }

        public ToolCategory Category { get; set; }
    }
}