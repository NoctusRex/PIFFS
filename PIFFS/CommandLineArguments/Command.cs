using System.Collections.Generic;

namespace PIFFS
{
    internal abstract class Command
    {
        public abstract List<string> Identifiers { get; }

        public void TryExecute(string[] arguments)
        {
            ValidateArguments(arguments);

            Execute(arguments);
        }

        internal abstract void ValidateArguments(string[] arguments);
        protected abstract void Execute(string[] arguments);
    }
}
