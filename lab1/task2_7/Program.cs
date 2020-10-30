using System;
using static System.Console;
class Program
{
    static bool isLeapYear(int y)
    {
        if (y % 4 == 0)
        {
            if (y % 100 == 0)
                if (y % 400 == 0)
                    return true;
                else 
                    return false;
            else 
                return true;
        }
        return false;
    }
    static bool have31(int m) => ((m > 7) ? (m % 2 == 0) : (m % 2 == 1));
    static bool dExist(int d, int m, int y)
    {
        if (d >= 1)
        {
            if (m == 2 && isLeapYear(y))
            {
                return d <= 29;
            }
            else if (have31(m))
            {
                return d <= 31;
            }
            else if (!have31(m))
            {
                return d <= 30;
            }
            else
            {
                return d <= 28;
            }
        }
        return false;
    }
    static void Main(string[] args)
    {
        int d1, d2, m1, m2, y1, y2;
        int days = 0, years = 0;
        WriteLine("Enter d1:");
        d1 = int.Parse(ReadLine());
        WriteLine("Enter m1:");
        m1 = int.Parse(ReadLine());
        WriteLine("Enter y1:");
        y1 = int.Parse(ReadLine());
        WriteLine("Enter d2:");
        d2 = int.Parse(ReadLine());
        WriteLine("Enter m2:");
        m2 = int.Parse(ReadLine());
        WriteLine("Enter y2:");
        y2 = int.Parse(ReadLine());

        // сравнение
        if (y2 < y1)
        {
            int y = y2;
            y2 = y1;
            y1 = y;
            int m = m2;
            m2 = m1;
            m1 = m;
            int d = d2;
            d2 = d1;
            d1 = d;
        }
        else if (y2 == y1)
        {
            if (m2 < m1)
            {
                int m = m2;
                m2 = m1;
                m1 = m;
                int d = d2;
                d2 = d1;
                d1 = d;
            }
            else if (m2 == m1)
            {
                if (d2 < d1)
                {
                    int d = d2;
                    d2 = d1;
                    d1 = d;
                }
            }
        }

        bool y1Exist = (y1 >= 1900 &&  y1 <= 2020);
        bool y2Exist = (y2 >= 1900 &&  y2 <= 2020);
        bool m1Exist = (m1 >= 1  && m1 <= 12);
        bool m2Exist = (m2 >= 1  && m2 <= 12);

        if (y1Exist && y2Exist && m1Exist && m2Exist && dExist(d1, m1, y1) && dExist(d2, m2, y2))
        {
            // если первая дата позднее первой, не учитывая год, то разница между годами на 1 меньше
            years = ((m1 > m2) || ((m2 == m1) && (d1 > d2))) ? y2 - y1 - 1 : y2 - y1;
            // переменная для перехода в следующий год, если он там есть
            int y = y1;
            for (int m = m1; (m != m2 || y != y2); m++)
            {
                if (m == 2)
                {
                    days += (isLeapYear(y) ? 29 : 28);
                }
                else
                    days +=  have31(m) ? 31 : 30;

                if (m == 12)
                {
                    y++;
                    m = 0;
                }
            }
            // вычесть лишние дни первого месяца и добавить недостающие первые дни последнего месяца
            days += d2;
            days -= d1;

            WriteLine("Years: {0}\nDays: {1}", years, days);
        }
        else
        {
            WriteLine("Invalid values.");
        }
    }
}
