using static System.Console;
using static System.Math;

class Program
{
    static void Main(string[] args)
    {
        WriteLine("Enter x:");
        double x = double.Parse(ReadLine());
        WriteLine("Enter y:");
        double y = double.Parse(ReadLine());
        WriteLine("Enter z:");
        double z = double.Parse(ReadLine());

        if (((x + z) != 0) && ((y - x) != 0))
        {
            double decimal_log = Log10(Abs(x + z));
            double natural_log = Log(Abs(y - x));

            if (natural_log != -2)
            {
                double a = 1 + decimal_log / (1 + natural_log / 2);
                double b = 1 / (Pow(a, 4) + 1);

                WriteLine($"a: {a}\nb: {b}");
            }
        }
        else
        {
            WriteLine("Invalid values.");
        }
    }
}
