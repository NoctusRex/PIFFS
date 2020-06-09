using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace PIFFS
{
    static class GameState
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        private static Bitmap ScreenShot { get; set; }

        public static bool IsAttackButtonSelected()
        {
            Console.WriteLine("checking if attack button is selected");

            try
            {
                GetScreenShot();
                return CheckPixelColor(180, 700, 193, 68, 79);

            }
            catch { return false; }
        }

        public static bool IsBattleStartingOrEnding()
        {
            Console.WriteLine("checking for dialoge");

            try
            {
                GetScreenShot();
                return CheckPixelColor(950, 720, 248, 248, 248);
            }
            catch { return false; }
        }

        public static bool IsInBattle()
        {
            Console.WriteLine("checking if is in battle");

            try
            {
                GetScreenShot();
                // Checking positions of the two health bars, for some reason they move up and down
                return
                    CheckPixelColor(39, 137, 0, 0, 0) && (CheckPixelColor(59, 127, 0, 0, 0) || CheckPixelColor(59, 122, 0, 0, 0)) &&
                    CheckPixelColor(621, 695, 0, 0, 0) && (CheckPixelColor(638, 680, 0, 0, 0) || CheckPixelColor(638, 674, 0, 0, 0));
            }
            catch { return false; }

        }

        public static bool IsInLevelUp()
        {
            Console.WriteLine("checking if level up");

            try
            {
                GetScreenShot();
                return
                    CheckPixelColor(650, 45, 39, 39, 39) && CheckPixelColor(1005, 454, 39, 39, 39);
            }
            catch { return false; }
        }

        public static bool IsInDeathScreen()
        {
            Console.WriteLine("Checking if dead");

            try
            {
                GetScreenShot();
                return
                    CheckPixelColor(380, 520, 220, 220, 220) && CheckPixelColor(761, 521, 220, 220, 220);
            }
            catch { return false; }
        }

        public static int GetNextAlivePokemonIndex()
        {
            Console.WriteLine("Getting next alive pokemon");

            try
            {
                GetScreenShot();

                if (CheckPixelColor(496, 54, 0, 104, 32)) return 0;
                if (CheckPixelColor(1008, 86, 0, 104, 32)) return 1;
                if (CheckPixelColor(496, 246, 0, 104, 32)) return 2;
                if (CheckPixelColor(1008, 278, 0, 104, 32)) return 3;
                if (CheckPixelColor(496, 438, 0, 104, 32)) return 4;
                if (CheckPixelColor(1008, 470, 0, 104, 32)) return 5;

                return -1;
            }
            catch { return -1; }
        }

        public static bool CheckPixelColor(int x, int y, int R, int G, int B)
        {
            if (ScreenShot is null) return false;

            Color color = ScreenShot.GetPixel(x, y);

            Console.WriteLine($"Checking pixel {x}, {y} is {color} should be {Color.FromArgb(R, G, B)}");

            return color == Color.FromArgb(R, G, B);
        }

        public static void GetScreenShot()
        {
            ScreenShot = (Bitmap)new ScreenCapture().CaptureWindow(GetForegroundWindow());
        }

        public static bool IsInPokemon()
        {
            return GetCurrentWindowTitle(GetForegroundWindow()) == "Pokémon Infinite Fusion";
        }

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
