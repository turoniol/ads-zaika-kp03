using System;
using System.Linq;

class Program
{
    const int WORD_LENGTH = 8;

    static string[] Sort(string[] array)
    {
        int len = array.Length;
        string[] sorted = new string[len];
        Array.Copy(array, sorted, len);

        for (int i = WORD_LENGTH - 1; i >= 0; --i)
        {
            string[] temp = new string[len];
            int[] chars = new int[len];
            Array.Copy(sorted, temp, len);

            for (int j = 0; j < len; ++j)
            {
                chars[j] = (int) sorted[j][i];
            }

            int[] counters = new int[chars.Max() + 1];
            
            for (int j = 0; j < len; ++j)
            {
                counters[chars[j]] += 1;
            }
            for (int j = 1; j < counters.Length; ++j)
            {
                counters[j] += counters[j - 1];
            }

            for (int j = len - 1; j >= 0; --j)
            {
                counters[chars[j]] -= 1;
                sorted[counters[chars[j]]] = temp[j];
            }
        }

        return sorted;
    }

    static bool CheckWord(string w)
    {
        bool firstLetter = char.IsLetter(w[0]);
        bool secondLetter = char.IsLetter(w[1]);
        bool thirdDigit = char.IsDigit(w[2]);
        bool fourthDigit = char.IsDigit(w[3]);
        bool fivethDigit = char.IsDigit(w[4]);
        bool sixthDigit = char.IsDigit(w[5]);
        bool seventhDigit = char.IsDigit(w[6]);
        bool eithdLetter = char.IsLetter(w[7]);

        return firstLetter && secondLetter && thirdDigit && fourthDigit && fivethDigit && sixthDigit && seventhDigit && eithdLetter;
    }

    static void Main(string[] args)
    {
        string[] controlArray = new string[]{"aA12345F","AA12346D","AA12346H","AB44526X",
            "AB34536G", "AC10000H","AC10000Y","AC10001H","XX00001X","XX00002X",
            "YY00000Y"};

        int control = 0;
        string[] sorted = new string[0];
        

        while (control != 1 && control != 2)
        {
            Console.WriteLine("Enter 1 for example, 2 for input.");
            int.TryParse(Console.ReadLine(), out control);
        }

        try
        {        
            if (control == 2)
            {
                Console.WriteLine("Enter a count of words: ");
                int count = 0;
                bool validCount = int.TryParse(Console.ReadLine(), out count);
                string[] words = new string[count];

                if (!validCount)
                {
                    throw new ArgumentException("The count must be a number.");
                }

                for (int i = 0; i < count; ++i)
                {
                    Console.WriteLine("Write a word. ({0})", i + 1);
                    string word = Console.ReadLine();

                    if (word.Length != 8)
                    {
                        Console.WriteLine("Invalid length of the word.");
                        i -= 1;
                        continue;
                    }
                    if (!CheckWord(word))
                    {
                        Console.WriteLine("Invalid word format. Must be XX00000X, X - letter, 0 - digit.");
                        i -= 1;
                        continue;
                    }

                    words[i] = word;
                }
                controlArray = words;
            }
        }
        catch (Exception ex)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ForegroundColor = color;
            Environment.Exit(-1);
        }
        // printing
        sorted = Sort(controlArray);
        Array.Reverse(sorted);

        Console.WriteLine("Source:");
        for (int i = 0; i < controlArray.Length; ++i)
        {
            if (String.Equals(controlArray[i], sorted[i]))
            {
                Console.WriteLine(controlArray[i]);
                continue;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(controlArray[i]);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        Console.WriteLine("Sorted:");
        for (int i = 0; i < sorted.Length; ++i)
        {
            if (String.Equals(controlArray[i], sorted[i]))
            {
                Console.WriteLine(sorted[i]);
                continue;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(sorted[i]);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
