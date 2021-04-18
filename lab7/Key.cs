using System;

namespace ASDLib
{
    public class Key
    {
        private string _doi;

        public Key(int year)
        {
            if (!(year >= 1000 && year <= 9999)) throw new ArgumentException("The year must be from 1000 to 9999");

            Random rand = new Random();

            string firstNumber = string.Empty;
            for (int i = 0; i < 2; ++i)
            {
                firstNumber += (char) (rand.Next((int)'0', (int)'9' + 1));
            }

            string secondNumber = string.Empty;
            for (int i = 0; i < 5; ++i)
            {
                secondNumber += (char) (rand.Next((int)'0', (int)'9' + 1));
            }

            string thirdWord = string.Empty;
            for (int i = 0; i < 2; ++i)
            {
                thirdWord += (char) rand.Next((int)'A', (int)'Z' + 1);
            }

            string fourthNumber = string.Empty;
            for (int i = 0; i < 2; ++i)
            {
                fourthNumber += (char) (rand.Next((int)'0', (int)'9' + 1));
            }

            string fivethNumber = ((char) (rand.Next((int)'0', (int)'9' + 1))).ToString();

            _doi = $"{firstNumber}.{secondNumber}/{thirdWord}.{year}.{fourthNumber}.{fivethNumber}";
        }

        public Key(string str)
        {
            _doi = str;
        }

        public string GetValue() => _doi;

        public override string ToString() => _doi;
    }
}