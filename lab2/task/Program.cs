using static System.Console;
using System;
struct Element
{
    public int i;
    public int j;
    public int value;
}
class Program
{
    static void PathStep(int[] path, ref Element max, ref Element min, int i, int j, ref int counter, int val)
    {
        path[counter++] = val;
        if (val > max.value)
        {
            max.i = i;
            max.j = j;
            max.value = val;
        }
        if (min.value > val)
        {
            min.i = i;
            min.j = j;
            min.value = val;
        }
    }
    static void PrintMatrix(in int[,] m, int size)
    {
        WriteLine("Matrix:");
        for (int j = 0; j < size; ++j)
        {
            for (int i = 0; i < size; ++i)
            {
                Write("{0} ", m[i, j]);
            }
            Write("\n");
        }
    }

    static void Main(string[] args)
    {
        WriteLine("Enter n:");
        int n = int.Parse(ReadLine());
        int decision = 0;

        if (n > 0)
        {
            int[,] matrix = new int[n, n];

            Random rand = new Random();

            while (decision != 1 && decision != 2)
            {
                WriteLine("Enter the number 1 (control matrix) or 2 (random matrix)");
                decision = int.Parse(ReadLine());
            }
            // filling
            int number = 0;
            for (int j = 0; j < n; ++j)
            {
                for (int i = 0; i < n; ++i)
                {
                    if (decision == 1)
                    {
                        matrix[i, j] = number;
                        number++;
                    }
                    else
                    {
                        matrix[i, j] = rand.Next(10, 100);
                    }
                }
            }
            
            int[] path = new int[n * n];

            Element uMax = new Element {i = 0, j = 0, value = 0};
            Element dMax = new Element {i = 0, j = 0, value = 0};
            Element oMax = new Element {i = 0, j = 0, value = 0};
            Element uMin = new Element {i = 0, j = 0, value = 100};
            Element dMin = new Element {i = 0, j = 0, value = 100};
            Element oMin = new Element {i = 0, j = 0, value = 100};

            int counter = 0;
            // under diagonal
            int ui = n - 1, uj = 0;
            while (ui + uj > 0)
            {
                uj = 0;
                while (uj < ui)
                {
                    PathStep(path, ref uMax, ref uMin, ui, uj, ref counter, matrix[uj, ui]);
                    uj++;
                }
                ui--;
                uj = ui - 1;
                while (uj >= 0)
                {
                    PathStep(path, ref uMax, ref uMin, ui, uj, ref counter, matrix[uj, ui]);
                    uj--;
                }
                ui--;
            }
            // diagonal
            for (int i = 0; i < n; ++i)
            {
                PathStep(path, ref dMax, ref dMin, i, i, ref counter, matrix[i, i]);
            }
            // over diagonal
            int oi = n - 2, oj = n - 1; 
            while (oi + oj > 0)
            {
                oi = oj - 1;
                while (oi >= 0)
                {
                    PathStep(path, ref oMax, ref oMin, oi, oj, ref counter, matrix[oj, oi]);
                    oi--;
                }
                oj--;
                oi = 0;
                while (oj > oi)
                {
                    PathStep(path, ref oMax, ref oMin, oi, oj, ref counter, matrix[oj, oi]);
                    oi++;
                }
                oj--;
            }
            PrintMatrix(matrix, n);
            WriteLine("Path: ");
            foreach (int val in path)
            {
                Write("{0} ", val);
            }
            Write("\n");
            WriteLine($"Under diagonal:\n max, i = {uMax.i}, j = {uMax.j}\n min, i = {uMin.i}, j = {uMin.j}");
            WriteLine($"Diagonal:\n max, i = {dMax.i}, j = {dMax.j}\n min, i = {dMin.i}, j = {dMin.j}");
            WriteLine($"Over diagonal:\n max, i = {oMax.i}, j = {oMax.j}\n min, i = {oMin.i}, j = {oMin.j}");
        }
        else if (n <= 0)
        {
            WriteLine("N must be must be a natural number");
        }
        Read();
    }
}
