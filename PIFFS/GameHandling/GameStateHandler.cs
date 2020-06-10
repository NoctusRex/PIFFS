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

            Console.WriteLine("End handling game state" + Environment.NewLine);
        }

        private static void HandleRoundEndNewAbility()
        {
            //TODO

            if (Configuration.IgnoreNewAbilities)
            {

                return;
            }
            
        }

        private static void HandleRoundEndPlayerDied()
        {
            //TODO
        }

        private static void HandleInBattle()
        {
            //TODO: Realy needed or use unkown?
            Console.WriteLine("Handling in battle");
            Input.Wait(8);
        }

        private static void HandleRoundEndPokemonDied()
        {
            Console.WriteLine("Pokemon died");
            Input.Space();
            Input.Wait(8);

            Console.WriteLine("Selecting next pokemon");
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

            if(Configuration.AutoSave && LastSave.AddMinutes(Configuration.AutoSaveIntervallInMinutes) > DateTime.Now)
            {
                LastSave = DateTime.Now;
                Save();
            }

            Input.MoveInCirle();
        }

        private static void Save()
        {
            //TODO
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
                    Input.Wait(4);
                    Input.Space();
                    Input.Wait(2);

                    break;
                case Attack.One:
                    Console.WriteLine("Executing attack 1");
                    NextAttack = Attack.Two;

                    Input.Wait(4);
                    Input.Up();
                    Input.Wait(2);
                    Input.Left();
                    Input.Wait(2);
                    Input.Space();
                    Input.Wait(2);

                    break;
                case Attack.Two:
                    Console.WriteLine("Executing attack 2");
                    NextAttack = Attack.Three;

                    Input.Wait(4);
                    Input.Right();
                    Input.Wait(2);
                    Input.Space();
                    Input.Wait(2);

                    break;
                case Attack.Three:
                    Console.WriteLine("Executing attack 3");
                    NextAttack = Attack.Four;

                    Input.Wait(4);
                    Input.Left();
                    Input.Wait(2);
                    Input.Down();
                    Input.Wait(2);
                    Input.Space();
                    Input.Wait(2);

                    break;
                case Attack.Four:
                    Console.WriteLine("Executing attack 4");
                    NextAttack = Attack.One;

                    Input.Wait(4);
                    Input.Right();
                    Input.Wait(2);
                    Input.Space();
                    Input.Wait(2);

                    break;
            }

            Console.WriteLine("Waiting for animation to play");
            Input.Wait(8 * 5);
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
