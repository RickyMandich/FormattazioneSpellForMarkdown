namespace FormattazioneSpellForMarkdownProject
{
    using System;
    internal class Program
    {
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
                    Input.WriteColored("premi un tasto per continuare...", ConsoleColor.White);
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
            //List<Spell> spells = new List<Spell>();
            //spells.Add(new Spell(
            //    "Palla di fuoco",
            //    3,
            //    "Evocazione",
            //    "1 azione",
            //    "150 piedi",
            //    "V, S, M (una piccola palla di guano di pipistrello e zolfo)",
            //    "Istantaneo",
            //    "Una esplosione di fuoco scaturisce da un punto che puoi vedere entro gittata e irradia in un raggio di 20 piedi. Ogni creatura nella zona deve effettuare un tiro salvezza su Destrezza. Una creatura subisce 8d6 danni da fuoco se fallisce il tiro salvezza, o la metà dei danni se lo supera. La palla di fuoco ignora la copertura. La palla di fuoco infligge il doppio dei danni ai bersagli in un'area di 20 piedi cubici all'interno dell'area dell'effetto.\n\nIl fuoco si propaga rapidamente e ignora gli ostacoli non infiammabili. Una volta lanciata, la palla di fuoco incenerisce tutto ciò che è infiammabile nella zona.",
            //    "Gli incantesimi di livello superiore: Quando lanci questo incantesimo usando uno slot incantesimo di 4° livello o superiore, i danni aumentano di 1d6 per ogni livello dello slot oltre il 3°.",
            //    "Mago, Stregone"
            //    ));
            //spells.Add(new Spell(
            //    "Scudo",
            //    1,
            //    "Evocazione",
            //    "Reazione, che puoi usare quando vieni colpito da un attacco o sei bersaglio di un incantesimo",
            //    "Personale",
            //    "V, S",
            //    "1 round",
            //    "Un campo magico protettivo si manifesta intorno a te fino all'inizio del tuo prossimo turno. Finché lo scudo è attivo, hai un bonus di +5 alla CA, incluso contro l'attacco che ha attivato questo incantesimo.\n\nLo scudo annulla anche l'effetto dell'incantesimo Missile Magico.",
            //    "",
            //    "Mago, Stregone"
            //    ));
            //bool success = printToFile(spells);
            //if (success)
            //{
            //    Console.WriteLine("file salvato con successo");
            //}
            //else
            //{
            //    Console.WriteLine("si è verificato un errore durante il salvataggio del file");
            //}
            exec();
            Input.PauseBeforeExit();
        }
    }
}