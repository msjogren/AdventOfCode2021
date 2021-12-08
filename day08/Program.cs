var input = File.ReadAllLines("input.txt");

string SortChars(string s)
{
    var a = s.ToCharArray();
    Array.Sort(a);
    return new string(a);
}

int part1 = 0, part2 = 0;

foreach (var line in input)
{
    var parts = line.Split(" | ");
    var signalPatterns = parts[0].Split(' ');
    var outputValueDigits = parts[1].Split(' ');

    // Part 1
    part1 += outputValueDigits.Count(d => d.Length <= 4 || d.Length == 7);

    // Part 2
    var patternToDigitMap = new Dictionary<string, int>();

    // 1 is the only one with two segments
    var onePattern = SortChars(signalPatterns.Single(p => p.Length == 2));    
    patternToDigitMap.Add(onePattern,  1);

    // 4 is the only one with four segments
    var fourPattern = SortChars(signalPatterns.Single(p => p.Length == 4));
    patternToDigitMap.Add(fourPattern,  4);

    // 7 is the only one with three segments
    patternToDigitMap.Add(SortChars(signalPatterns.Single(p => p.Length == 3)),  7);

    // 8 is the only one with all seven segments
    patternToDigitMap.Add(SortChars(signalPatterns.Single(p => p.Length == 7)),  8);

    // Five segments can be 2, 3 or 5.
    // Only 3 will share the two segments of 1.
    var threePattern = SortChars(signalPatterns.Single(p => p.Length == 5 && p.ToCharArray().Intersect(onePattern.ToCharArray()).Count() == 2));
    patternToDigitMap.Add(threePattern,  3);
    // Only 2 will share only two segments with 4.
    var twoPattern = SortChars(signalPatterns.Single(p => p.Length == 5 && p.ToCharArray().Intersect(fourPattern.ToCharArray()).Count() == 2));
    patternToDigitMap.Add(twoPattern,  2);
    // The remaining one must be 5.
    patternToDigitMap.Add(signalPatterns.Where(p => p.Length == 5).Select(s => SortChars(s)).Except(new[] { twoPattern, threePattern }).Single(), 5);

    // Six segments can be 0, 6 or 9.
    // Only 6 will have a single segment shared with 1.
    var sixPattern = SortChars(signalPatterns.Single(p => p.Length == 6 && p.ToCharArray().Intersect(onePattern.ToCharArray()).Count() == 1));
    patternToDigitMap.Add(sixPattern,  6);
    // Only 9 will share all four segments of 4.
    var ninePattern = SortChars(signalPatterns.Single(p => p.Length == 6 && p.ToCharArray().Intersect(fourPattern.ToCharArray()).Count() == 4));
    patternToDigitMap.Add(ninePattern,  9);
    // The remaining one must be 0.
    patternToDigitMap.Add(signalPatterns.Where(p => p.Length == 6).Select(s => SortChars(s)).Except(new[] { sixPattern, ninePattern }).Single(), 0);

    part2 += outputValueDigits.Select(d => patternToDigitMap[SortChars(d)]).Aggregate((l, r) => l * 10 + r);
}

Console.WriteLine(part1);
Console.WriteLine(part2);
