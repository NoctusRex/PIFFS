using System;
using System.Collections.Generic;
using System.Linq;

namespace PIFFS
{
    internal class ConfigurationCommand : Command
    {
        public override List<string> Identifiers => new List<string> { "configurations", "settings", "parameters" };

        protected override void Execute(string[] arguments)
        {

            for (int i = 0; i < arguments.Length; i += 2)
            {
                if (arguments[i].ToLower() == ("autosave"))
                {
                    GameStateHandler.Configuration.AutoSave = bool.Parse(arguments[i + 1]);
                }

                if (arguments[i].ToLower() == ("autosaveintervall"))
                {
                    GameStateHandler.Configuration.AutoSaveIntervallInMinutes = int.Parse(arguments[i + 1]);
                }

                if (arguments[i].ToLower() == ("ignorenewabilities"))
                {
                    GameStateHandler.Configuration.IgnoreNewAbilities = bool.Parse(arguments[i + 1]);
                }
            }

            Console.WriteLine("Auto Save: " + GameStateHandler.Configuration.AutoSave.ToString());
            Console.WriteLine("Auto Save Intervall In Minutes: " + GameStateHandler.Configuration.AutoSaveIntervallInMinutes.ToString());
            Console.WriteLine("Ignore New Abilities: " + GameStateHandler.Configuration.IgnoreNewAbilities.ToString());

        }

        internal override void ValidateArguments(string[] arguments)
        {
            bool autoSave = false;
            bool autoSaveIntervall = false;
            bool ignoreAbilites = false;

            if (arguments.Length % 2 != 0) throw new Exception("Invalid count of configuration arguments");

            autoSave = arguments.Count(x => x.ToLower() == ("autosave")) == 1;
            autoSaveIntervall = arguments.Count(x => x.ToLower() == ("autosaveintervall")) == 1;
            ignoreAbilites = arguments.Count(x => x.ToLower() == ("ignorenewabilities")) == 1;

            if (!autoSave && !autoSaveIntervall && !ignoreAbilites) throw new Exception("No or equal valid configuration arguments specified");

        }
    }
}
