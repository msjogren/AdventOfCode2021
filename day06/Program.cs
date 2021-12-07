var input = File.ReadAllText("input.txt");
var initialState = input.Split(',').Select(int.Parse);

long Solve(int days)
{
    var fishCountByAge = new long[9];
    foreach (var i in initialState)
    {
        fishCountByAge[i]++;
    }

    for (int day = 0; day < days; day++)
    {
        var newFishCount = fishCountByAge[0];
        Array.Copy(fishCountByAge, 1, fishCountByAge, 0, 8);
        fishCountByAge[6] += newFishCount;
        fishCountByAge[8] = newFishCount;
    }

    return fishCountByAge.Sum();
}

Console.WriteLine(Solve(80));
Console.WriteLine(Solve(256));
