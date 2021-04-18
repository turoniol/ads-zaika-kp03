using System;

namespace ASDLib
{
    public class Hashtable<TKey, TValue>
    {
        private Entry<TKey, TValue>[] table;
        public double loadness
        {
            get => (double) size / table.Length;
        }
        public int size
        {
            get;
            private set;
        }

        public Hashtable()
        {
            table = new Entry<TKey, TValue>[17];
        }

        public void InsertEntry(TKey key, TValue value)
        {
            if (loadness > 0.5f)
            {
                Console.WriteLine($"Loadness is {loadness}. It's time to rehash it!");
                Rehashing();
            }

            int index = GetIndex(key);

            if (table[index] == null)
            {
                size += 1;
            }

            table[index] = new Entry<TKey, TValue>(key, value);
        }

        public bool RemoveEntry(TKey key)
        {
            int index = GetIndex(key);

            if (table[index] == null)
            {
                return false;
            }

            table[index] = null;
            size -= 1;

            return true;
        }

        public Entry<TKey, TValue> FindEntry(TKey key)
        {
            int index = GetIndex(key);

            return table[index];
        }

        public void Rehashing()
        {
            size = 0;
            
            int len = table.Length;
            Entry<TKey, TValue>[] temp = new Entry<TKey, TValue>[len];
            Array.Copy(table, temp, len);

            var list = PNSearcher.FindAll(2 * len);
            table = new Entry<TKey, TValue>[list[list.Count - 1]];

            foreach (var entry in temp)
            {
                if (entry == null) continue;
                InsertEntry(entry.key, entry.value);
            }
        }
    
        public int HashCode(TKey key)
        {
            int value = 0;
            foreach(char c in key.ToString())
            {
                if (char.IsLetter(c))
                {
                    value += (int) (c - 'A');
                }
                else if (char.IsDigit(c))
                {
                    value += (int) (c - '0');
                }
            }

            return value;
        }

        public int GetHash(TKey key)
        {
            return HashCode(key) % table.Length;
        }

        public override string ToString()
        {
            string result = "{\n";
            int counter = 0;

            for (int i = 0; i < table.Length; ++i)
            {
                var obj = table[i];
                if (obj != null)
                {
                    counter += 1;
                    result += $"[{counter},{i}]. {obj}; \n";
                }
            }

            result += "}";
            return result;
        }

        private int GetIndex(TKey key)
        {
            int value = GetHash(key);
            int i = 0;
            int index =  value;
            while (table[index] != null && !IsEqual(table[index].key, key))
            {
                i += 1;
                index = (value + Math.Abs(i * i)) % table.Length;
            }

            return index;
        }

        static private bool IsEqual(TKey left, TKey right)
        {
            string sLeft = left.ToString();
            string sRight = right.ToString();

            if (sLeft.Length != sRight.Length)
            {
                return false;
            }

            for (int i = 0; i < sLeft.Length; ++i)
            {
                if (sLeft[i] != sRight[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}