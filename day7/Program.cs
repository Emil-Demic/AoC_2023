
using System.Runtime.CompilerServices;

internal class Program
{
    private static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("input1.txt");

        List<string> fiveOfAKind = [];
        List<string> fourOfAKind = [];
        List<string> fullHouse = [];
        List<string> threeOfAKind = [];
        List<string> twoPair = [];
        List<string> onePair = [];
        List<string> highCard = [];

        foreach (string line in lines)
        {
            var cards = line.Split(' ')[0];

            var cardCounts =  (from c in cards
                        group c by c into counts
                        orderby counts.Count() descending
                        select counts.Count()).ToArray();

            var numOfJ = CountJs(line);

            switch (cardCounts[0])
            {
                case 5:
                    fiveOfAKind.Add(line);
                    break;
                case 4:
                    if (numOfJ != 0)
                        fiveOfAKind.Add(line);
                    else
                        fourOfAKind.Add(line);
                    break;
                case 3:
                    if (cardCounts[1] == 2)
                        if (numOfJ != 0)
                            fiveOfAKind.Add(line);
                        else
                            fullHouse.Add(line);
                    else
                        if (numOfJ != 0)
                            fourOfAKind.Add(line);
                        else
                            threeOfAKind.Add(line);
                    break;
                case 2:
                    if (cardCounts[1] == 2)
                        if (numOfJ == 2)
                            fourOfAKind.Add(line);
                        else if (numOfJ == 1)
                            fullHouse.Add(line);
                        else
                            twoPair.Add(line);
                    else
                        if (numOfJ != 0)
                            threeOfAKind.Add(line);
                        else
                            onePair.Add(line);
                    break;
                case 1:
                    if (numOfJ == 1)
                        onePair.Add(line);
                    else
                        highCard.Add(line);
                    break;
                default:
                    throw new ArgumentException("Invalid argument");
            }
        }
        
        highCard.Sort(CompareHands);
        onePair.Sort(CompareHands);
        twoPair.Sort(CompareHands);
        threeOfAKind.Sort(CompareHands);
        fullHouse.Sort(CompareHands);
        fourOfAKind.Sort(CompareHands);
        fiveOfAKind.Sort(CompareHands);

        var winnings = 0;
        var totalHandsCounted = 0;

        for (var i = 0; i < highCard.Count; i++)
        {
            var rank = totalHandsCounted + i + 1;
            var bid = int.Parse(highCard[i].Split(' ')[1]);
            winnings += rank * bid;
        }
        totalHandsCounted += highCard.Count;

        for (var i = 0; i < onePair.Count; i++)
        {
            var rank = totalHandsCounted + i + 1;
            var bid = int.Parse(onePair[i].Split(' ')[1]);
            winnings += rank * bid;
        }
        totalHandsCounted += onePair.Count;

        for (var i = 0; i < twoPair.Count; i++)
        {
            var rank = totalHandsCounted + i + 1;
            var bid = int.Parse(twoPair[i].Split(' ')[1]);
            winnings += rank * bid;
        }
        totalHandsCounted += twoPair.Count;

        for (var i = 0; i < threeOfAKind.Count; i++)
        {
            var rank = totalHandsCounted + i + 1;
            var bid = int.Parse(threeOfAKind[i].Split(' ')[1]);
            winnings += rank * bid;
        }
        totalHandsCounted += threeOfAKind.Count;

        for (var i = 0; i < fullHouse.Count; i++)
        {
            var rank = totalHandsCounted + i + 1;
            var bid = int.Parse(fullHouse[i].Split(' ')[1]);
            winnings += rank * bid;
        }
        totalHandsCounted += fullHouse.Count;

        for (var i = 0; i < fourOfAKind.Count; i++)
        {
            var rank = totalHandsCounted + i + 1;
            var bid = int.Parse(fourOfAKind[i].Split(' ')[1]);
            winnings += rank * bid;
        }
        totalHandsCounted += fourOfAKind.Count;

        for (var i = 0; i < fiveOfAKind.Count; i++)
        {
            var rank = totalHandsCounted + i + 1;
            var bid = int.Parse(fiveOfAKind[i].Split(' ')[1]);
            winnings += rank * bid;
        }
        totalHandsCounted += fiveOfAKind.Count;

        // foreach (var t in test)
        // {
        //     Console.WriteLine(t);
        // }

        Console.WriteLine(winnings);
        
    }

    private static int CountJs(string s)
    {
        var count = 0;
        foreach (char c in s)
        {
            if (c == 'J')
                count++;
        }
        return count;
    }

    private static int CompareHands(string a, string b)
    {
        char[] ordering = ['A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2', 'J'];
        for (var i = 0; i < a.Length; i++)
        {
            var valA = Array.IndexOf(ordering, a[i]);
            var valB = Array.IndexOf(ordering, b[i]);
            if (valA > valB)
            {
                return -1;
            }
            else if (valB > valA)
            {
                return 1;
            }
        }
        return 0;
    }
}