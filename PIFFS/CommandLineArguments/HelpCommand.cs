using System;
using System.Collections.Generic;

namespace PIFFS
{
    internal class HelpCommand : Command
    {
        public override List<string> Identifiers => new List<string> { "help", "?", "tip", "hint" };

        protected override void Execute(string[] arguments)
        {
            Console.WriteLine("This is not helpful at all!");
            throw new Exception("HAH!");
        }

        internal override void ValidateArguments(string[] arguments) { }
    }
}
