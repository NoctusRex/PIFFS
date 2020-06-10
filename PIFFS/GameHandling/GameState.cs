using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace PIFFS
{
    enum GameStates
    {
        Unknown,
        OutOfPokemon,
        OutOfBattle,
        InBattle,
        Dialoge,
        AttackButtonSelected,
        RoundEndLevelUp,
        RoundEndPokemonDied,
        RoundEndNewAbility,
        RoundEndPlayerDied
    }

    static class GameState
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        private static Bitmap ScreenShot { get; set; }

        public static GameStates GetCurrentGameState()
        {
            try
            {
                TakeScreenShot();
            }
            catch { return GameStates.OutOfPokemon; }

            if (!IsInPokemon()) return GameStates.OutOfPokemon;
            bool isInBattle = IsInBattle();
            if (IsDialogeShowing()) return GameStates.Dialoge;
            if (isInBattle && IsAttackButtonSelected()) return GameStates.AttackButtonSelected;
            if (IsInLevelUp()) return GameStates.RoundEndLevelUp;
            if (IsInNewAbility()) return GameStates.RoundEndNewAbility;
            if (IsInDeathScreen()) return GameStates.RoundEndPokemonDied;
            if (IsInPlayerDeathScreen()) return GameStates.RoundEndPlayerDied;
            if (isInBattle) return GameStates.InBattle; // Has to be at the end, in order to check for all events
            if (IsOutOfBattle()) return GameStates.OutOfBattle;

            return GameStates.Unknown;
        }

        public static bool IsInPlayerDeathScreen()
        {
            Console.WriteLine("Checking if is in player death screen");
            return CheckPixelColor(984, 437, 248, 248, 248);
        }

        public static bool IsInNewAbility()
        {
            Console.WriteLine("Checking if is new ability");
            return CheckPixelColor(70, 668, 248, 248, 248) && CheckPixelColor(110, 675, 248, 248, 248) && CheckPixelColor(124, 687, 248, 248, 248);
        }

        public static bool IsOutOfBattle()
        {
            Console.WriteLine("Checking if is not in battle");

            if (IsInBattle() || IsDialogeShowing() || IsInLevelUp() || IsInDeathScreen() || IsInNewAbility() || IsInPlayerDeathScreen()) return false;

            return true;
        }

        public static bool IsAttackButtonSelected()
        {
            Console.WriteLine("Checking if attack button is selected");
            return CheckPixelColor(180, 700, 193, 68, 79); // color of attack button
        }

        public static bool IsDialogeShowing()
        {
            Console.WriteLine("Checking for dialoge");
            return CheckPixelColor(950, 720, 248, 248, 248); // Arrow symbol on bottom right
        }

        public static bool IsInBattle()
        {
            Console.WriteLine("Checking if is in battle");
            // Checking positions of the two health bars, for some reason they move up and down
            return
                CheckPixelColor(39, 137, 0, 0, 0) && (CheckPixelColor(59, 127, 0, 0, 0) || CheckPixelColor(59, 122, 0, 0, 0)) &&
                CheckPixelColor(621, 695, 0, 0, 0) && (CheckPixelColor(638, 680, 0, 0, 0) || CheckPixelColor(638, 674, 0, 0, 0));
        }

        public static bool IsInLevelUp()
        {
            Console.WriteLine("Checking if level up");
            return CheckPixelColor(650, 45, 39, 39, 39) && CheckPixelColor(1005, 454, 23, 23, 23); // Levelup stats window corners
        }

        public static bool IsInDeathScreen()
        {
            Console.WriteLine("Checking if dead");
            return CheckPixelColor(380, 520, 220, 220, 220) && CheckPixelColor(761, 521, 220, 220, 220); // check yes and no button 
        }

        public static int GetNextAlivePokemonIndex()
        {
            Console.WriteLine("Getting next alive pokemon");

            // check color of each pokemon slot, if green it is alive
            if (CheckPixelColor(496, 53, 104, 216, 112)) return 0;
            if (CheckPixelColor(1008, 85, 104, 216, 112)) return 1;
            if (CheckPixelColor(496, 245, 104, 216, 112)) return 2;
            if (CheckPixelColor(1008, 277, 104, 216, 112)) return 3;
            if (CheckPixelColor(496, 437, 104, 216, 112)) return 4;
            if (CheckPixelColor(1008, 469, 104, 216, 112)) return 5;

            return -1;
        }

        public static bool IsSaveButtonSelected()
        {
            try
            {
                TakeScreenShot();
            }
            catch { return false; }

            return CheckPixelColor(710, 340, 88, 88, 80);

        }

        public static bool CheckPixelColor(int x, int y, int R, int G, int B)
        {
            if (ScreenShot is null) return false;

            Color color = ScreenShot.GetPixel(x, y);
            Color compareColor = Color.FromArgb(R, G, B);

            Console.WriteLine($"Checking pixel {x}, {y} is {color} should be {compareColor}");

            return color == compareColor;
        }

        public static void TakeScreenShot() => ScreenShot = (Bitmap)new ScreenCapture().CaptureWindow(GetForegroundWindow());

        public static bool IsInPokemon() => GetCurrentWindowTitle(GetForegroundWindow()) == "Pokémon Infinite Fusion";

        public static string GetCurrentWindowTitle(IntPtr handle)
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

    }
}
