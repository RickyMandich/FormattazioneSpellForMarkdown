using System;
using System.Collections.Generic;
namespace FormattazioneSpellForMarkdownProject {

    internal class Input
    {
        internal class Settings
        {
            private readonly string _filePath;
            private readonly Dictionary<string, string> _map;

            internal Settings()
            {
                _filePath = "FormatSpell.config";
                _map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                try
                {
                    if (System.IO.File.Exists(_filePath))
                    {
                        foreach (var rawLine in System.IO.File.ReadAllLines(_filePath))
                        {
                            if (string.IsNullOrWhiteSpace(rawLine))
                                continue;

                            var line = rawLine.Trim();
                            if (line.StartsWith("#"))
                                continue;

                            int eq = line.IndexOf('=');
                            if (eq < 0)
                                continue;

                            var key = line.Substring(0, eq).Trim();
                            var value = line.Substring(eq + 1).Trim();

                            if (key.Length == 0)
                                continue;

                            _map[key] = value;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to read settings file '{_filePath}': {ex.Message}");
                }
            }

            public string? Get(string key)
            {
                if (string.IsNullOrEmpty(key))
                    return null;

                return _map.TryGetValue(key, out var value) ? value : null;
            }

            public bool Set(string key, string value)
            {
                if (string.IsNullOrEmpty(key))
                    return false;

                if (!_map.ContainsKey(key))
                    return false;

                _map[key] = value ?? string.Empty;
                return true;
            }

            public bool Add(string key, string value)
            {
                if (string.IsNullOrEmpty(key))
                    return false;

                if (_map.ContainsKey(key))
                    return false;

                _map[key] = value ?? string.Empty;
                return true;
            }

            public bool Remove(string key)
            {
                if (string.IsNullOrEmpty(key))
                    return false;

                return _map.Remove(key);
            }

            public void Save()
            {
                try
                {
                    var lines = new System.Collections.Generic.List<string>(_map.Count);
                    foreach (var kvp in _map)
                    {
                        lines.Add($"{kvp.Key}={kvp.Value}");
                    }

                    System.IO.File.WriteAllLines(_filePath, lines);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to save settings file '{_filePath}': {ex.Message}");
                }
            }
        }
        public static void WriteColored(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
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

        public static int GetInt(string s = "")
        {
            try
            {
                return Int32.Parse(GetString(s));
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

        public static void Pause()
        {
            Console.WriteLine("premi invio per continuare...");
            Console.ReadLine();
        }
    }
}