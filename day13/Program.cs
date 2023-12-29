using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        string input = File.ReadAllText("input1.txt");

        string[] patterns = input.Split("\r\n\r\n");

        var sum = 0;

        foreach (string pattern in patterns)
        {
            string[] lines = pattern.Split("\r\n");
            int result;

            if (CheckSymmetry2(lines, out result))
            {
                sum += 100 * result;
            }
            else 
            {
                lines = TransposePattern(lines);
                CheckSymmetry2(lines, out result);
                sum += result;
            }
        }

        Console.WriteLine(sum);
    }

    private static string[] TransposePattern(string[] lines)
    {
        var transposedPattern = new string[lines[0].Length];

        for (var i = 0; i < lines[0].Length; i++)
        {
            var stringBuilder = new StringBuilder(lines.Length);
            for (var j = 0; j < lines.Length; j++)
            {
                stringBuilder.Append(lines[j][i]);
            }
            transposedPattern[i] = stringBuilder.ToString();
        }

        return transposedPattern;
    }

    private static bool CheckSymmetry(string[] lines, out int middleIndex)
    {
        for (var i = 0; i < lines.Length - 1; i++)
        {
            var indexUp = i;
            var indexDown = i + 1;

            var symmetric = true;
            while (indexUp >= 0 && indexDown < lines.Length)
            {
                if (!lines[indexUp].Equals(lines[indexDown]))
                {
                    symmetric = false;
                    break;
                }

                indexUp--;
                indexDown++;
            }
            if (symmetric)
            {
                middleIndex = i + 1;
                return true;
            }
        }

        middleIndex = -1;
        return false;
    }

    private static bool CheckSymmetry2(string[] lines, out int middleIndex)
    {
        for (var i = 0; i < lines.Length - 1; i++)
        {
            var indexUp = i;
            var indexDown = i + 1;

            var symmetric = true;
            var nonSymmetricLines = 0;
            while (indexUp >= 0 && indexDown < lines.Length)
            {
                var correctCount = lines[indexDown].Zip(lines[indexUp], (x, y) => x == y).Count(x => x);
                if (correctCount < lines[indexDown].Length)
                {
                    if (correctCount < lines[indexDown].Length - 1)
                    {
                        symmetric = false;
                        break;
                    } 
                    else if (nonSymmetricLines == 1)
                    {
                        symmetric = false;
                        break;
                    }
                    nonSymmetricLines = 1;
                }

                indexUp--;
                indexDown++;
            }
            if (symmetric && nonSymmetricLines == 1)
            {
                middleIndex = i + 1;
                return true;
            }
        }

        middleIndex = -1;
        return false;
    }
}