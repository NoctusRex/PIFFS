using System;
using System.Collections.Generic;
using System.Linq;

namespace PIFFS
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                LoadCommands();
                if (args.Length > 0) CommandHandler.TryExecuteCommand(args);

                while (true) GameStateHandler.HandleGameState();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        private static void LoadCommands()
        {
            CommandHandler.AddCommand(new HelpCommand());
            CommandHandler.AddCommand(new ConfigurationCommand());
        }

    }
}
