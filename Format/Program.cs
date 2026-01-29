namespace Format;
using System;

internal class Program
{
    public static List<Spell> spells = new();
    static void Main(string[] args)
    {
        Dictionary<string, Action<string[]>> commands = new()
        {
            { "spell", Spell },
            { "help", Help },
        };
        if (commands.ContainsKey(args.ElementAtOrDefault(0) ?? ""))
        {
            Console.WriteLine($"Executing command: {args[0]}");
            commands[args[0]].Invoke(args);
        }
        else
        {
            Console.WriteLine($"Command {args.ElementAtOrDefault(0)} not found");
        }
    }

    static void Spell(string[] args)
    {
        Console.WriteLine("Inside Spell method.");
        Dictionary<string, Action> spellCommands = new()
        {
            { "add", SpellAdd },
            { "remove", SpellRemove },
            { "edit", SpellEdit },
        };
        if (spellCommands.ContainsKey(args.ElementAtOrDefault(1) ?? ""))
        {
            Console.WriteLine($"Executing spell command: {args[1]}");
        }
        else
        {
            Console.WriteLine($"Spell command {args.ElementAtOrDefault(1)} not found");
        }
    }

    static void Help(string[] args)
    {
        Console.WriteLine("Inside Help method.");
    }

    static void SpellAdd()
    {
        Console.WriteLine("Inside SpellAdd method.");
    }

    static void SpellRemove()
    {
        Console.WriteLine("Inside SpellRemove method.");
    }

    static void SpellEdit()
    {
        Console.WriteLine("Inside SpellEdit method.");
    }
}