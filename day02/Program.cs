var input = File.ReadAllLines("input.txt")
                .Select(line => line.Split())
                .Select(parts => (direction: parts[0], x: int.Parse(parts[1])));

// Part 1
var forwards = input.Where(cmd => cmd.direction == "forward").Sum(cmd => cmd.x);
var ups      = input.Where(cmd => cmd.direction == "up").Sum(cmd => cmd.x);
var downs    = input.Where(cmd => cmd.direction == "down").Sum(cmd => cmd.x);
Console.WriteLine(forwards * (downs - ups));

// Part 2
var (aim, depth, xpos) = (0L, 0L, 0L);
foreach (var cmd in input)
{
    (aim, depth, xpos) = cmd.direction switch
    {
        "forward" => (aim,         depth + aim * cmd.x, xpos + cmd.x),
        "up"      => (aim - cmd.x, depth,               xpos), 
        "down"    => (aim + cmd.x, depth,               xpos),
        _         => throw new InvalidOperationException()
    };    
}
Console.WriteLine(xpos * depth);
