var input = File.ReadAllLines("input.txt");
int height = input.Length, width = input[0].Length;
var basins = new List<HashSet<(int x, int y)>>();

IEnumerable<(int x, int y)> Adjacent(int x, int y)
{
    if (y > 0) yield return ((x, y - 1));
    if (y < height - 1) yield return ((x, y + 1));
    if (x > 0) yield return ((x - 1, y));
    if (x < width - 1) yield return ((x + 1, y));
}

void FloodFillBasin(HashSet<(int x, int y)> basin, (int x, int y) pt)
{
    if (input[pt.y][pt.x] == '9' || basin.Contains(pt)) return;

    basin.Add(pt);
    
    foreach (var adj in Adjacent(pt.x, pt.y))
        FloodFillBasin(basin, adj);
}

int part1 = 0;
for (int y = 0; y < height; y++)
{
    for (int x = 0; x < width; x++)
    {
        if (Adjacent(x, y).All(a => input[y][x] < input[a.y][a.x]))
        {
            // Part 1
            part1 += (input[y][x] - '0') + 1;

            // Part 2
            basins.Add(new());
            FloodFillBasin(basins.Last(), (x, y));
        }
    }
}

Console.WriteLine(part1);

int part2 = basins.Select(set => set.Count)
                  .OrderByDescending(n => n)
                  .Take(3)
                  .Aggregate((product, factor) => product * factor);

Console.WriteLine(part2);
