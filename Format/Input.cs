#nullable enable
using System;
using System.Collections.Generic;
namespace Format{

    internal class Input
    {
        public static bool pause = true;
        public static void WriteColored(string text, ConsoleColor color)
        {
            ConsoleColor previousColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = previousColor;
        }

        public static string GetString(string s, ConsoleColor color = ConsoleColor.Blue)
        {
            WriteColored(s, color);
            return GetString();
        }

        public static string GetString()
        {
            return Console.ReadLine() ?? string.Empty;
        }

        public static bool GetBool(string s = "")
        {
            if(!string.IsNullOrEmpty(s))
            {
                WriteColored(s, ConsoleColor.Blue);
            }
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.T)
            {
                return true;
            }else if(key.Key == ConsoleKey.F)
            {
                return false;
            }
            else
            {
                Console.Error.WriteLine("devi inserire un valore booleano\t(T per sì F per no)");
                return GetBool();
            }
        }

        public static int GetInt(int min, string s)
        {
            int a = GetInt(s);
            if(a < min)
            {
                Console.Error.WriteLine($"devi inserire un numero intero maggiore o uguale a {min}");
                return GetInt(min, s);
            }
            else
            {
                return a;
            }
        }

        public static int GetInt(int min, int max, string s)
        {
            int a = GetInt(min, s);
            if(a > max)
            {
                Console.Error.WriteLine($"devi inserire un numero intero compreso tra {min} e {max}");
                return GetInt(min, max, s);
            }
            else
            {
                return a;
            }
        }

        public static int GetInt(string s = "")
        {
            if(int.TryParse(GetString(s), out int result))
            {
                return result;
            }
            else
            {
                Console.Error.WriteLine("devi inserire un numero intero");
                return GetInt();
            }
        }

        public static void Pause()
        {
            if(!pause) return;
            Console.WriteLine("premi invio per continuare...");
            Console.ReadLine();
        }
    }
}