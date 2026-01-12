using System;
namespace FormattazioneSpellForMarkdownProject {

    internal class Input
    {
        static void WriteColored(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static string GetString(string s)
        {
            WriteColored(s, ConsoleColor.Blue);
            return GetString();
        }

        public static string GetString()
        {
            return Console.ReadLine();
        }

        public static bool GetBool(string s)
        {
            Console.WriteLine(s);
            return GetBool();
        }

        public static bool GetBool()
        {
            try
            {
                return Boolean.Parse(GetString());
            }
            catch (FormatException)
            {
                Console.Error.WriteLine("devi inserire un valore booleano\t(true per sì e false per no)");
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

        public static int GetInt(string s)
        {
            Console.WriteLine(s);
            return GetInt();
        }

        public static int GetInt()
        {
            try
            {
                return Int32.Parse(GetString());
            }
            catch (FormatException)
            {
                Console.Error.WriteLine("devi inserire un numero intero");
                return GetInt();
            }
            catch (OverflowException)
            {
                Console.Error.WriteLine("devi inserire un numero intero di massimo 9 cifre");
                return GetInt();
            }
        }

        public static void PauseBeforeExit()
        {
            Console.WriteLine("premi invio per uscire...");
            Console.ReadLine();
        }
    }
}