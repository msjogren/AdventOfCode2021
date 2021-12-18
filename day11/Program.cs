var input = File.ReadAllLines("input.txt");
int width = input[0].Length, height = input.Length;
var energyLevels = new Dictionary<(int x, int y), int>();

for (int y = 0; y < height; y++)
{
    for (int x = 0; x < width; x++)
    {
        energyLevels.Add((x, y), input[y][x] - '0');
    }
}

int part1TotalFlashes = 0, part2Step = 0;

for (int step = 1; part2Step == 0; step++)
{
    var flashers = new Queue<(int x, int y)>();

    foreach (var pt in energyLevels.Keys)
    {
        if (++energyLevels[pt] > 9) flashers.Enqueue(pt);
    }

    while (flashers.Count > 0)
    {
        var flash = flashers.Dequeue();
        if (energyLevels[flash] == -1) continue;
        energyLevels[flash] = -1;

        foreach (var offset in new (int x, int y)[] {(-1, -1), (0, -1), (1, -1), (-1, 0), (1, 0), (-1, 1), (0, 1), (1, 1)})
        {
            var adjacent = (flash.x + offset.x, flash.y + offset.y);
            if (!energyLevels.TryGetValue(adjacent, out var adjacentEnergy) || adjacentEnergy == -1) continue;
            if (++energyLevels[adjacent] > 9) flashers.Enqueue(adjacent);
        }
    }

    int stepFlashes = 0;
    foreach (var pt in energyLevels.Keys)
    {
        if (energyLevels[pt] == -1)
        {
            energyLevels[pt] = 0;
            stepFlashes++;
        }
    }
    
    if (step <= 100)
    {
        part1TotalFlashes += stepFlashes;
    }

    if (stepFlashes == width * height)
    {
        part2Step = step;
    }
}

Console.WriteLine(part1TotalFlashes);
Console.WriteLine(part2Step);
