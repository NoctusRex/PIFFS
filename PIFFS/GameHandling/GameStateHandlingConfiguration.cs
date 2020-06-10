namespace PIFFS
{
    class GameStateHandlingConfiguration
    {
        public bool IgnoreNewAbilities { get; set; } = true;
        public bool AutoSave { get; set; } = true;
        public  int AutoSaveIntervallInMinutes { get; set; } = 5;
    }
}
