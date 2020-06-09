using System;
using System.Threading;
using WindowsInput;
using WindowsInput.Native;

namespace PIFFS
{
    class Program
    {


        static KeyboardSimulator Keyboard { get; set; }
        static int KeyHoldDurationInMilliSeconds { get; set; } = 125;

        enum Attack
        {
            One,
            Two,
            Three,
            Four,
            Init
        }

        static Attack NextAttack { get; set; } = Attack.Init;

        /// <summary>
        /// TODOs: Levelup -> Space, Death -> select Next, New Ability -> User Interaction or NO, NO AP left -> ?, better timing, Check if out of battle instead of time out
        /// </summary>

        static void Main(string[] args)
        {
            Keyboard = new KeyboardSimulator(new InputSimulator());

            while (true)
            {

                if (!GameState.IsInPokemon())
                {
                    Wait(4);
                    Console.WriteLine("Not in pokemon");
                    continue;
                }

                if (GameState.IsBattleStartingOrEnding())
                {
                    Console.WriteLine("Battle starting");

                    Space();
                    Wait(8);

                    while (!GameState.IsInBattle()) { Console.WriteLine("Waiting for battle start"); Thread.Sleep(1000); continue; }

                    Battle();
                }
                else
                {
                    Console.WriteLine("Not in Battle");
                    MoveInCirle();
                }

            }

        }

        static void Battle()
        {
            while (GameState.IsInBattle())
            {
                while (!GameState.IsAttackButtonSelected() && GameState.IsInBattle()) { Console.WriteLine("Selecting attack button"); MoveRight(); Wait(); }

                Space();

                // Alternate between attacks to spread AP use
                switch (NextAttack)
                {
                    case Attack.Init:
                        Console.WriteLine("Executing attack 1");
                        NextAttack = Attack.Two;
                        Wait(4);
                        Space();
                        Wait(2);

                        break;
                    case Attack.One:
                        Console.WriteLine("Executing attack 1");
                        NextAttack = Attack.Two;

                        Wait(4);
                        MoveUp();
                        Wait(2);
                        MoveLeft();
                        Wait(2);
                        Space();
                        Wait(2);

                        break;
                    case Attack.Two:
                        Console.WriteLine("Executing attack 2");
                        NextAttack = Attack.Three;

                        Wait(4);
                        MoveRight();
                        Wait(2);
                        Space();
                        Wait(2);

                        break;
                    case Attack.Three:
                        Console.WriteLine("Executing attack 3");
                        NextAttack = Attack.Four;

                        Wait(4);
                        MoveLeft();
                        Wait(2);
                        MoveDown();
                        Wait(2);
                        Space();
                        Wait(2);

                        break;
                    case Attack.Four:
                        Console.WriteLine("Executing attack 4");
                        NextAttack = Attack.One;

                        Wait(4);
                        MoveRight();
                        Wait(2);
                        Space();
                        Wait(2);

                        break;
                }

                Console.WriteLine("Waiting for animation to play");
                Wait(8 * 5);

                PostBattleRound();
            }

            NextAttack = Attack.Init;
        }

        static void PostBattleRound()
        {
            // Check if battle is still ongoing (transitions screens and stuff)
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Still in battle? Try {i + 1}/5");

                while (PostBattleRoundEvents()) { Console.WriteLine("Handled post battle round event"); }
                if (GameState.IsAttackButtonSelected()) break;
            }
        }

        static bool PostBattleRoundEvents()
        {
            Wait(10); // wait for screen to draw

            if (GameState.IsInLevelUp()) { LevelUp(); return true; }
            if (GameState.IsInDeathScreen()) { Death(); return true; }
            if (GameState.IsBattleStartingOrEnding()) { PostBattleRoundDialoge(); return true; }

            return false;
        }

        static void PostBattleRoundDialoge()
        {
            Console.WriteLine("Battle ended or is interrupted by a dialoge");
            Space();
            Wait(4);
        }

        static void LevelUp()
        {
            Console.WriteLine("Level up");
            Space();
            Wait(4);
            Space();
            Wait(8);
        }

        static void Death()
        {
            Console.WriteLine("Pokemon died");
            Space();
            Wait(8);

            Console.WriteLine("Selecting next pokemon");
            int index = GameState.GetNextAlivePokemonIndex();
            for (int i = 0; i < 6; i++)
            {
                if (i == index) break;
                MoveRight();
                Wait(4);
            }

            Space();
            Wait(4);
            Space();
            while (!GameState.IsAttackButtonSelected()) Wait(4);

            Wait(8);
        }

        static void Wait(decimal modifier = 1)
        {
            Console.WriteLine($"Waiting for {(int)(KeyHoldDurationInMilliSeconds * modifier)}");
            Thread.Sleep((int)(KeyHoldDurationInMilliSeconds * modifier));
        }

        static void MoveInCirle()
        {
            MoveRight();
            MoveDown();
            MoveLeft();
            MoveUp();
        }

        static void Space()
        {
            Console.WriteLine("SPACE");
            Keyboard.KeyDown(VirtualKeyCode.SPACE);
            Wait();
            Keyboard.KeyUp(VirtualKeyCode.SPACE);
        }

        static void MoveUp()
        {
            Console.WriteLine("UP");
            Keyboard.KeyDown(VirtualKeyCode.UP);
            Wait();
            Keyboard.KeyUp(VirtualKeyCode.UP);
        }

        static void MoveDown()
        {
            Console.WriteLine("DOWN");
            Keyboard.KeyDown(VirtualKeyCode.DOWN);
            Wait();
            Keyboard.KeyUp(VirtualKeyCode.DOWN);
        }

        static void MoveLeft()
        {
            Console.WriteLine("LEFT");
            Keyboard.KeyDown(VirtualKeyCode.LEFT);
            Wait();
            Keyboard.KeyUp(VirtualKeyCode.LEFT);
        }

        static void MoveRight()
        {
            Console.WriteLine("RIGHT");
            Keyboard.KeyDown(VirtualKeyCode.RIGHT);
            Wait();
            Keyboard.KeyUp(VirtualKeyCode.RIGHT);
        }

    }
}
