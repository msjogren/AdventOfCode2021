var input = File.ReadAllLines("input.txt");

char MostCommonAtPos(int pos, IEnumerable<string> numbers)
    => (numbers.Count(n => n[pos] == '1') - numbers.Count(n => n[pos] == '0')) >= 0 ? '1' : '0';


char LeastCommonAtPos(int pos, IEnumerable<string> numbers)
    => (numbers.Count(n => n[pos] == '1') - numbers.Count(n => n[pos] == '0')) < 0 ? '1' : '0';

// Part 1
string gamma = "", epsilon = "";
for (int i = 0; i < input[0].Length; i++)
{
    gamma += MostCommonAtPos(i, input);
    epsilon += LeastCommonAtPos(i, input);
}
Console.WriteLine(Convert.ToInt64(gamma, 2) * Convert.ToInt64(epsilon, 2));

// Part 2
string Search(Func<int, IEnumerable<string>, char> criteria, IEnumerable<string> numbers)
{
    for (int i = 0; numbers.Count() > 1; i++)
    {
        var pos = i;
        var keep = criteria(pos, numbers);
        numbers = numbers.Where(n => n[pos] == keep);
    }
    return numbers.Single();
}

var oxygen = Search(MostCommonAtPos, input);
var co2 = Search(LeastCommonAtPos, input);
Console.WriteLine(Convert.ToInt64(oxygen, 2) * Convert.ToInt64(co2, 2));
