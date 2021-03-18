using System;
using MyGeneric;

class Program
{
    static void PrintDeque<T>(Deque<T> d)
    {
        Console.WriteLine(d);
    }

    static Deque<char> StringToDeque(string s)
    {
        Deque<char> d = new Deque<char>();
        foreach (char c in s)
        {
            d.InsertTail(c);
            PrintDeque<char>(d);
        }
        return d;
    }

    static bool CheckPalindrom(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return false;
        }

        var deque = StringToDeque(s);

        while (!deque.IsEmpty())
        {
            if (deque.Head.Data != deque.Tail.Data)
            {
                return false;
            }
            if (!deque.IsEmpty())
            {
                deque.RemoveHead();
                PrintDeque<char>(deque);
            }
            if (!deque.IsEmpty())
            {
                deque.RemoveTail();
                PrintDeque<char>(deque);
            }
        }
        return true;
    }

    static void OperateWithDeque<T>(Deque<T> d)
    {
        int end = d.Count / 2;
        Deque<T> temp = new Deque<T>();
        for (int i = 0; i < end; ++i)
        {
            temp.InsertTail(d.Tail.Data);
            d.RemoveTail();
            PrintDeque<T>(d);
        }
        Console.WriteLine("Half: ");
        PrintDeque<T>(temp);

        Console.WriteLine("Adding: ");
        while (!temp.IsEmpty())
        {
            d.InsertHead(temp.Tail.Data);
            temp.RemoveTail();
            PrintDeque<T>(d);
        }
    }

    static void Main(string[] args)
    {
        string example = "123examaxe321";
        string decision = "";

        while (true)
        {
            Console.WriteLine("Enter 1 for control example, 2 for custom");
            decision = Console.ReadLine();
            
            if (decision == "1" || decision == "2")
            {
                break;
            }
        }

        if (decision == "1")
        {
            Console.WriteLine(example);
            var pl = CheckPalindrom(example);
            Console.WriteLine($"Is Palindrom: {pl}");
            OperateWithDeque<char>(StringToDeque(example));
            return;
        }

        try 
        {
            while (true)
            {
                Console.WriteLine("Enter a string: ");
                string input = Console.ReadLine();

                bool isPalindrom = CheckPalindrom(input);
                Console.WriteLine($"Is Palindrom: {isPalindrom}");
                
                if (isPalindrom)
                {
                    OperateWithDeque<char>(StringToDeque(input));
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: {0}", ex.Message);
        }
    }
}

