using System.Collections.Generic;

namespace DefragEngine
{
    public class CommandLine
    {
        public string Command { get; set; }

        public List<string> Arguments { get; set; }

        public CommandLine(string command, params string[] args)
        {
            Command = command;
            Arguments = new List<string>(args);
        }

        public override string ToString() => string.Format("{0} {1}", Command, ListArguments());

        private string ListArguments() => "";
    }
}