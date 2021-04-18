using System.Collections.Generic;

static class PNSearcher
{
    public static List<int> FindAll(int n)
    {
        
        List<int> list = new List<int>();

        for (int i = 3; i < n; i += 2)
        {
            if (IsPrime(i))
            {
                list.Add(i);
            }
        }

        return list;
    }

    public static bool IsPrime(int n)
    {
        for (int i = 2; i < n; ++i)
        {
            if (n % i == 0)
            {
                return false;
            }
        }
        return true;
    }
}