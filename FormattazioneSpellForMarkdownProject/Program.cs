namespace FormattazioneSpellForMarkdownProject
{
    using System;
    internal class Program
    {
        public static Input.Settings config;
        static void exec()
        {
            string opt = """
                cosa vuoi fare?
                    1) creare un incantesimo
                    2) ottenere l'elenco degli incantesimi formattati in markdown
                    3) inserire tutti gli incantesimi creati in un file markdown
                    4) svuotare l'elenco attuale degli incantesimi
                    0) uscire
                """;
            bool run = true;
            List<Spell> spells = new List<Spell>();
            while (run) {
                switch (Input.GetInt(0, 4, opt))
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
                    default:
                        Console.WriteLine("opzione non valida");
                        break;
                }
                if (run == true)
                {
                    Input.Pause()
                    Console.Clear();
                }
            }
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
                        spell.printToFile(true, d);
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            config = new Input.Settings();
            Input.Pause();
        }
    }
}