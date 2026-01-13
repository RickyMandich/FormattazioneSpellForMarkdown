using System;
using System.Collections.Generic;
using System.Text;

namespace FormattazioneSpellForMarkdownProject
{
    internal class Spell
    {
        protected string name {get; }
        protected int level {get; }
        protected string school {get; }
        protected string castingTime {get; }
        protected string range {get; }
        protected string components {get; }
        protected string duration {get; }
        protected string description {get; }
        protected string higherLevels {get; }
        protected string classes {get; }

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
         * @constructor Crea un nuovo incantesimo chiedendo all'utente di inserire i vari parametri
         */
        public Spell() {
            name = Input.GetString("inserisci il nome dell'incantesimo:");
            level = Input.GetInt("inserisci il livello dell'incantesimo (0 per trucchetto):");
            school = Input.GetString("inserisci la scuola dell'incantesimo:");
            castingTime = Input.GetString("inserisci il tempo di lancio dell'incantesimo:");
            range = Input.GetString("inserisci il raggio d'azione dell'incantesimo:");
            components = Input.GetString("inserisci i componenti dell'incantesimo: (V, S, M [...])");
            duration = Input.GetString("inserisci la durata dell'incantesimo:");
            description = Input.GetString("inserisci il primo paragrafo della descrizione dell'incantesimo:");
            string line;
            while(!string.Empty(line = Input.GetString("inserisci il prossimo paragrafo della descrizione dell'incantesimo (se sono finiti lascia vuoto")))
            {
                description = $"{description}\n{line}";
            }
            higherLevels = Input.GetString("inserisci l'effetto ai livelli superiori dell'incantesimo (se non c'è, lascia vuoto):");
            classes = Input.GetString("inserisci le classi nella cui lista è presente questo incantesimo:");
        }

        /**
         * create a new Spell asking the params directly to the user
         */
        public Spell(string name, int level, string school, string castingTime, string range, string components, string duration, string description, string higherLevels, string classes)
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
                """;
            if(!string.IsNullOrEmpty(higherLevels))
            {
                ret = ret.Replace("<higherLevels>", $"Effetto ai livelli superiori:\t{higherLevels}\n");
            }else{
                ret = ret.Replace("<higherLevels>", "");
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
            ret = $"{ret}**Classi:** {classes}\n\n---";
            return ret;
        }
    }
}
