string[] lines = File.ReadAllLines("input1.txt");

for (var i = 0; i < lines.Length; i++)
{
    lines[i] = lines[i].Split(':')[1].Trim();
}

var sum = 0;
Dictionary<int, int> copiesOfCardWon = new();


for (var i = 0; i< lines.Length; i++)
{
    var splitLine = lines[i].Split('|');
    var unprocessedWinningNumbers = splitLine[0].Trim();
    var unprocessedSelectedNumbers = splitLine[1].Trim();

    int[] winningNumbers = (from number in unprocessedWinningNumbers.Split(' ')
                            where number is not ""
                            select int.Parse(number)).ToArray();

    int[] selectedNumbers = (from number in unprocessedSelectedNumbers.Split(' ')
                            where number is not ""
                            select int.Parse(number)).ToArray();


    if (!copiesOfCardWon.TryGetValue(i, out int numOfCopies))
    {
        numOfCopies = 1;
    }

    int value = 0;

    foreach (int selectedNumber in selectedNumbers)
    {
        if (winningNumbers.Contains(selectedNumber))
        {
            // value = value == 0 ? 1 : value * 2;
            value++;
        }
    }

    for (var j = 1; j <= value; j++)
    {
        if (!copiesOfCardWon.ContainsKey(i + j))
        {
            copiesOfCardWon.Add(i + j, numOfCopies + 1);
        }
        else {
            copiesOfCardWon[i + j] += numOfCopies;
        }
    }
    
    sum += numOfCopies;
    // Console.WriteLine(numOfCopies);
    
}

Console.WriteLine(sum);