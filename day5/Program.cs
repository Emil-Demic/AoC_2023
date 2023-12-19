internal class Program
{
    private static void Main()
    {
        string[] parts = File.ReadAllText("input1.txt").Split("\r\n\r\n");

        long[] seeds = (from number in parts[0].Split(' ')
                       where !number.Contains("seeds")
                       select long.Parse(number)).ToArray();

        // foreach (var seed in seeds)
        // {
        //     Console.WriteLine(seed);
        // }

        var maps = new Map[parts.Length - 1];
        for (var i = 1; i < parts.Length; i++)
        {
            maps[i - 1] = new Map(parts[i]);
        }

        // for (var i = 0; i < seeds.Length; i++)
        // {
        //     foreach (Map map in maps)
        //     {
        //         seeds[i] = map.Remap(seeds[i]);
        //     }
        // }

        // Console.WriteLine(seeds.Min());

        Stack<(long, long)> results = [];

        for (var i = 0; i < seeds.Length; i += 2)
        {
            Stack<(long, long)> ranges = [];
            ranges.Push((seeds[i], seeds[i] + seeds[i + 1]));
            foreach (Map map in maps)
            {
                ranges = map.RemapRange(ranges);
            }

            results.Push(ranges.Min());
        }

        Console.WriteLine(results.Min());


    }
}


class Map
{
    private readonly long[][] _mapValues;
    public Map(string s)
    {
        string[] lines = s.Split('\n')[1..];
        _mapValues = new long[lines.Length][];
        for (var i = 0; i < lines.Length; i++)
        {
            _mapValues[i] = (from number in lines[i].Split(' ')
                            select long.Parse(number)).ToArray();
        }
    }

    public long Remap(long value)
    {
        for (var i = 0; i < _mapValues.Length; i++)
        {
            if (value >= _mapValues[i][1] && value < _mapValues[i][1] + _mapValues[i][2])
            {
                return value + (_mapValues[i][0] - _mapValues[i][1]);
            }
        }
        return value;
    }

    public Stack<(long, long)> RemapRange(Stack<(long, long)> ranges)
    {
        var A = new Stack<(long, long)>();

        for (var i = 0; i < _mapValues.Length; i++)
        {
            long destination = _mapValues[i][0];
            long source = _mapValues[i][1];
            long mapLength = _mapValues[i][2];
            long sourceEnd = source + mapLength;
            var newRanges = new Stack<(long, long)>();
            
            while (ranges.Count != 0)
            {
                var (start, end) = ranges.Pop();

                (long, long) before = (start, Math.Min(source, end));
                (long, long) inter = (Math.Max(start, source), Math.Min(sourceEnd, end));
                (long, long) after = (Math.Max(start, sourceEnd), end);

                if (before.Item2 > before.Item1)
                {
                    newRanges.Push(before);
                }
                if (inter.Item2 > inter.Item1)
                {
                    A.Push((inter.Item1 - source + destination, inter.Item2 - source + destination));
                }
                if (after.Item2 > after.Item1)
                {
                    newRanges.Push(after);
                }
            }

            ranges = newRanges;
        }

        return new Stack<(long, long)> (A.Concat(ranges));
    }
}