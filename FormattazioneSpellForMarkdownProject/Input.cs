#nullable enable
using System;
using System.Collections.Generic;
namespace FormattazioneSpellForMarkdownProject{

    internal class Input
    {
        internal class Settings
        {
            private readonly string _filePath;
            private readonly Dictionary<string, string> _map;

            internal Settings(string[] optionsNeeded)
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
                foreach (string optionNeeded in optionsNeeded)
                {
                    if (!string.IsNullOrEmpty(optionNeeded) && optionNeeded.Contains("=") && optionNeeded.IndexOf("=") > 0)
                    {
                        Add(optionNeeded.Split('=')[0], optionNeeded.Split('=')[1]);
                    }
                }
            }

            /**
             * Get the value of a setting by its key.
             * Returns null if the key does not exist.
             */
            public string? Get(string key)
            {
                if (string.IsNullOrEmpty(key))
                    return null;

                return _map.TryGetValue(key, out var value) ? value : null;
            }

            /**
             * Set the value of an existing setting.
             * Returns true if the setting was updated, if the key does not exist it create it by the Add method.
             */
            public bool Set(string key, string value)
            {
                if (string.IsNullOrEmpty(key))
                    return false;

                if (!_map.ContainsKey(key))
                    return Add(key, value);

                _map[key] = value ?? string.Empty;
                Save();
                return true;
            }

            /**
             * Add a new setting.
             * Returns false if the key already exists.
             */
            public bool Add(string key, string value)
            {
                if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("impossibile aggiungere un'impostazione con chiave o valore vuoti");
                    return false;
                }

                if (_map.ContainsKey(key))
                    return false;

                _map[key] = value ?? string.Empty;
                return true;
            }

            /**
             * Remove a setting by its key.
             * Returns false if the key does not exist.
             */
            public bool Remove(string key)
            {
                if (string.IsNullOrEmpty(key))
                    return false;

                return _map.Remove(key);
            }

            /**
             * Save the current settings to the configuration file.
             */
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

            /**
             *  Update an existing option interactively.
             */
            private void UpdateAnOption()
            {
                Console.WriteLine("impostazioni correnti:");
                int tot = _map.Count;
                string[] keys = new string[tot];
                int i = 0;
                foreach (var kvp in _map)
                {
                    keys[i++] = kvp.Key;
                    Console.WriteLine($"{i})    {kvp.Key} = {kvp.Value}");
                }
                string key = keys[GetInt(0, tot-1, "inserisci l'indice dell'impostazione da modificare (oppure premi invio per uscire):")];
                string value = GetString($"inserisci il nuovo valore per l'impostazione '{key}':");
                _map[key] = value;
                Save();
                Console.WriteLine("impostazione salvata");
            }

            /**
             * Edit the settings interactively.
             */
            public void Edit()
            {
                string opt = """
                cosa vuoi fare?
                    1) modificare un'impostazione
                    2) aggiungere una nuova impostazione
                    3) rimuovere un'impostazione
                    4) vedere tutte le impostazioni
                    0) uscire da questo menu
                """;
                bool run = true;
                while (run)
                {
                    switch (Input.GetInt(0, 5, opt))
                    {
                        case 0:
                            run = false;
                            break;
                        case 1:
                            this.UpdateAnOption();
                            break;
                        case 2:
                            this.Add(Input.GetString("inserisci il nome della nuova impostazione"), Input.GetString("inserisci il valore della nuova impostazione"));
                            break;
                        case 3:
                            this.Remove(Input.GetString("inserisci il nome dell'impostazione da eliminare"));
                            break;
                        case 4:
                            this.ShowSettings();
                            break;
                        default:
                            Console.WriteLine("opzione non valida");
                            break;
                    }
                    if (run == true)
                    {
                        Input.Pause();
                        Console.Clear();
                    }
                }
            }

            /**
             * show all current settings
             */
            private void ShowSettings()
            {
                Console.WriteLine("impostazioni correnti:");
                foreach (var kvp in _map)
                {
                    Console.WriteLine($"{kvp.Key} = {kvp.Value}");
                }
            }
        }
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