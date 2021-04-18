using ASDLib;
using System;
using System.Collections.Generic;

public static class CommandProcessor
{
    private static List<Entry<Key, Value>> controlList = new List<Entry<Key, Value>>
            {
                new Entry<Key, Value> (
                    new Key("00.00000/XX.2000.00.0"), 
                    new Value("Some Title", new List<string>(new string[]{"Ivan Morozov", "Stepan Holodov"}), "Journal name", 2000, 14)),
                new Entry<Key, Value> (
                    new Key("00.00050/XX.2000.00.0"), 
                    new Value("Some Title_1", new List<string>(new string[]{"Nikolay Kovalenko", "Stepan Holodov"}), "Journal", 2000, 2)),
                new Entry<Key, Value> (
                    new Key("00.00050/AX.2001.00.0"), 
                    new Value("Some 2Title", new List<string>(new string[]{"Nikolay Kovalenko", "Ivan Morozov"}), "Journal 1name", 2001, 14)),
                new Entry<Key, Value> (
                    new Key("00.00440/XX.3000.00.0"), 
                    new Value("Some Title", new List<string>(new string[]{"Bohdan Beliy", "Ivan Morozov"}), "Journal n2ame", 3000, 6)),
                new Entry<Key, Value> (
                    new Key("14.00050/XA.2000.06.0"), 
                    new Value("Some Title_1", new List<string>(new string[]{"Nikolay Kovalenko", "Bohdan Beliy"}), "Journal", 2000, 7)),
            };

    private static TaskHashtable _hashTable;

    private static void PrintHelpBox()
    {
        string text = @"
control - add control values;
add - add new casual element;
remove {key} - remove entry with key {key};
find {key} - find entry with key {key};
print - print hashtable;
hprint - print hindex hashtable;
hindex {authorName} - print {authorName} hindex;
exit - exit;
        ";
        Console.WriteLine(text);
    }

    public static void Procces()
    {
        _hashTable = new TaskHashtable();
        while (true)
        {
            try 
            {
                Console.WriteLine("Enter the command (help for the helpbox): ");
                string command = Console.ReadLine().Trim();
                
                if (command == "exit") return;
                else if (command == "help")
                {
                    PrintHelpBox();
                    continue;
                }
                else if (string.IsNullOrEmpty(command))
                {
                    Console.WriteLine("Command is empty.");
                    continue;
                }

                string[] args = null;

                if (command.StartsWith("hindex "))
                {
                    args = new string[] {"hindex", command.Substring("hindex ".Length)};
                }
                else 
                {
                    args = command.Split(' ');
                }

                ProcessCommand(args);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }
    }

    private static void ProcessCommand(string[] args)
    {
        if (args.Length == 1)
        {
            On1Argument(args);
        }
        else if (args.Length == 2)
        {
            On2Arguments(args);
        }
        else 
        {
            throw new ArgumentException("Invalid word combination. Check the helpbox.");
        }
    }

    private static void On2Arguments(string[] args)
    {
        string command = args[0];
        string value = args[1];

        if (command == "remove")
        {
            OnRemove(value);
        }
        else if (command == "find")
        {
            OnFind(value);
        }
        else if (command == "hindex")
        {
            OnHindex(value);
        }
        else 
        {
            throw new ArgumentException("Invalid word combination. Check the helpbox.");
        }
    }

    private static void OnRemove(string value)
    {
        bool isSucces = _hashTable.RemoveEntry(new Key(value));
        if (isSucces)
        {
            Console.WriteLine("Deleted!");
        }
        else
        {
            Console.WriteLine("Not found.");
        }
    }

    private static void OnFind(string value)
    {
        var entry = _hashTable.FindEntry(new Key(value));

        if (entry != null)
        {
            Console.WriteLine(entry);
        }
        else 
        {
            Console.WriteLine("Not found.");
        }
    }

    private static void OnHindex(string value)
    {
        Console.WriteLine($"An author {value} has index {_hashTable.AuthorHIndex(value)}.");
    }

    private static void On1Argument(string[] args)
    {
        string command = args[0];
        
        if (command == "add")
        {
            OnAdd();
        }
        else if (command == "control")
        {
            OnControl();
        }
        else if (command == "print")
        {
            OnPrint();
        }
        else if (command == "hprint")
        {
            OnHprint();
        }
        else 
        {
            throw new ArgumentException($"Invalid command \'{command}\'.");
        }
    }

    private static void OnAdd()
    {
        Console.WriteLine("Enter the title:");
        string title = Console.ReadLine();
        Console.WriteLine("Enter authors (empty line will stop process):");
        List<string> authors = new List<string>();
        
        while (true)
        {
            string author = Console.ReadLine().Trim();

            if (string.IsNullOrEmpty(author))
            {
                if (authors.Count == 0)
                {
                    Console.WriteLine("Enter at least one author!");
                    continue;
                }
                break;
            }

            bool isCorrect = CheckName(author);

            if (!isCorrect)
            {
                Console.WriteLine("Invalid name format!");
                continue;
            }

            authors.Add(author);
        }

        Console.WriteLine("Enter the journal name:");
        string journal = Console.ReadLine();
        Console.WriteLine("Enter year");
        int year = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter the count of citings:");
        int citings = int.Parse(Console.ReadLine());

        if (citings < 0)
        {
            throw new ArgumentException("The count of citins must be not less than 0.");
        }
        
        Key key = new Key(year);
        Value value = new Value(title, authors, journal, year, citings);

        _hashTable.InsertEntry(key, value);
        Console.WriteLine("Succesfull!\n");
    }

    private static void OnControl()
    {
        foreach (var obj in controlList)
        {
            _hashTable.InsertEntry(obj.key, obj.value);
        }
    }

    private static void OnPrint() => Console.WriteLine(_hashTable.MainTableToString());

    private static void OnHprint() => Console.WriteLine(_hashTable.HIndexTableToString());

    private static bool CheckName(string name)
    {
        foreach (char c in name)
        {
            if (!(char.IsLetter(c)  || c == ' '))
            {
                return false;
            }
        }
        return true;
    }
}