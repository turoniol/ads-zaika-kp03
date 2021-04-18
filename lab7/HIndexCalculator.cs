using System.Collections.Generic;

namespace ASDLib
{
    static class HIndexCalculator
    {
        public static int CalculateHIndex(List<int> citings)
        {
            citings.Sort();

            for (int i = 0; i < citings.Count; ++i)
            {
                int j = citings.Count - 1 - i;
                if (citings[j] < i + 1)
                {
                    return i;
                }
            }

            return citings.Count;
        }
    }
}