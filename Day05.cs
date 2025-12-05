public class Day05(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{
    protected override string SolvePartOne()
    {
        var result = 0;
        var input = Input.Input_MultiLineTextArray;

        var ranges = input.Where(c => c.Contains('-')).ToList()
                    .Select(c => c.Split('-'))
                    .Select(c => (c[0].ToInt64(), c[1].ToInt64()))
                    .ToList();

        var inventory = input.Skip(ranges.Count + 1).Select(c => c.ToInt64());

        foreach(var i in inventory)
        {
            if(ranges.Any(c => c.Item1 <= i && c.Item2 >= i)) result++;
        }

        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        var result = 0L;
        var input = Input.Input_TwoDCharArray;

        return result.ToString();
    }
}
