using System;
using List;
using static Printer;

class Program
{
    static void PrintHelpBox()
    {
        Printer.Print("-----------------------------------------------------------");

        Printer.Print("Print {command} to {action}");
        Printer.Print("quit - quit");
        Printer.Print("addf data - add first element to list with data(int).");
        Printer.Print("addl data - add last element to list with data(int).");
        Printer.Print("add data pos - add element at pos(int) to list with data(int).");
        Printer.Print("delf - delete first element of list.");
        Printer.Print("dell - delete last element of list.");
        Printer.Print("del pos - delete element of list at pos(int).");
        Printer.Print("part1 data - add element after middle element with data(int).");
        Printer.Print("part2 data - delete some elements (their value is less than the value of mibble) and add new head with data(int)");

        Printer.Print("-----------------------------------------------------------");
    }
    static bool ParseCommand(string command, DLList list)
    {
        if (list == null)
        {
            Printer.PrintError("List doesn`t exist.");
            Environment.Exit(1);
        }
        if (command == "quit") return true;

        command = command.Trim();
        string[] parsedCommand = command.Split(" ");
        string keyWord = parsedCommand[0];
        if (keyWord == "addf" && parsedCommand.Length == 2)
        {
            int data;
            bool correctData = int.TryParse(parsedCommand[1], out data);
            if (correctData)
            {
                list.AddFirst(data);
            }
            else 
            {
                Printer.PrintError("Invalid data.");
            }
        }
        else if (keyWord == "addl" && parsedCommand.Length == 2)
        {
            int data;
            bool correctData = int.TryParse(parsedCommand[1], out data);
            if (correctData)
            {
                list.AddLast(data);
            }
            else 
            {
                Printer.PrintError("Invalid data.");
            }
        }
        else if (keyWord == "add" && parsedCommand.Length == 3)
        {
            int data, pos;
            bool correctData = int.TryParse(parsedCommand[1], out data);
            bool correctPos = int.TryParse(parsedCommand[2], out pos);
            if (correctData && correctPos)
            {
                list.AddAtPosition(data, pos);
                list.Print();
            }
            else 
            {
                if (!correctData) Printer.PrintError("Invalid data.");
                if (!correctPos) Printer.PrintError("Invalid position.");
            }
        }
        else if (keyWord == "delf" && parsedCommand.Length == 1)
        {
            list.DeleteFirst();
        }
        else if (keyWord == "dell" && parsedCommand.Length == 1)
        {
            list.DeleteLast();
        }
        else if (keyWord == "del" && parsedCommand.Length == 2)
        {
            int pos;
            bool correctPos = int.TryParse(parsedCommand[1], out pos);
            if (correctPos)
            {
                list.DeleteAtPos(pos);
            } 
            else
            {
                Printer.PrintError("Invalid position.");
            }
        }
        else if (keyWord == "part1" && parsedCommand.Length == 2)
        {
            int data = 0;
            bool dataExist = int.TryParse(parsedCommand[1], out data);
            if (!dataExist)
            {
                Printer.PrintError("Invalid data.");
                return false;
            }
            list.AddAfterMid(data);
        }
        else if (keyWord == "part2" && parsedCommand.Length == 2)
        {
            SLList slist = null;
            int mid = (list.size % 2 == 0) ? list.size / 2 : list.size / 2 + 1;
            var midData = list.GetDataAtPos(mid);
            
            slist = list.DeleteNodesWithData(midData);
            Printer.Print($"Mid value: {midData}");
            slist.Print();
        }
        else if (keyWord == "help" && parsedCommand.Length == 1)
        {
            PrintHelpBox();
        }
        else 
        {
            Printer.PrintError($"Unknown command \"{command}\"");
        }
        return false;
    }
    static void Main(string[] args)
    {
        Console.Clear();
        string command;
        DLList list = new DLList();

        while (true)
        {
            Printer.Print("Print command (\"help\" for help):");
            command = Console.ReadLine();
            bool exit = ParseCommand(command, list);
            if (exit) break;
        }
    }
}

