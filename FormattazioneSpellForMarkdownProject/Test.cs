using System;
using System.IO;
namespace FormattazioneSpellForMarkdownProject
{
    internal class Test
    {
        public static bool Run(string path = "data")
        {
            string fileName = "nomeFileDiTest";
            path = path.Replace("\\", "/");
            Directory.CreateDirectory(path);
            fileName = $"{path}/{fileName}.md";
            Console.WriteLine($"testo il salvataggio su {fileName}...");
            if (File.Exists(fileName))
            {
                Input.WriteColored($"attenzione: il file {fileName} esiste già, operazione annullata", ConsoleColor.Red);
                return false;
            }
            StreamWriter? writer = null;
            try
            {
                writer = new StreamWriter(fileName);
                writer.WriteLine("contenuto di test");
            }
            catch (FileNotFoundException)
            {
                Console.Error.WriteLine($"file {fileName} non trovato");
                return false;
            }
            catch (IOException)
            {
                Console.Error.WriteLine($"errore di lettura/scrittura su {fileName}");
                return false;
            }
            finally
            {
                writer?.Close();
            }
            return true;
        }
    }
}