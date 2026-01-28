using System;
using System.Collections.Generic;
using System.IO;

namespace Format
{
    internal class Spell
    {
        protected string name { get; private set; }
        protected int level { get; private set; }
        protected string school { get; private set; }
        protected string castingTime { get; private set; }
        protected string range { get; private set; }
        protected string components { get; private set; }
        protected string duration { get; private set; }
        protected string description { get; private set; }
        protected string higherLevels { get; private set; }
        protected string classes { get; private set; }
        protected string sorgente { get; private set; }

        /**
         * @param name il nome dell'incantesimo
         * @param level il livello dell'incantesimo
         * @param school la scuola dell'incantesimo
         * @param castingTime il tempo di lancio dell'incantesimo
         * @param range il raggio d'azione dell'incantesimo
         * @param components i componenti dell'incantesimo
         * @param duration la durata dell'incantesimo
         * @param description la descrizione dell'incantesimo
         * @param higherLevels l'effetto ai livelli superiori dell'incantesimo
         * @param classes le classi nella cui lista è presente questo incantesimo
         * @param sorgente link alla sorgente dell'incantesimo (opzionale)
         * @constructor Crea un nuovo incantesimo chiedendo all'utente di inserire i vari parametri
         */
        public Spell() {
            name = Input.GetString("inserisci il nome dell'incantesimo:");
            level = Input.GetInt("inserisci il livello dell'incantesimo (0 per trucchetto):");
            school = Input.GetString("inserisci la scuola dell'incantesimo:");
            castingTime = Input.GetString("inserisci il tempo di lancio dell'incantesimo:");
            range = Input.GetString("inserisci la gittata dell'incantesimo:");
            components = Input.GetString("inserisci i componenti dell'incantesimo: (V, S, M [...])");
            duration = Input.GetString("inserisci la durata dell'incantesimo:");
            description = Input.GetString("inserisci il primo paragrafo della descrizione dell'incantesimo:");
            string line;
            while(!string.IsNullOrEmpty(line = Input.GetString("inserisci il prossimo paragrafo della descrizione dell'incantesimo (se sono finiti lascia vuoto)", ConsoleColor.Yellow)))
            {
                description = $"{description}\n{line}";
            }
            higherLevels = Input.GetString("inserisci l'effetto ai livelli superiori dell'incantesimo (se non c'è, lascia vuoto):");
            classes = Input.GetString("inserisci le classi nella cui lista è presente questo incantesimo:");
            sorgente = Input.GetString("inserisci il link sorgente dell'incantesimo (se non c'è, lascia vuoto):");
            Console.WriteLine("incantesimo creato:");
            Console.WriteLine(this.ToMarkdown());
            if (Input.GetBool("vuoi modificare l'incantesimo?"))
            {
                Edit();
            }
        }

        /**
         * create a new Spell asking the params directly to the user
         */
        public Spell(string name, int level, string school, string castingTime, string range, string components, string duration, string description, string higherLevels, string classes, string sorgente)
        {
            this.name = name;
            this.level = level;
            this.school = school;
            this.castingTime = castingTime;
            this.range = range;
            this.components = components;
            this.duration = duration;
            this.description = description;
            this.higherLevels = higherLevels;
            this.classes = classes;
            this.sorgente = sorgente;
        }

        public void Edit()
        {
            string opt = """


                cosa vuoi fare?
                    1) nome
                    2) livello
                    3) scuola
                    4) tempo di lancio
                    5) gittata
                    6) componenti
                    7) durata
                    8) descrizione
                    9) effetto ai livelli superiori
                    10) classi
                    11) sorgente
                    0) uscire
                """;
            bool run = true;
            while (run)
            {
                switch (Input.GetInt(opt))
                {
                    case 0:
                        run = false;
                        break;
                    case 1:
                        this.name = Input.GetString("inserisci il nuovo nome dell'incantesimo");
                        break;
                    case 2:
                        level = Input.GetInt("inserisci il nuovo livello dell'incantesimo");
                        break;
                    case 3:
                        school = Input.GetString("inserisci la nuova scuola dell'incantesimo");
                        break;
                    case 4:
                        castingTime = Input.GetString("inserisci il nuovo tempo di lancio dell'incantesimo");
                        break;
                    case 5:
                        range = Input.GetString("inserisci la nuova gittata dell'incantesimo");
                        break;
                    case 6:
                        components = Input.GetString("inserisci i nuovi componenti (V, S, M [...])");
                        break;
                    case 7:
                        duration = Input.GetString("inserisci la nuova durata");
                        break;
                    case 8:
                        description = Input.GetString("Inserisci il primo paragrafo della nuova descrizione");
                        string line;
                        while (!string.IsNullOrEmpty(line = Input.GetString("inserisci il prossimo paragrafo della nuova descrizione (se sono finiti lascia vuoto)", ConsoleColor.Yellow)))
                        {
                            description = $"{description}\n{line}";
                        }
                        break;
                    case 9:
                        higherLevels = Input.GetString("inserisci il nuovo effetto ai livelli superiori");
                        break;
                    case 10:
                        classes = Input.GetString("inserisci le nuove classi");
                        break;
                    case 11:
                        sorgente = Input.GetString("inserisci la nuova sorgente (link)");
                        break;
                    default:
                        Input.WriteColored("opzione non valida", ConsoleColor.Red);
                        break;
                }
                if (run)
                {
                    Input.Pause();
                    //Console.Clear();
                }
            }
        }

        public static string ToCamelCase(string originalName)
        {
            originalName = originalName.Replace("'", "");
            if (string.IsNullOrWhiteSpace(originalName))
                return string.Empty;

            var parts = originalName
                .ToLower()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0)
                return string.Empty;

            var result = "";

            foreach (var part in parts)
            {
                if (part.Length > 0)
                {
                    result = $"{result}{(part != parts[0] ? "-" : "")}{part}";
                }
            }

            return result;
        }


        public bool printToFile(string directory = "tutti")
        {
            bool byReference = !directory.Equals("tutti");
            string fileName = ToCamelCase(this.name);
            string path = $"{Program.config.Get("OUTPUT_DIRECTORY")}/incantesimi/{directory}";
            Directory.CreateDirectory(path);
            string filePath = $"{path}/{fileName}.md";
            filePath = Input.SanitizePath(filePath);
            Console.WriteLine($"salvo l'incantesimo {name} su {filePath}...");
            if (File.Exists(fileName))
            {
                Input.WriteColored($"attenzione: il file {filePath} esiste già, operazione annullata", ConsoleColor.Red);
                return false;
            }
            StreamWriter? writer = null;
            try
            {
                writer = new StreamWriter(filePath);
                writer.WriteLine(byReference ? this.ToObsidianReference() : this.ToMarkdown());
            }
            catch (IOException)
            {
                Console.Error.WriteLine($"errore di lettura/scrittura su {filePath}");
                return false;
            }
            finally
            {
                writer?.Close();
            }
            if (!byReference)
            {
                string backupFilePath = Input.SanitizePath($"{Program.config.Get("BACKUP_PATH")}data/incantesimi/{fileName}.md");
                Directory.CreateDirectory(Input.SanitizePath($"{Program.config.Get("BACKUP_PATH")}data/incantesimi"));
                try
                {
                    writer = new StreamWriter(backupFilePath);
                    writer.WriteLine(this.ToMarkdown());
                }
                catch (IOException)
                {
                    Console.Error.WriteLine($"errore di lettura/scrittura su {backupFilePath}");
                    return false;
                }
                finally
                {
                    writer?.Close();
                }
            }
            return true;
        }

        public override string ToString()
        {
            string ret = "";
            ret = $"""
                Nome:               {name}
                Livello:            {level}
                Scuola:             {school}
                Tempo di lancio:    {castingTime}
                Raggio d'azione:    {range}
                Componenti:         {components}
                Durata:             {duration}
                Descrizione:        {description}
                <higherLevels>Classi:{classes}
                <source>
                """;
            if (!string.IsNullOrEmpty(higherLevels))
            {
                ret = ret.Replace("<higherLevels>", $"Effetto ai livelli superiori:\t{higherLevels}\n");
            }
            else
            {
                ret = ret.Replace("<higherLevels>", "");
            }

            if (!string.IsNullOrEmpty(sorgente))
            {
                ret = ret.Replace("<source>", $"Sorgente:\t{sorgente}\n");
            }
            else
            {
                ret = ret.Replace("<source>", "");
            }

            return ret;
        }

        public string ToMarkdown()
        {
            string ret = $"## {name}\n*";
            if (level == 0)
            {
                ret = $"{ret}Trucchetto,";
            }
            else
            {
                ret = $"{ret}{level}° livello,";
            }
            ret = $"{ret} {school}*";
            ret = $"{ret}\n\n";
            ret = $"{ret}- **Tempo di lancio:** {castingTime}\n";
            ret = $"{ret}- **Raggio d'azione:** {range}\n";
            ret = $"{ret}- **Componenti:** {components}\n";
            ret = $"{ret}- **Durata:** {duration}\n\n";
            ret = $"{ret}{description}\n\n";
            if (!string.IsNullOrEmpty(higherLevels))
            {
                ret = $"{ret}**Ai livelli superiori:** {higherLevels}\n\n";
            }
            ret = $"{ret}**Classi:** {classes}\n\n";
            if (!string.IsNullOrEmpty(sorgente))
            {
                // render link as clickable markdown but show the URL as link text
                ret = $"{ret}**Sorgente:** [{sorgente}]({sorgente})\n\n";
            }
            ret = $"{ret}---";
            return ret;
        }

        public string ToObsidianReference()
        {
            string ret = $"![[tutti/{ToCamelCase(name).ToLower()}]]";
            return ret;
        }
    }
}