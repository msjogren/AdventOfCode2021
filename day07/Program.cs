var input = File.ReadAllText("input.txt")
                .Split(',')
                .Select(int.Parse);

long Solve(Func<int, int, long> cost)
    => Enumerable.Range(input.Min(), input.Max() - input.Min() + 1)
                 .Min(target => input.Sum(crab => cost(crab, target)));

// Part 1
Console.WriteLine(Solve((crabPos, targetPos) => Math.Abs(crabPos - targetPos)));

// Part 2
// 1 + 2 + ... + (n-1) + n = n(n+1)/2
Console.WriteLine(Solve((crabPos, targetPos) => {
    long n = Math.Abs(crabPos - targetPos);
    return n * (n + 1) / 2;
}));
