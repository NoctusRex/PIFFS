using System;
using System.Threading;
using WindowsInput;
using WindowsInput.Native;

namespace PIFFS
{
    static class Input
    {
        static KeyboardSimulator Keyboard { get; set; } = new KeyboardSimulator(new InputSimulator());
        public static int KeyHoldDurationInMilliSeconds { get; private set; } = 125;

        public static void LeftRight()
        {
            Left();
            Wait();
            Right();
        }

        public static void Space()
        {
            Console.WriteLine("SPACE DOWN");
            Keyboard.KeyDown(VirtualKeyCode.SPACE);
            Wait();
            Keyboard.KeyUp(VirtualKeyCode.SPACE);
            Console.WriteLine("SPACE UP");
        }

        public static void ESC()
        {
            Console.WriteLine("ESC DOWN");
            Keyboard.KeyDown(VirtualKeyCode.ESCAPE);
            Wait();
            Keyboard.KeyUp(VirtualKeyCode.ESCAPE);
            Console.WriteLine("ESC UP");
        }

        public static void Up()
        {
            Console.WriteLine("UP DOWN");
            Keyboard.KeyDown(VirtualKeyCode.UP);
            Wait();
            Keyboard.KeyUp(VirtualKeyCode.UP);
            Console.WriteLine("UP UP");
        }

        public static void Down()
        {
            Console.WriteLine("DOWN DOWN");
            Keyboard.KeyDown(VirtualKeyCode.DOWN);
            Wait();
            Keyboard.KeyUp(VirtualKeyCode.DOWN);
            Console.WriteLine("DOWN UP");
        }

        public static void Left()
        {
            Console.WriteLine("LEFT DOWN");
            Keyboard.KeyDown(VirtualKeyCode.LEFT);
            Wait();
            Keyboard.KeyUp(VirtualKeyCode.LEFT);
            Console.WriteLine("LEFT UP");
        }

        public static void Right()
        {
            Console.WriteLine("RIGHT DOWN");
            Keyboard.KeyDown(VirtualKeyCode.RIGHT);
            Wait();
            Keyboard.KeyUp(VirtualKeyCode.RIGHT);
            Console.WriteLine("RIGHT UP");
        }

        public static void Wait(decimal modifier = 1)
        {
            Console.WriteLine($"Waiting for {(int)(KeyHoldDurationInMilliSeconds * modifier)}");
            Thread.Sleep((int)(KeyHoldDurationInMilliSeconds * modifier));
        }

    }
}
