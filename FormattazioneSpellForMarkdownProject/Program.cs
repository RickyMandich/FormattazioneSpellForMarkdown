namespace FormattazioneSpellForMarkdownProject
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    internal class Program
    {
        public static Input.Settings config;
        public static bool windows; //if true is runned in windows, if false in wsl debian
        static void exec()
        {
            string opt = """
                cosa vuoi fare?
                    1) creare un incantesimo
                    2) ottenere l'elenco degli incantesimi formattati in markdown
                    3) inserire tutti gli incantesimi creati in un file markdown
                    4) svuotare l'elenco attuale degli incantesimi
                    5) modificare le impostazioni
                    6) modificare la cartella di destinazione degli incantesimi
                    0) uscire
                """;
            if (config.Has("OUTPUT_DIRECTORY"))
            {
                Input.WriteColored($"cartella di destinazione attuale: {config.Get("OUTPUT_DIRECTORY")}\n", ConsoleColor.Green);
            }
            else
            {
                config.Add("OUTPUT_DIRECTORY", Input.GetString("inserisci la cartella di output (verrà ricordata anche tra esecuzioni diverse)"));
            }
            if (config.Has("BACKUP_PATH"))
            {
                Input.WriteColored($"cartella di destinazione attuale: {config.Get("BACKUP_PATH")}\n", ConsoleColor.Green);
            }
            else
            {
                config.Add("BACKUP_PATH", Input.GetString("inserisci la cartella di backup (verrà ricordata anche tra esecuzioni diverse)"));
            }
            bool run = true;
            List<Spell> spells = new List<Spell>();
            while (run) {
                switch (Input.GetInt(opt))
                {
                    case 0:
                        run = false;
                        break;
                    case 1:
                        spells.Add(new Spell());
                        break;
                    case 2:
                        foreach (Spell spell in spells)
                        {
                            Console.WriteLine(spell.ToMarkdown());
                        }
                        break;
                    case 3:
                        printToFile(spells);
                        break;
                    case 4:
                        spells = new List<Spell>();
                        break;
                    case 5:
                        config.Edit();
                        break;
                    case 6:
                        config.Set("OUTPUT_DIRECTORY", Input.GetString("inserisci la nuova cartella di destinazione del salvataggio degli incantesimi"));
                        break;
                    default:
                        Console.WriteLine("opzione non valida");
                        break;
                }
                if (run == true)
                {
                    Input.Pause();
                    //Console.Clear();
                }
            }
            config.Save();
        }

        static void printToFile(List<Spell> spells)
        {
            foreach (Spell spell in spells)
            {
                spell.printToFile();
                if(Input.GetBool("vuoi salvare questo incantesimo anche in una cartella?"))
                {
                    string dir = Input.GetString("inserisci il nome della cartella in cui lo vuoi copiare, se vuoi copiarlo in più di una cartella inserisci più nomi senza spazi separati da `;`");
                    string[] dirs = dir.Split(';');
                    foreach (string d in dirs)
                    {
                        spell.printToFile(d);
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            windows = RuntimeInformation
                                               .IsOSPlatform(OSPlatform.Windows);
            if (args.Length > 0 && args[0] == "--no-pause")
            {
                Input.pause = false;
            }
            config = new Input.Settings(["OUTPUT_DIRECTORY=data", "SYSTEM_PATH=system"]);
            exec();
            Input.Pause();
        }
    }
}