internal class Program
{
    private static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("input1.txt");

        var grid = new char[lines.Length][];

        var startLocation = (0, 0); 
        for (var i = 0; i < lines.Length; i++)
        {
            if (lines[i].Contains('S'))
            {
                startLocation = (i, lines[i].IndexOf('S'));
            }
            grid[i] = lines[i].ToCharArray();
        }

        var currentLocation = startLocation;
        currentLocation = (currentLocation.Item1 + 1, currentLocation.Item2);
        var prevLocation = startLocation;

        var counter = 1;

        var path = new List<(int, int)>
        {
            startLocation
        };

        while (grid[currentLocation.Item1][currentLocation.Item2] != 'S')
        {
            path.Add(currentLocation);
            switch (grid[currentLocation.Item1][currentLocation.Item2])
            {
                case '-':
                    if (prevLocation.Item2 == currentLocation.Item2 - 1)
                    {
                        prevLocation = currentLocation;
                        currentLocation = (currentLocation.Item1, currentLocation.Item2 + 1);
                    }
                    else
                    {
                        prevLocation = currentLocation;
                        currentLocation = (currentLocation.Item1, currentLocation.Item2 - 1);
                    }
                    break;
                case '|':
                    if (prevLocation.Item1 == currentLocation.Item1 - 1)
                    {
                        prevLocation = currentLocation;
                        currentLocation = (currentLocation.Item1 + 1, currentLocation.Item2);
                    }
                    else
                    {
                        prevLocation = currentLocation;
                        currentLocation = (currentLocation.Item1 - 1, currentLocation.Item2);
                    }
                    break;
                case 'L':
                    if (prevLocation.Item1 == currentLocation.Item1 - 1)
                    {
                        prevLocation = currentLocation;
                        currentLocation = (currentLocation.Item1, currentLocation.Item2 + 1);
                    }
                    else
                    {
                        prevLocation = currentLocation;
                        currentLocation = (currentLocation.Item1 - 1, currentLocation.Item2);
                    }
                    break;
                case 'J':
                    if (prevLocation.Item2 == currentLocation.Item2 - 1)
                    {
                        prevLocation = currentLocation;
                        currentLocation = (currentLocation.Item1 - 1, currentLocation.Item2);
                    }
                    else
                    {
                        prevLocation = currentLocation;
                        currentLocation = (currentLocation.Item1, currentLocation.Item2 - 1);
                    }
                    break;
                case '7':
                    if (prevLocation.Item2 == currentLocation.Item2 - 1)
                    {
                        prevLocation = currentLocation;
                        currentLocation = (currentLocation.Item1 + 1, currentLocation.Item2);
                    }
                    else
                    {
                        prevLocation = currentLocation;
                        currentLocation = (currentLocation.Item1, currentLocation.Item2 - 1);
                    }
                    break;
                case 'F':
                    if (prevLocation.Item2 == currentLocation.Item2 + 1)
                    {
                        prevLocation = currentLocation;
                        currentLocation = (currentLocation.Item1 + 1, currentLocation.Item2);
                    }
                    else
                    {
                        prevLocation = currentLocation;
                        currentLocation = (currentLocation.Item1, currentLocation.Item2 + 1);
                    }
                    break;

            }
            counter++;
        }

        var area = 0;
        for (var i = 0; i < path.Count - 1; i++)
        {
            area += (path[i].Item2 + path[i + 1].Item2) * (path[i].Item1 - path[i + 1].Item1);
        }
        
        area += (path[^1].Item2 + path[0].Item2) * (path[^1].Item1 - path[0].Item1);

        area = (int) Math.Ceiling(Math.Abs(area) / 2.0);

        var answer = area - (counter / 2 - 1);

        Console.WriteLine(area);
        Console.WriteLine(counter);
        Console.WriteLine(answer);

        Console.WriteLine();

        Console.WriteLine(counter / 2);

    }
}