var depths = File.ReadAllLines("input.txt")
                 .Select(line => int.Parse(line));

// Part 1
var incs = depths.Zip(depths.Skip(1))
                 .Count(pair => pair.Second > pair.First);
Console.WriteLine(incs);

// Part 2
var windows = depths.Zip(depths.Skip(1), depths.Skip(2))
                    .Select(tr => tr.First + tr.Second + tr.Third);
var incs2 = windows.Zip(windows.Skip(1))
                   .Count(pair => pair.Second > pair.First);
Console.WriteLine(incs2);
