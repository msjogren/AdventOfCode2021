var input = File.ReadAllLines("input.txt");
var template = input[0];
var rules = input.Skip(2).ToDictionary(line => (left: line[0], right: line[1]), line => line[6]);

long Solve(int n)
{
    var pairCounts = rules.Keys.ToDictionary(chars => chars, chars => 0L);
    var charCounts = rules.Keys.SelectMany(chars => new[] {chars.left, chars.right}).Distinct().ToDictionary(c => c, c => 0L);

    for (int i = 0; i < template.Length - 1; i++)
    {
        pairCounts[(template[i], template[i+1])]++;
        charCounts[template[i]]++;
    }
    charCounts[template.Last()]++;

    while (n-- > 0)
    {
        var oldPairCounts = new Dictionary<(char left, char right), long>(pairCounts);
        foreach (var kvp in oldPairCounts.Where(kvp => kvp.Value > 0))
        {
            pairCounts[kvp.Key] -= kvp.Value;
            char insertedChar = rules[kvp.Key];
            pairCounts[(kvp.Key.left, insertedChar)] += kvp.Value;
            pairCounts[(insertedChar, kvp.Key.right)] += kvp.Value;
            charCounts[insertedChar] += kvp.Value;
        }
    }

    return charCounts.Values.Max() - charCounts.Values.Min();
}

Console.WriteLine(Solve(10));
Console.WriteLine(Solve(40));
