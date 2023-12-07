class Program
{
    const int maxRed = 12;
    const int maxGreen = 13;
    const int maxBlue = 14;
    public static void Main()
    {
        string[] lines = File.ReadAllLines("input.txt");

        Solution sol = new(lines);
        sol.Run();

        // int sum = 0;

        // for (int i = 0; i < lines.Length; i++)
        // {
        //     bool valid = true;

        //     string line = lines[i];
        //     line = line[(line.IndexOf(':') + 2)..];

        //     while (line.Contains(';'))
        //     {
        //         string currentSet = line[..line.IndexOf(';')];
        //         line = line.Remove(0, currentSet.Length + 2);

        //         valid = SetValid(currentSet);
        //         if (!valid) 
        //             break;
        //     }

        //     if (valid && SetValid(line))
        //         sum += i + 1;
        // }

        // Console.WriteLine(sum);
        
    }

    private static bool SetValid(string set)
    {
        bool valid = true;

        string[] subsets = set.Split(',');

        foreach (string subset in subsets)
        {
            string trimmed = subset.TrimStart();

            int length = trimmed.Length - 1;
            int spaceIndex = trimmed.IndexOf(' ');

            int number = int.Parse(trimmed[..spaceIndex]);

            if (length - spaceIndex == 3) {
                if (number > maxRed) 
                    valid = false;
            } else if (length - spaceIndex == 4) {
                if (number > maxBlue)
                    valid = false;
            } else {
                if (number > maxGreen)
                    valid = false;
            }
        }

        return valid;
    }
}

class Solution(string[] lines)
{
    int minRed = 0;
    int minBlue = 0;
    int minGreen = 0;

    string[] lines = lines;

    public void Run()
    {
        int sum = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            minBlue = 0;
            minGreen = 0;
            minRed = 0;

            string line = lines[i];
            line = line[(line.IndexOf(':') + 2)..];

            while (line.Contains(';'))
            {
                string currentSet = line[..line.IndexOf(';')];
                line = line.Remove(0, currentSet.Length + 2);
                MinCubesForSet(currentSet);
            }
            MinCubesForSet(line);
            sum += minBlue * minGreen * minRed;
            
        }

        Console.WriteLine(sum);
    }

    private void MinCubesForSet(string set)
    {
        string[] subsets = set.Split(',');

        foreach (string subset in subsets)
        {
            string trimmed = subset.TrimStart();

            int length = trimmed.Length - 1;
            int spaceIndex = trimmed.IndexOf(' ');

            int number = int.Parse(trimmed[..spaceIndex]);

            if (length - spaceIndex == 3) {
                if (number > minRed) 
                    minRed = number;
            } else if (length - spaceIndex == 4) {
                if (number > minBlue)
                    minBlue = number;
            } else {
                if (number > minGreen)
                    minGreen = number;
            }
        }
    }
}