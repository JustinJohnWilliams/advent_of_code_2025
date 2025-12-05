public class Day05(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{
    protected override string SolvePartOne()
    {
        var result = 0;
        var input = Input.Input_MultiLineTextArray;

        var ranges = input.Where(c => c.Contains('-')).ToList()
                    .Select(c => c.SplitAndRemoveEmpty('-'))
                    .Select(c => (min: c[0].ToInt64(), max: c[1].ToInt64()))
                    .ToList();

        var inventory = input.Skip(ranges.Count + 1).Select(c => c.ToInt64());

        foreach(var i in inventory)
        {
            if(ranges.Any(c => c.min <= i && c.max >= i)) result++;
        }

        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        var result = 0L;
        var input = Input.Input_MultiLineTextArray;

        var ranges = input.Where(c => c.Contains('-')).ToList()
                    .Select(c => c.SplitAndRemoveEmpty('-'))
                    .Select(c => (min: c[0].ToInt64(), max: c[1].ToInt64()))
                    .OrderBy(c => c.min)
                    .ToList();

        var currentMin = ranges[0].min;
        var currentMax = ranges[0].max;

        for(int i = 1; i < ranges.Count; i++)
        {
            var next = ranges[i];
            if(next.min <= currentMax + 1)
            {
                if(next.max > currentMax)
                {
                    currentMax = next.max;
                }
            }
            else
            {
                result += (currentMax - currentMin + 1);

                currentMin = next.min;
                currentMax = next.max;
            }
        }

        result += (currentMax - currentMin + 1);

        return result.ToString();
    }
}
