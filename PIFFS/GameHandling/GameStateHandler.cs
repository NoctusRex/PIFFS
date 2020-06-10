using System;

namespace PIFFS
{
    static class GameStateHandler
    {
        private enum Attack
        {
            One,
            Two,
            Three,
            Four,
            Init
        }
        private static Attack NextAttack { get; set; } = Attack.Init;
        public static GameStateHandlingConfiguration Configuration { get; set; } = new GameStateHandlingConfiguration();
        private static DateTime LastSave { get; set; } = DateTime.Now;

        public static void HandleGameState()
        {
            Console.WriteLine("Start handling game state");
            switch (GameState.GetCurrentGameState())
            {
                case GameStates.OutOfPokemon:
                    HandleOutOfPokemon();
                    break;

                case GameStates.InBattle:
                    HandleInBattle();
                    break;

                case GameStates.AttackButtonSelected:
                    HandleAttackButtonSelected();
                    break;

                case GameStates.Dialoge:
                    HandleDialoge();
                    break;

                case GameStates.OutOfBattle:
                    HandleOutOfBattle();
                    break;

                case GameStates.RoundEndLevelUp:
                    HandleRoundEndLevelUp();
                    break;

                case GameStates.RoundEndPokemonDied:
                    HandleRoundEndPokemonDied();
                    break;

                case GameStates.RoundEndPlayerDied:
                    HandleRoundEndPlayerDied();
                    break;

                case GameStates.RoundEndNewAbility:
                    HandleRoundEndNewAbility();
                    break;

                case GameStates.Unknown:
                    HandleUnkownGameState();
                    break;
            }

            Input.Wait(4);
            Console.WriteLine("End handling game state" + Environment.NewLine);
        }

        private static void HandleRoundEndNewAbility()
        {
            Console.WriteLine("Handling new ability");

            Input.Wait(3);

            if (Configuration.IgnoreNewAbilities)
            {
                Input.Right();
                Input.Wait(3);
                Input.Space();
                Input.Wait(3);
                Input.Space();
                Input.Wait(3);
                return;
            }

            Input.Space();
            Input.Wait(3);
            Input.Space();
            Input.Wait(3);
            Input.Space();
            Input.Wait(3);
            Input.Space();
            Input.Wait(3);
            Input.Space();
            Input.Wait(3);
            Input.Space();
            Input.Wait(3);
            Input.Space();
            Input.Wait(3);
            Input.Space();
            Input.Wait(3);
        }

        private static void HandleRoundEndPlayerDied()
        {
            HandleDialoge();
            Save();
            throw new Exception("You died looser!");
        }

        private static void HandleInBattle()
        {
            Console.WriteLine("Handling in battle");

            while (!GameState.IsAttackButtonSelected() && GameState.IsInBattle()) { Input.Right(); Input.Wait(4); GameState.TakeScreenShot(); }

            Input.Wait(8);
        }

        private static void HandleRoundEndPokemonDied()
        {
            Console.WriteLine("Pokemon died");
            Input.Space();
            Input.Wait(16);

            Console.WriteLine("Selecting next pokemon");
            GameState.TakeScreenShot();
            int index = GameState.GetNextAlivePokemonIndex();
            for (int i = 0; i < 6; i++)
            {
                if (i == index) break;
                Input.Right();
                Input.Wait(4);
            }

            Input.Space();
            Input.Wait(4);
            Input.Space();
            Input.Wait(8);
        }

        private static void HandleRoundEndLevelUp()
        {
            Console.WriteLine("Handling round end level up");
            Input.Space();
            Input.Wait(4);
            Input.Space();
            Input.Wait(8);
        }

        private static void HandleOutOfPokemon()
        {
            Console.WriteLine("Handling out of pokemon");
            Input.Wait(8);
        }

        private static void HandleOutOfBattle()
        {
            Console.WriteLine("Handling ouf of battle");

            if (Configuration.AutoSave && LastSave.AddMinutes(Configuration.AutoSaveIntervallInMinutes) < DateTime.Now)
            {
                LastSave = DateTime.Now;
                Save();
            }

            Input.LeftRight();
        }

        private static void Save()
        {
            Console.WriteLine("Handling Save");
            Input.Wait(4);
            Input.ESC();

            while (!GameState.IsSaveButtonSelected() && !GameState.IsInBattle()) { Console.WriteLine("Selecting save button");  Input.Down(); Input.Wait(4); }

            Input.Space();
            Input.Wait(4);
            Input.Space();
            Input.Wait(6);
            Input.Space();
        }

        private static void HandleAttackButtonSelected()
        {
            Console.WriteLine("Handling attack");

            Input.Space();

            // Alternate between attacks to spread AP use
            switch (NextAttack)
            {
                case Attack.Init:
                    Console.WriteLine("Executing attack 1");
                    NextAttack = Attack.Two;
                    Input.Wait(6);
                    Input.Space();
                    Input.Wait(4);

                    break;
                case Attack.One:
                    Console.WriteLine("Executing attack 1");
                    NextAttack = Attack.Two;

                    Input.Wait(6);
                    Input.Up();
                    Input.Wait(4);
                    Input.Left();
                    Input.Wait(4);
                    Input.Space();
                    Input.Wait(4);

                    break;
                case Attack.Two:
                    Console.WriteLine("Executing attack 2");
                    NextAttack = Attack.Three;

                    Input.Wait(6);
                    Input.Right();
                    Input.Wait(4);
                    Input.Space();
                    Input.Wait(4);

                    break;
                case Attack.Three:
                    Console.WriteLine("Executing attack 3");
                    NextAttack = Attack.Four;

                    Input.Wait(6);
                    Input.Left();
                    Input.Wait(4);
                    Input.Down();
                    Input.Wait(4);
                    Input.Space();
                    Input.Wait(4);

                    break;
                case Attack.Four:
                    Console.WriteLine("Executing attack 4");
                    NextAttack = Attack.One;

                    Input.Wait(6);
                    Input.Right();
                    Input.Wait(4);
                    Input.Space();
                    Input.Wait(4);

                    break;
            }

        }

        private static void HandleDialoge()
        {
            Console.WriteLine("Handling dialoge");
            Input.Space();
            Input.Wait(8);
        }

        private static void HandleUnkownGameState()
        {
            Console.WriteLine("Unkown game state");
            Input.Wait(8);
        }

    }
}
