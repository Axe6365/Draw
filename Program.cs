using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Reflection.PortableExecutable;
using System.Xml.Linq;

namespace Draw
{
    internal class Program
    {
        public static ConsoleColor bg = ConsoleColor.Black;
        const UInt16 StatusBarWidht = 30;
        static char ChosenChar;
        static void reset(ConsoleColor color)
        {
            Console.BackgroundColor = color;
            bg = color;
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            //Console.SetCursorPosition(0,Console.WindowHeight-2);
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                for (int j = 0; j < Console.WindowWidth; j++)
                {
                    if (i == 0 && j == 0) Console.Write('┌');
                    else if (i == 0 && j == Console.WindowWidth - 1) Console.Write('┐');
                    else if (i == Console.WindowHeight - 1 && j == 0) Console.Write('└');
                    else if (i == Console.WindowHeight - 1 && j == Console.WindowWidth - 1) Console.Write('┘');
                    else if (i == 0 && Console.WindowWidth - j == StatusBarWidht) Console.Write('┬');
                    else if (i == Console.WindowHeight-1 && Console.WindowWidth - j == StatusBarWidht) Console.Write('┴');
                    else if (i == 0 || i == Console.WindowHeight - 1) Console.Write('─');
                    else if (j == 0 || j == Console.WindowWidth - 1) Console.Write('│');
                    else if (Console.WindowWidth - j == StatusBarWidht) Console.Write('│');
                    else Console.Write(' ');
                }
                Console.WriteLine();
            }
            Console.SetCursorPosition(0,0);
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            ColorRefresh(color);
            Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
        }
        static void ColorRefresh(ConsoleColor color)
        {
            string[] colors = Enum.GetNames(typeof(ConsoleColor));
            (int x, int y) = Console.GetCursorPosition();
            int choosedColor = (int)color+1;
            for (int i = 1; i <= 16; i++)
            {
                if (choosedColor == i)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.SetCursorPosition(Console.WindowWidth - StatusBarWidht + 1, i);
                Console.WriteLine($"{colors[i-1]} ");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.SetCursorPosition(0,0);
            Console.SetCursorPosition(x, y);
        }
        static void OppRefresh()
        {
            (int x, int y) = Console.GetCursorPosition();
            char[] opp = { '█', '▓', '▒', '░' };
            for (int i = 0; i < 4; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth - StatusBarWidht + 1, i + 18);
                Console.Write($"{opp[i]}");
                if (ChosenChar == opp[i])
                {
                    Console.Write(" Chosen");
                }
                else
                {
                    while (Console.CursorLeft < Console.WindowWidth - 1)
                    {
                        Console.Write(' ');
                    }
                }
            }
            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(x, y);
        }
        static void OpenHelp()
        {
            const int helpSize = 30;
            char[,] behind = new char[helpSize, helpSize];
            (int x,int y) = Console.GetCursorPosition();
            int a = Console.WindowWidth / 2 - helpSize / 2 - StatusBarWidht / 2;
            int b = Console.WindowHeight / 2 - helpSize / 2;
            Console.SetCursorPosition(a, b);
            for (int i = b; i < b+helpSize; i++)
            {
                for (int j = a; j < a+helpSize; j++)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write(' ');
                }
                Console.WriteLine();
                Console.CursorLeft = a;
            }
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
            Console.SetWindowSize(190, 50);
            reset(ConsoleColor.Black);
            ConsoleKey input;
            ConsoleColor color = ConsoleColor.Black;
            char character = '█';
            bool drawing = false;
            bool helpIsOpen = false;
            do
            {
                input = Console.ReadKey(true).Key;
                switch (input)
                {
                    case ConsoleKey.DownArrow:
                        if(helpIsOpen)break;
                        if (Console.CursorTop < Console.WindowHeight-2)
                        {
                            Console.CursorTop++;
                        }
                        CheckDraw(drawing, color, character);
                        break;
                    case ConsoleKey.UpArrow:
                        if (helpIsOpen) break;
                        if (Console.CursorTop > 0)
                        {
                            Console.CursorTop--;
                        }
                        CheckDraw(drawing, color, character);
                        break;
                    case ConsoleKey.RightArrow:
                        if (helpIsOpen) break;
                        if (Console.CursorLeft < Console.WindowWidth-StatusBarWidht-1)
                        {
                            Console.CursorLeft++;
                        }
                        CheckDraw(drawing, color, character);
                        break;
                    case ConsoleKey.LeftArrow:
                        if (helpIsOpen) break;
                        if (Console.CursorLeft > 1)
                        {
                            Console.CursorLeft--;
                        }
                        CheckDraw(drawing, color, character);
                        break;
                    case ConsoleKey.Spacebar:
                        Console.ForegroundColor = color;
                        Console.Write(character);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.CursorLeft--;
                        break;
                    case ConsoleKey.PageUp:
                        if(color > ConsoleColor.Black)
                        {
                            color -= 1;
                            ColorRefresh(color);
                        }
                        break;
                    case ConsoleKey.PageDown:
                        if(color < ConsoleColor.White)
                        {
                            color += 1;
                            ColorRefresh(color);
                        }
                        break;
                    case ConsoleKey.Backspace:
                        reset(ConsoleColor.Black);
                        break;
                    case ConsoleKey.F:
                        reset(color);
                        break;
                    case ConsoleKey.D1:
                        character = '█';
                        ChosenChar = character;
                        OppRefresh();
                        break;
                    case ConsoleKey.D2:
                        character = '▓';
                        ChosenChar = character;
                        OppRefresh();
                        break;
                    case ConsoleKey.D3:
                        character = '▒';
                        ChosenChar = character;
                        OppRefresh();
                        break;
                    case ConsoleKey.D4:
                        character = '░';
                        ChosenChar = character;
                        OppRefresh();
                        break;
                    case ConsoleKey.Delete:
                        Console.BackgroundColor = bg;
                        Console.Write(' ');
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.CursorLeft--;
                        break;
                    /*case ConsoleKey.S:
                        //Save();
                        break;*/
                    case ConsoleKey.T:
                        if(drawing)drawing = false;
                        else drawing = true;
                        break;
                    case ConsoleKey.F1:
                        if (helpIsOpen) helpIsOpen = false;
                        else helpIsOpen = true; OpenHelp();
                        break;
                }
            }
            while (input != ConsoleKey.Q);
        }
        static void CheckDraw(bool drawing, ConsoleColor color, char character)
        {
            if (drawing)
            {
                Console.ForegroundColor = color;
                Console.Write(character);
                Console.ForegroundColor = ConsoleColor.White;
                Console.CursorLeft--;
            }
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