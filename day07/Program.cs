var input = File.ReadAllText("input.txt")
                .Split(',')
                .Select(int.Parse);

int Solve(Func<int, int> cost)
    => Enumerable.Range(input.Min(), input.Max() - input.Min() + 1)
                 .Min(target => input.Sum(crab => cost(Math.Abs(crab - target))));

// Part 1
Console.WriteLine(Solve(distance => distance));

// Part 2
// 1 + 2 + ... + (n-1) + n = n(n+1)/2
Console.WriteLine(Solve(distance => distance * (distance + 1) / 2));
