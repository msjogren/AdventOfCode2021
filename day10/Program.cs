var input = File.ReadAllLines("input.txt");
int part1 = 0;
var part2 = new List<long>();

foreach (var line in input)
{
    var opened = new Stack<char>();
    bool corrupted = false;

    foreach (char c in line)
    {
        if ("([{<".Contains(c))
        {
            opened.Push(c);
        }
        else
        {
            char o = opened.Pop();
            if (o == '(' && c == ')' || o == '[' && c == ']' || o == '{' && c == '}' || o == '<' && c == '>') continue;
            corrupted = true;
            part1 += c switch {
                ')' => 3,
                ']' => 57,
                '}' => 1197,
                '>' => 25137,
            };
            break;
        }
    }

    if (!corrupted)
    {
        long score = 0;
        while (opened.Any())
        {
            score *= 5;
            score += opened.Pop() switch {
                '(' => 1,
                '[' => 2,
                '{' => 3,
                '<' => 4
            };
        }
        part2.Add(score);
    }
}

Console.WriteLine(part1);
part2.Sort();
Console.WriteLine(part2[part2.Count / 2]);
