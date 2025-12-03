public class Day03(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{
    protected override string SolvePartOne()
    {
        var result = 0;
        var input = Input.Input_MultiLineTextArray;

        foreach (var line in input)
        {
            var maxFirst = 0;
            var maxSecond = 0;
            var idx = 0;
            for (int i = 0; i < line.Length - 1; i++)
            {
                if (line[i].ToString().ToInt32() > maxFirst)
                {
                    maxFirst = line[i].ToString().ToInt32();
                    idx = i;
                }
            }

            var newLine = line.Substring(idx + 1);

            for (int i = 0; i < newLine.Length; i++)
            {
                if (newLine[i].ToString().ToInt32() > maxSecond)
                {
                    maxSecond = newLine[i].ToString().ToInt32();
                }
            }

            var max = $"{maxFirst}{maxSecond}";
            result += max.ToInt32();
        }

        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        var result = 0L;
        var input = Input.Input_MultiLineTextArray;

        foreach(var line in input)
        {
            var joltage = GetMaxJoltage(line, 12);
            //Console.WriteLine(joltage);
            result += joltage;
        }

        return result.ToString();
    }

    private long GetMaxJoltage(string line, int maxLength)
    {
        var length = line.Length;
        if(maxLength >= length) return line.ToInt64(); // bail if less than length
        var sb = new StringBuilder(maxLength);

        var start = 0;
        for(int pos = 0; pos < maxLength; pos++)
        {
            var remainingPositions = maxLength - pos;
            var endExclusive = length - remainingPositions + 1;

            var bestDigit = 0;
            var idx = start;

            for(int i = start; i < endExclusive; i++)
            {
                var digit = line[i].ToString().ToInt32();
                if(digit > bestDigit)
                {
                    bestDigit = digit;
                    idx = i;
                    if(digit == 9) break;
                }
            }
            sb.Append(bestDigit);

            start = idx + 1;
        }

        return sb.ToString().ToInt64();
    }
}


