internal class Program
{
    private static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("input1.txt");

        long[] times =  (from time in lines[0].Replace(" ", "").Split(':')
                        where !time.Contains("Time") && time is not ""
                        select long.Parse(time)).ToArray();

        long[] bestDistances =  (from distance in lines[1].Replace(" ", "").Split(':')
                            where !distance.Contains("Distance") && distance is not ""
                            select long.Parse(distance)).ToArray();

        long product = 1;

        for (var i = 0; i < times.Length; i++)
        {
            long firstWin = 0;
            for (var j = 0; j < times[i]; j++)
            {
                var distance = j * (times[i] - j);
                if (distance > bestDistances[i])
                {
                    firstWin = j;
                    break;
                }
            }

            long lastWin = 0;
            for (var j = times[i]; j > 0; j--)
            {
                var distance = j * (times[i] - j);
                if (distance > bestDistances[i])
                {
                    lastWin = j;
                    break;
                }
            }

            product *= lastWin - firstWin + 1;
        }

        Console.WriteLine(product);
    }
}