using Microsoft.VisualBasic;

string[] lines = File.ReadAllLines("input.txt");

var schematic = CreateSchematic(lines);

int sum = 0;

// for (var i = 0; i < schematic.Length; i++)
// {
//     for (var j = 0; j < schematic[0].Length; j++)
//     {
//         if (!char.IsDigit(schematic[i][j])) 
//             continue;

//         int start = j;
//         int end = start + 1;
//         while (end < schematic[0].Length && char.IsDigit(schematic[i][end]))
//         {
//             end++;
//         }

//         bool enginePart = CheckIfEnginePart(i, j, start, end, schematic);

//         bool test = int.TryParse(new String(schematic[i][start..end]), out int number);
//         if (test && enginePart)
//             sum += number;

//         j += end - start;
//     }
// }

var counts = new Dictionary<(int, int), List<int>>();

for (var i = 0; i < schematic.Length; i++)
{
    for (var j = 0; j < schematic[0].Length; j++)
    {
        if (!char.IsDigit(schematic[i][j])) 
            continue;

        int start = j;
        int end = start + 1;
        while (end < schematic[0].Length && char.IsDigit(schematic[i][end]))
        {
            end++;
        }

        bool gear = CheckIfGear(i, j, start, end, schematic, out var location);

        bool test = int.TryParse(new String(schematic[i][start..end]), out int number);
        if (test && gear)
            if (counts.TryGetValue(location, out var numbers))
            {
                numbers.Add(number);
            } 
            else
            {
                counts.Add(location, [number]);
            }

        j += end - start;
    }
}

foreach (var (location, numbers) in counts)
{
    // Console.WriteLine(location);
    // foreach (int number in numbers)
    //     Console.WriteLine(number);
    // Console.WriteLine();
    if (numbers.Count == 2)
    {
        sum += numbers[0] * numbers[1];
    }
}


Console.WriteLine(sum);

static bool CheckIfGear(int i, int j, int start, int end, char[][] schematic, out (int, int) location)
{
    bool gear = false;

    location = (0, 0);

    for (var k = start - 1; k < end + 1; k ++)
    {
        if (k < 0 || k > schematic[0].Length - 1)
            continue;

        if (i != 0 && schematic[i - 1][k] == '*')
        {
            gear = true;
            location = (i - 1, k);
            break;
        }
        if (i != schematic.Length - 1 && schematic[i + 1][k] == '*')
        {
            gear = true;
            location = (i + 1, k);
            break;
        }
    }

    if (start != 0 && schematic[i][start - 1] == '*')
    {
        gear = true;
        location = (i, start - 1);
    }

    if (end < schematic[0].Length && schematic[i][end] == '*')
    {
        gear = true;
        location = (i, end);
    }

    return gear;
}

static bool CheckIfEnginePart(int i, int j, int start, int end, char[][] schematic)
{
    bool enginePart = false;

    for (var k = start - 1; k < end + 1; k ++)
    {
        if (k < 0 || k > schematic[0].Length - 1)
            continue;

        if (i != 0 && !char.IsDigit(schematic[i - 1][k]) && schematic[i - 1][k] != '.')
        {
            enginePart = true;
            break;
        }
        if (i != schematic.Length - 1 && !char.IsDigit(schematic[i + 1][k]) && schematic[i + 1][k] != '.')
        {
            enginePart = true;
            break;
        }
    }

    if (start != 0 && !char.IsDigit(schematic[i][start - 1]) && schematic[i][start - 1] != '.')
        enginePart = true;

    if (end < schematic[0].Length && !char.IsDigit(schematic[i][end]) && schematic[i][end] != '.')
        enginePart = true;

    return enginePart;
}

static char[][] CreateSchematic(string[] lines)
{
    var schematic = new char[lines.Length][];

    for (var i = 0; i < lines.Length; i++)
    {
        char[] symbols = [.. lines[i]];
        schematic[i] = symbols;
    }

    return schematic;
}