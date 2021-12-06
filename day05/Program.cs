var input = File.ReadAllLines("input.txt");

(int x, int y) ParseCoord(string s)
{
    int comma = s.IndexOf(',');
    return (int.Parse(s[..comma]), int.Parse(s[(comma+1)..]));
}

int Solve(bool part2)
{
    var grid = new Dictionary<(int x, int y), int>();

    foreach (var line in input)
    {
        var coords = line.Split(" -> ");
        (int x1, int y1) = ParseCoord(coords[0]); 
        (int x2, int y2) = ParseCoord(coords[1]);

        if (!part2 && x1 != x2 && y1 != y2) continue;

        var dx = Math.Sign(x2 - x1);
        var dy = Math.Sign(y2 - y1);
        for (int x = x1, y = y1; ; x += dx, y += dy)
        {
            int n = 0;
            grid.TryGetValue((x, y), out n);
            grid[(x, y)] = ++n;

            if ((dx != 0 && x == x2) || (dy != 0 && y == y2)) break;
        }
    }

    return grid.Values.Count(n => n > 1);
}

Console.WriteLine(Solve(false));
Console.WriteLine(Solve(true));
