using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Xml.Linq;

namespace Draw
{
    internal class Program
    {
        public static ConsoleColor bg = ConsoleColor.Black;
        static void reset(ConsoleColor color)
        {
            Console.SetWindowSize(190,40);
            Console.BackgroundColor = color;
            bg = color;
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0,Console.WindowHeight-2);
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write('-');
            }
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.WriteLine("Space - Rajzolás  PageUp - Színváltás felfele  PageDown - Színváltás lefele  F1 - F4 ecset erőssége  Backspace - teljes törlés  F - Háttércsere kiválasztott színre    Color:");
            ColorRefresh(color);
            Console.SetCursorPosition(0,0);
            Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
        }
        static void ColorRefresh(ConsoleColor color)
        {
            (int x, int y) = Console.GetCursorPosition();
            Console.SetCursorPosition(Console.WindowWidth - 17, Console.WindowHeight - 1);
            Console.Write(color+"              ");
            Console.SetCursorPosition(0,0);
            Console.SetCursorPosition(x, y);
        }
        /*struct Cell
        {
            public char Character;
            public ConsoleColor Color;
        }
        static Cell[,] canvas;*/
        static void Main(string[] args)
        {
            Console.Title = "Draw program";
            reset(ConsoleColor.Black);
            ConsoleKey input;
            char character = '█';
            ConsoleColor color = ConsoleColor.Black;
            do
            {
                input = Console.ReadKey(true).Key;
                switch (input)
                {
                    case ConsoleKey.DownArrow:
                        if (Console.CursorTop < Console.WindowHeight-3)
                        {
                            Console.CursorTop++;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (Console.CursorTop > 0)
                        {
                            Console.CursorTop--;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (Console.CursorLeft < Console.WindowWidth-1)
                        {
                            Console.CursorLeft++;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if(Console.CursorLeft > 0)
                        {
                            Console.CursorLeft--;
                        }
                        break;
                    case ConsoleKey.Spacebar:
                        Console.ForegroundColor = color;
                        Console.Write(character);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.CursorLeft--;
                        break;
                    case ConsoleKey.PageUp:
                        if(color < ConsoleColor.White)
                        {
                            color += 1;
                            ColorRefresh(color);
                        }
                        break;
                    case ConsoleKey.PageDown:
                        if(color > ConsoleColor.Black)
                        {
                            color -= 1;
                            ColorRefresh(color);
                        }
                        break;
                    case ConsoleKey.Backspace:
                        reset(ConsoleColor.Black);
                        break;
                    case ConsoleKey.F:
                        reset(color);
                        break;
                    case ConsoleKey.F1:
                        character = '█';
                        break;
                    case ConsoleKey.F2:
                        character = '▓';
                        break;
                    case ConsoleKey.F3:
                        character = '▒';
                        break;
                    case ConsoleKey.F4:
                        character = '░';
                        break;
                    case ConsoleKey.Delete:
                        Console.BackgroundColor = bg;
                        Console.Write(' ');
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.CursorLeft--;
                        break;
                    case ConsoleKey.S:
                        Save();
                        break;
                }
            }
            while (input != ConsoleKey.Q);
        }
        static void Save()
        {
            /*using(StreamWriter writer = new StreamWriter(""))
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                for (int j = 0; j < Console.WindowWidth; j++)
                {
                    var cell = canvas[i, j];

                }
            }*/
        }
    }
}