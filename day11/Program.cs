internal class Program
{
    private static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("input1.txt");

        var galaxyLocations = new List<(int, int)>();

        var populatedRows = new bool[lines.Length];
        var populatedColumns = new bool[lines[0].Length];

        for (var i = 0;i < lines.Length; i++)
        {
            for (var j = lines[i].IndexOf('#'); j > -1; j = lines[i].IndexOf('#', j + 1))
            {
                populatedRows[i] = true;
                populatedColumns[j] = true;

                galaxyLocations.Add((i, j));
            }
        }

        for (var i = populatedRows.Length - 1; i >= 0; i--)
        {
            if (!populatedRows[i])
            {
                for (var j = 0; j < galaxyLocations.Count; j++)
                {
                    var location = galaxyLocations[j];
                    if (location.Item1 > i)
                    {
                        galaxyLocations[j] = (location.Item1 + 1000000 - 1, location.Item2);
                    }
                }
            }
        }

        for (var i = populatedColumns.Length - 1; i >= 0; i--)
        {
            if (!populatedColumns[i])
            {
                for (var j = 0; j < galaxyLocations.Count; j++)
                {
                    var location = galaxyLocations[j];
                    if (location.Item2 > i)
                    {
                        galaxyLocations[j] = (location.Item1, location.Item2 + 1000000 - 1);
                    }
                }
            }
        }

        long sum = 0;

        for (var i = 0; i < galaxyLocations.Count; i++)
        {
            for (var j = i; j < galaxyLocations.Count; j++)
            {
                var location1 = galaxyLocations[i];
                var location2 = galaxyLocations[j];
                sum += Math.Abs(location1.Item1 - location2.Item1);
                sum += Math.Abs(location1.Item2 - location2.Item2);
            }
        }

        Console.WriteLine(sum);
    }

}
