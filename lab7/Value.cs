using System.Collections.Generic;

namespace ASDLib
{
    public class Value
    {
        public string Title {get; private set;}
        public List<string> Authors {get; private set;}
        public string JournalName {get; private set;}
        public int YearOfPublishing {get; private set;}
        public int NumberOfCiting {get; private set;}

        public Value(string title, List<string> authors, string journalName, int yearOfPublishing, int numberOfCiting)
        {
            Title = title;
            Authors = authors;
            JournalName = journalName;
            YearOfPublishing = yearOfPublishing;
            NumberOfCiting = numberOfCiting;
        }

        public override string ToString()
        {
            string result = $"\'{Title}\'" + ", ";
            foreach (var obj in Authors)
            {
                result += $" {obj}";
            }
            result += $"; \'{JournalName}\', {YearOfPublishing}, {NumberOfCiting}";
            return result;
        }
    }
}