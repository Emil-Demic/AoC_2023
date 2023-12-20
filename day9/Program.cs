internal class Program
{
    private static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("input1.txt");

        var sum = 0;

        foreach (string line in lines)
        {
            // var v = NextValue(line, false);
            // Console.WriteLine(v);
            // sum += v;
            sum += NextValue(line, false);
        }

        Console.WriteLine(sum);
    }

    private static int NextValue(string line, bool last)
    {
        var lastValues = new List<int>();
        var firstValue = new List<int>();
        var history =  (from number in line.Split(' ')
                        select int.Parse(number)).ToArray();

        while (!CheckIfAllZero(history))
        {
            lastValues.Add(history[^1]);
            firstValue.Add(history[0]);

            var newHistory = new int[history.Length - 1];

            for (var i = 0; i < newHistory.Length; i++)
            {
                newHistory[i] = history[i + 1] - history[i];
            }

            history = newHistory;
        }

        if (last)
        {
            return lastValues.Sum();
        }

        var test = 0;

        firstValue.Reverse();

        foreach (int val in firstValue)
        {
            test = val - test;
        }

        return test;
        
        
    }

    private static bool CheckIfAllZero(int[] history)
    {
        foreach (int i in history)
        {
            if (i != 0)
                return false;
        }
        return true;
    }
}