var input = File.ReadAllLines("input.txt");
var dots = new HashSet<(int x, int y)>();
var folds = new List<(char axis, int pos)>();

foreach (string line in input)
{
    if (line.Length > 0 && Char.IsDigit(line[0]))
    {
        var parts = line.Split(',');
        dots.Add((int.Parse(parts[0]), int.Parse(parts[1])));
    }
    else if (line.StartsWith("fold"))
    {
        folds.Add((line[11], int.Parse(line[13..])));
    }
}

bool part1 = true;

foreach (var fold in folds)
{
    HashSet<(int x, int y)> newDots = new(), removeDots = new();

    foreach (var pt in dots)
    {
        if (fold.axis == 'x' && pt.x > fold.pos)
        {
            removeDots.Add(pt);
            newDots.Add((fold.pos - Math.Abs(pt.x - fold.pos), pt.y));
        }
        else if (fold.axis == 'y' && pt.y > fold.pos)
        {
            removeDots.Add(pt);
            newDots.Add((pt.x, fold.pos - Math.Abs(pt.y - fold.pos)));
        }
    }

    dots.ExceptWith(removeDots);
    dots.UnionWith(newDots);

    if (part1)
    {
        Console.WriteLine(dots.Count());
        part1 = false;
    }
}

// Part 2
var maxX = dots.Max(pt => pt.x);
var maxY = dots.Max(pt => pt.y);

for (int y = 0; y <= maxY; y++)
{
    for (int x = 0; x <= maxX; x++)
    {
        Console.Write(dots.Contains((x, y)) ? '#' : '.');
    }
    Console.WriteLine();
}
