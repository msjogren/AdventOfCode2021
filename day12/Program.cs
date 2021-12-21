var input = File.ReadAllLines("input.txt");
var connections = new Dictionary<string, HashSet<string>>();

foreach (string line in input)
{
    void AddConnection(string from, string to)
    {
        if (!connections.TryGetValue(from, out var set))
        {
            connections[from] = set = new HashSet<string>();
        }

        set.Add(to);
    }
    
    var parts = line.Split('-');
    if (parts[1] != "start" && parts[0] != "end")
    {
        AddConnection(parts[0], parts[1]);
    }
    if (parts[0] != "start" && parts[1] != "end")
    {
        AddConnection(parts[1], parts[0]);
    }
}

void FindPaths(ISet<IList<string>> foundPaths, string cave, IList<string> path, bool mayVisitSmallCaveTwice)
{
    foreach (var connected in connections[cave])
    {
        var newPath = new List<string>(path) { connected };

        if (connected == "end")
        {
            foundPaths.Add(newPath);
        }
        else
        {
            if (Char.IsLower(connected[0]) && path.Contains(connected))
            {
                if (!mayVisitSmallCaveTwice) continue;
                FindPaths(foundPaths, connected, newPath, false);
            }
            else
            {
                FindPaths(foundPaths, connected, newPath, mayVisitSmallCaveTwice);
            }
        }
    }
}

var part1 = new HashSet<IList<string>>();
FindPaths(part1, "start", new[] { "start" }, false);
Console.WriteLine(part1.Count);

var part2 = new HashSet<IList<string>>();
FindPaths(part2, "start", new[] { "start" }, true);
Console.WriteLine(part2.Count);
