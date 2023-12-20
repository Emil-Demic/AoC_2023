internal class Program
{
    private static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("input1.txt");

        string instructions = lines[0];

        var directions = new Dictionary<string, (string, string)>();

        for (var i = 2; i < lines.Length; i++)
        {
            var line = lines[i];
            var key = line[..3];
            if (key[^1] == 'A')
                Console.WriteLine(key);
            line = line[7..^1];
            var left = line.Split(", ")[0];
            var right = line.Split(", ")[1];
            directions.Add(key, (left, right));
        }

        var currentKey = "FXA";
        var steps = 0;

        while (true)
        {
            foreach (char c in instructions)
            {
                steps++;
                var (left, right) = directions[currentKey];
                if (c == 'L')
                    currentKey = left;
                else
                    currentKey = right;
                
                if (currentKey[^1] == 'Z')
                    break;

            }
            if (currentKey[^1] == 'Z')
                    break;
        }

        Console.WriteLine(steps);
    }
}