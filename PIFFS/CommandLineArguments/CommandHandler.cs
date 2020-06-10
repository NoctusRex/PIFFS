using System;
using System.Collections.Generic;
using System.Linq;

namespace PIFFS
{
    internal static class CommandHandler
    {
        static List<Command> Commands { get; set; } = new List<Command>();

        public static void AddCommand(Command command)
        {
            if (Commands.Any(x => x.GetType() == command.GetType())) { Console.WriteLine($"'{command.GetType().Name}' was already added."); return; }

            command.Identifiers.ForEach(i =>
            {
                if (Commands.Any(c => c.Identifiers.Contains(i))) throw new InvalidOperationException($"The identifier '{i}' of '{command.GetType().Name}' is already registered.");
            });

            Commands.Add(command);
        }

        public static void TryExecuteCommand(string[] arguments)
        {
            string mainCommand = arguments[0].ToLower();
            List<string> foundIdentifiers = new List<string>();

            foreach (Command command in Commands)
            {
                foreach (string identifier in command.Identifiers)
                {
                    if (!identifier.StartsWith(mainCommand)) continue;
                    foundIdentifiers.Add(identifier);
                }
            }

            if (foundIdentifiers.Count == 1)
            {
                Commands.First(x => x.Identifiers.Contains(foundIdentifiers.First())).TryExecute(arguments.Skip(1).ToArray());
            }
            else
            {
                string message = Environment.NewLine + $"Argument {mainCommand} is not unique. Did you mean:" + Environment.NewLine;
                foundIdentifiers.ForEach(x => { message += $" - {x}" + Environment.NewLine; });
                throw new Exception(message);
            }

        }
    }
}
