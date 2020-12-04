using System;

class Program
{
    static bool TryParse(out int a) => int.TryParse(Console.ReadLine(), out a);
    static void PrintMatrix(int[,] m, int k)
    {
        for (int i = 0; i < m.GetLength(0); ++i)
        {
            for (int j = 0; j < m.GetLength(1); ++j)
            {
                if (k != 0 && (k * j) % 2 == 0)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                }
                Console.Write("{0}", m[i, j]);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(" ");
            }
            Console.WriteLine();
        }
    }
    static void FillMatrix(int[,] matrix)
    {
        int length = matrix.Length;
        int degree = (int) Math.Log10(length);
        int min = (int) Math.Pow(10, degree);
        if (length / min == 9)
        {
            min *= 10;
        }
        int max = min * 10;
        int[] generatedValues = new int[max - min];
        Random rand = new Random();

        for (int i = 0; i < matrix.GetLength(0); ++i)
        {
            for (int j = 0; j < matrix.GetLength(1); ++j)
            {
                int val = -1;
                do 
                {
                    val = rand.Next(min, max);
                } while (generatedValues[val - min] != 0);

                generatedValues[val - min] = val;
                matrix[i, j] = val;
            }
        }
    }
    static void Swap(ref int a, ref int b)
    {
        int t = a;
        a = b;
        b = t;
    }
    static void SortArray(int[] a)
    {
        int length = a.Length;
        bool sorted = false;

        while (!sorted)
        {
            sorted = true;
            for (int j = length - 2; j > 0; j -= 2)
            {
                if (a[j] < a[j - 1])
                {
                    Swap(ref a[j], ref a[j - 1]);
                    sorted = false;
                }
            }
            for (int j = length - 1; j > 0; j -= 2)
            {
                if (a[j] < a[j - 1])
                {
                    Swap(ref a[j], ref a[j - 1]);
                    sorted = false;
                }
            }
        }
    }
    static void SortMatrix(int[,] m, int k)
    {
        int height = m.GetLength(0);
        int width = m.GetLength(1);
        int counter = 0;
        for (int x = 0; x < width; ++x)
        {
            if ((x * k) % 2 == 0)
            {
                counter += 1;
            }
        }
        for (int i = 0; i < height; ++i)
        {
            int iterator = 0;
            int[] values = new int[counter];
            int[] indexes = new int [counter];
            for (int j = 0; j < width; ++j)
            {
                if ((j * k) % 2 == 0)
                {
                    values[iterator] = m[i, j];
                    indexes[iterator] = j;
                    iterator += 1;
                }
            }
            SortArray(values);
            for (int x = 0; x < values.Length; ++x)
            {
                m[i, indexes[x]] = values[x];
            }
        }
    }
    static void Main(string[] args)
    {
        Console.Clear();
        int m = 0, n = 0, k = 0;
        Console.WriteLine("Enter height:");
        bool mExist = (TryParse(out m) && m > 0);
        Console.WriteLine("Enter width:");
        bool nExist = (TryParse(out n) && n > 0);
        Console.WriteLine("Enter k:");
        bool kExist = (TryParse(out k) && k > 0);
        // Console.WriteLine("size: {0}", m);
        if (mExist && nExist && kExist)
        {
            // System.Diagnostics.Stopwatch myStopwatch = new System.Diagnostics.Stopwatch();
            // myStopwatch.Start();
            int[,] arr = new int[m, n];
            FillMatrix(arr);
            // myStopwatch.Stop();
            // Console.WriteLine("{0} s {1} ms", myStopwatch.Elapsed.Seconds, myStopwatch.Elapsed.Milliseconds % 1000);
            Console.WriteLine("Source matrix: ");
            PrintMatrix(arr, k);
            SortMatrix(arr, k);
            Console.WriteLine("Sorted matrix: ");
            PrintMatrix(arr, k);
        }
        else
        {
            if (!mExist)
            {
                Console.WriteLine("Invalid height.");
            }
            if (!nExist)
            {
                Console.WriteLine("Invalid width.");
            }
            if (!kExist)
            {
                Console.WriteLine("Invalid k.");
            }
        }
    }
}
