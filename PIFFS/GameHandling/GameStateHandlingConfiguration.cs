using System.Runtime.InteropServices;

namespace PIFFS
{
    class GameStateHandlingConfiguration
    {
        public bool IgnoreNewAbilities { get; set; } = false;
        public bool AutoSave { get; set; } = true;
        public  int AutoSaveIntervallInMinutes { get; set; } = 5;

    }
}
