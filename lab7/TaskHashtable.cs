using System.Collections.Generic;

namespace ASDLib
{
    public class TaskHashtable
    {
        private Hashtable<Key, Value> _mainHashTable;
        private Hashtable<string, int> _hIndexHashTable;
        private Hashtable<string, List<int>> _citingsHashTable;

        public TaskHashtable()
        {
            _mainHashTable = new Hashtable<Key, Value>();
            _hIndexHashTable = new Hashtable<string, int>();
            _citingsHashTable = new Hashtable<string, List<int>>();
        }
    
        private Entry<T, V> FindEntryFromHS<T, V>(Hashtable<T, V> hs, T key)
        {
            Entry<T, V> entry = hs.FindEntry(key);

            if (entry == null) throw new System.ArgumentException($"Not found entry with key \'{key}\'");

            return entry;
        }

        public void InsertEntry(Key key, Value value)
        {
            List<string> authors= value.Authors;
            
            for (int i = 0; i < authors.Count; ++i)
            {
                var entry = _citingsHashTable.FindEntry(authors[i]);
                List<int> entryList;

                if (entry == null)
                {
                    entryList = new List<int>();
                }
                else
                {
                    entryList = entry.value;
                }

                entryList.Add(value.NumberOfCiting);
                _citingsHashTable.InsertEntry(authors[i], entryList);
                CalculateHIndex(authors[i]);
            }
            
            _mainHashTable.InsertEntry(key, value);
        }

        public bool RemoveEntry(Key key)
        {
            var entry = FindEntryFromHS(_mainHashTable, key);
            Value entryValue = entry.value;
            List<string> authors = entryValue.Authors;

            for (int i = 0; i < authors.Count; ++i)
            {
                List<int> entryList = FindEntryFromHS(_citingsHashTable, authors[i]).value;
                entryList.Remove(entryValue.NumberOfCiting);

                if (entryList.Count == 0)
                {
                    _citingsHashTable.RemoveEntry(authors[i]);
                    _hIndexHashTable.RemoveEntry(authors[i]);
                }
                else 
                {
                    _citingsHashTable.InsertEntry(authors[i], entryList);
                    CalculateHIndex(authors[i]);
                }
            }

            return _mainHashTable.RemoveEntry(key);   
        }

        public Entry<Key, Value> FindEntry(Key key) => _mainHashTable.FindEntry(key);

        public string MainTableToString() => _mainHashTable.ToString();

        public string HIndexTableToString() => _hIndexHashTable.ToString();

        public int AuthorHIndex(string authorName)
        {
            var entry = FindEntryFromHS(_hIndexHashTable, authorName);

            return entry.value;
        }

        private void CalculateHIndex(string authorName)
        {
            int index = HIndexCalculator.CalculateHIndex(_citingsHashTable.FindEntry(authorName).value);
            _hIndexHashTable.InsertEntry(authorName, index);
        }
    }
}