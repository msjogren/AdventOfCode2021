var input = File.ReadAllLines("input.txt");
var numbers = input[0].Split(',').Select(int.Parse);
var rows = new List<Row>();

for (int boardInputLine = 2, boardNum = 1; (boardInputLine + 4) <= input.Length; boardInputLine += 6, boardNum++)
{
    int[][] boardLines = input
        .Skip(boardInputLine)
        .Take(5)
        .Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray())
        .ToArray();

    foreach (var horizontalLine in boardLines)
    {
        rows.Add(new Row()
        {
            Board = boardNum,
            Numbers = horizontalLine
        });
    }

    for (int col = 0; col < 5; col++)
    {
        rows.Add(new Row() 
        {
            Board = boardNum,
            Numbers = new[] { boardLines[0][col], boardLines[1][col], boardLines[2][col], boardLines[3][col], boardLines[4][col] }
        });
    }
}

// Part 1
for (int drawn = 5; ; drawn++)
{
    var drawnNumbers = numbers.Take(drawn);
    var bingoBoard = rows.FirstOrDefault(row => row.Numbers.Intersect(drawnNumbers).Count() == 5);
    if (bingoBoard == null) continue;

    var boardNumbers = rows
        .Where(r => r.Board == bingoBoard.Board)
        .SelectMany(b => b.Numbers)
        .Distinct();
    var unmarkedNumbersSum = boardNumbers
        .Except(drawnNumbers)
        .Sum();
    Console.WriteLine($"{unmarkedNumbersSum} * {drawnNumbers.Last()} = {unmarkedNumbersSum * drawnNumbers.Last()}");
    break;
}

// Part 2
for (int drawn = 5; rows.Any(); drawn++)
{
    var drawnNumbers = numbers.Take(drawn);
    foreach (var bingoBoard in rows.Where(row => row.Numbers.Intersect(drawnNumbers).Count() == 5).ToArray())
    {
        var boardNumbers = rows
            .Where(r => r.Board == bingoBoard.Board)
            .SelectMany(b => b.Numbers)
            .Distinct();
        var unmarkedNumbersSum = boardNumbers
            .Except(drawnNumbers)
            .Sum();
        rows.RemoveAll(r => r.Board == bingoBoard.Board);

        if (!rows.Any())
        {
            Console.WriteLine($"{unmarkedNumbersSum} * {drawnNumbers.Last()} = {unmarkedNumbersSum * drawnNumbers.Last()}");
            break;
        }
    }
}

class Row
{
    public int Board;
    public int[] Numbers; 
}

