public class Day02(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{    
    protected override string SolvePartOne()
    {
        var result = 0L;
        var input = Input.Input_SingleLineTextSplitEntriesBy(',').ToList();
        var ranges = new List<(long min, long max)>();

        foreach(var i in input)
        {
            var parts = i.Split('-');
            ranges.Add((parts[0].ToInt64(), parts[1].ToInt64()));
        }

        foreach(var (min, max) in ranges)
        {
            var set = EnumerableEx.Range(min, (max - min) + 1);
            foreach(var num in set)
            {
                if(IsRepeat(num)) result += num;
            }
        }

        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        var result = 0;
        var input = Input.Example_SingleLineText;

        return result.ToString();
    }

    private bool IsRepeat(long num)
    {
        var s = num.ToString();

        if(s.Length % 2 != 0) return false;

        var half = s.Length / 2;
        for(int i = 0; i < half; i++)
        {
            if(s[i] != s[i + half]) return false;
        }

        return true;
    }
}
