using System;
using System.IO;

class Program
{
    public static void Main()
    {
        string[] lines = File.ReadAllLines("input.txt");

        char[] digits = ['1', '2', '3', '4', '5', '6', '7', '8', '9'];
        string[] words = ["one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];

        int sum = 0;

        foreach (string line in lines)
        {
            int firstDigitLocation = line.IndexOfAny(digits);
            int lastDigitLocation = line.LastIndexOfAny(digits);

            int firstDigit = 0;
            if (firstDigitLocation != -1)
            {
                firstDigit = (int)Char.GetNumericValue(line[firstDigitLocation]);
            } else {
                firstDigitLocation = line.Length;
            }
             
            int lastDigit = 0;
            if (lastDigitLocation != -1)
            {
                lastDigit = (int)Char.GetNumericValue(line[lastDigitLocation]);
            }

            for (int i = 0; i < 9; i ++)
            {
                int digitLocation = line.IndexOf(words[i]);
                if (digitLocation != -1 && digitLocation < firstDigitLocation)
                {
                    firstDigit = i + 1;
                    firstDigitLocation = digitLocation;

                }

                digitLocation = line.LastIndexOf(words[i]);
                if (digitLocation > lastDigitLocation)
                {
                    lastDigit = i + 1;
                    lastDigitLocation = digitLocation;
                }
            }
            // Console.WriteLine(firstDigit * 10 + lastDigit);
            sum += firstDigit * 10 + lastDigit;

        }

        Console.WriteLine(sum);
    }
}