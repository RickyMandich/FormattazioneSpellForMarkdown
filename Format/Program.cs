namespace Format;
using System;

internal class Program
{
    static void Main(string[] args)
    {
        string[] commands = { "spell", "help" };
        if(commands.Contains(args.ElementAtOrDefault(0)))
        {
            
        }
        else
        {
            Console.WriteLine($"Command {args.ElementAtOrDefault(0)} not found");
        }
    }

    static void Spell(string[] args)
    {
        Console.WriteLine("Inside Spell method.");
        string[] options = { "add", "remove", "edit" };
    }
}