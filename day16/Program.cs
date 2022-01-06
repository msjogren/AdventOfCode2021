var input = File.ReadAllText("input.txt");
var binaryInput = input.Select(c => c switch {
    '0' => "0000",
    '1' => "0001",
    '2' => "0010",
    '3' => "0011",
    '4' => "0100",
    '5' => "0101",
    '6' => "0110",
    '7' => "0111",
    '8' => "1000",
    '9' => "1001",
    'A' => "1010",
    'B' => "1011",
    'C' => "1100",
    'D' => "1101",
    'E' => "1110",
    'F' => "1111"
});
var binaryString = String.Join("", binaryInput);
int part1Sum = 0;

int BitsToInt(ReadOnlySpan<char> bits)
{
    System.Diagnostics.Debug.Assert(bits.Length < 32);
    int result = 0;
    for (int bit = 0, ch = bits.Length - 1; bit < bits.Length; bit++, ch--)
    {
        result |= (bits[ch] == '1' ? 1 : 0) << bit;
    }
    return result;
}

(long result, int consumed) ParsePacket(ReadOnlySpan<char> packet)
{
    int versionNum = BitsToInt(packet.Slice(0, 3));
    part1Sum += versionNum;
    int type = BitsToInt(packet.Slice(3, 3));
    int consumed = 6;

    if (type == 0b100)
    {
        // Literal value
        long literalValue = 0;
        bool last = false;
        do
        {
            last = packet[consumed] == '0';
            literalValue = (literalValue << 4) | (long)BitsToInt(packet.Slice(consumed + 1, 4));
            consumed += 5;
        } while (!last);
        return (literalValue, consumed);
    }
    else
    {
        // Operator
        var subResults = new List<long>();
        char lengthTypeId = packet[consumed];
        consumed++;
        if (lengthTypeId == '0')
        {
            int subPacketLength = BitsToInt(packet.Slice(consumed, 15));
            consumed += 15;
            int subPacketEnd = consumed + subPacketLength;
            while (consumed < subPacketEnd)
            {
                var (res, cons) = ParsePacket(packet.Slice(consumed));
                consumed += cons;
                subResults.Add(res);
            }
        }
        else
        {
            int subPacketCount = BitsToInt(packet.Slice(consumed, 11));
            consumed += 11;
            while (subPacketCount-- > 0)
            {
                var (res, cons) = ParsePacket(packet.Slice(consumed));
                consumed += cons;
                subResults.Add(res);
            }
        }

        var result = type switch {
            0b000 => subResults.Sum(),
            0b001 => subResults.Aggregate(1L, (product, factor) => product * factor),
            0b010 => subResults.Min(),
            0b011 => subResults.Max(),
            0b101 => subResults[0] > subResults[1] ? 1 : 0,
            0b110 => subResults[0] < subResults[1] ? 1 : 0,
            0b111 => subResults[0] == subResults[1] ? 1 : 0
        };
        return (result, consumed);
    }
}

var (part2, _) = ParsePacket(binaryString);
Console.WriteLine(part1Sum);
Console.WriteLine(part2);
