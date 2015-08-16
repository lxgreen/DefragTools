namespace DefragEngine
{
    public class Tool : DefragEngineBaseUnit
    {
        private PropertyCollection _properties;

        public bool CanUpdate { get; set; }

        public string UpdateURL { get; set; }

        public string FilePath { get; set; }

        public string CommandLine { get; set; }

        public bool IsPortable { get; set; }

        public PropertyCollection Properties
        {
            get
            {
                return _properties ?? (_properties = new PropertyCollection());
            }
        }

        public Tool(string name) : base(name)
        {
        }
    }
}