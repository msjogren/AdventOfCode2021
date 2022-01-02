var input = File.ReadAllLines("input.txt");
int inputSize = input.Length;
var inputGrid = new int[inputSize, inputSize];

for (int y = 0; y < inputSize; y++)
{
    for (int x = 0; x < inputSize; x++)
    {
        inputGrid[x, y] = input[y][x] - '0';
    }
}

int AStarSearch(int[,] grid, int size, (int x, int y) start, (int x, int y) end)
{
    var queue = new PriorityQueue<(int x, int y), int>();
    queue.Enqueue(start, 0);
    var costs = new Dictionary<(int x, int y), int>();
    costs[start] = 0;

    while (queue.Count > 0)
    {
        var current = queue.Dequeue();
        if (current == end) return costs[current];

        foreach (var offset in new (int x, int y)[] {(-1, 0), (0, -1), (1, 0), (0, 1) })
        {
            if (current.x + offset.x < 0 || current.x + offset.x >= size || current.y + offset.y < 0 || current.y + offset.y >= size) continue;
            var adjacent = (x: current.x + offset.x, y: current.y + offset.y);
            var cost = costs[current] + grid[adjacent.x, adjacent.y];
            if (costs.TryGetValue(adjacent, out int existingCost) == false || existingCost > cost)
            {
                costs[adjacent] = cost;
                queue.Enqueue(adjacent, cost + Math.Abs(end.x - adjacent.x) + Math.Abs(end.y - adjacent.y));
            }
        }
    }

    return -1;
}

// Part 1
var part1 = AStarSearch(inputGrid, inputSize, (0, 0), (inputSize - 1, inputSize - 1));
Console.WriteLine(part1);

// Part 2
int part2Size = 5 * inputSize;
var part2Grid = new int[part2Size, part2Size];

for (int y = 0; y < part2Size; y++)
{
    int sourceY = y % inputSize, repeatY = y / inputSize;
    for (int x = 0; x < part2Size; x++)
    {
        int sourceX = x % inputSize, repeatX = x / inputSize;
        part2Grid[x, y] = ((inputGrid[sourceX, sourceY] + repeatX + repeatY - 1) % 9) + 1;
    }
}

var part2 = AStarSearch(part2Grid, part2Size, (0, 0), (part2Size - 1, part2Size - 1));
Console.WriteLine(part2);
