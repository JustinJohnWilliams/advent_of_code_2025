public class Day02(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{    
    protected override string SolvePartOne()
    {
        var result = 0L;
        var ranges = Input.Input_SingleLineTextSplitEntriesBy(',')
            .Select(c => c.SplitAndRemoveEmpty('-'))
            .Select(c => (min: c[0].ToInt64(), max: c[1].ToInt64()));

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
        var result = 0L;
        var ranges = Input.Input_SingleLineTextSplitEntriesBy(',')
            .Select(c => c.SplitAndRemoveEmpty('-'))
            .Select(c => (min: c[0].ToInt64(), max: c[1].ToInt64()));

        foreach(var (min, max) in ranges)
        {
            var set = EnumerableEx.Range(min, (max - min) + 1);
            foreach(var num in set)
            {
                if(IsRepeat2(num)) result += num;
            }
        }

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

    private bool IsRepeat2(long num)
    {
        var s = num.ToString();
        var len = s.Length;

        // grab chunks that fit half the string
        for(int chunkLength = 1; chunkLength <= len / 2; chunkLength++)
        {
            // make sure it's an evnely divisible chunk e.g. 21212 12118 can be 1, 2, or 5
            if(len % chunkLength != 0) continue;

            var repeats = len / chunkLength;
            if(repeats < 2) continue;

            bool match = true;
            // cycle through chunk parts to match chunk base. gpt came in clutch with this pattern.
            // e.g.
            // i:      0 1 2 3 4 5 6 7 8 9
            // i % 2:  0 1 0 1 0 1 0 1 0 1
            // i % 3:  0 1 2 0 1 2 0 1 2
            for(int i = chunkLength; i < len; i++)
            {
                // s[i] must match the corresponding chunk character:
                // example: "1212121212", chunkLen = 2, chunk="12"
                //
                // i=0 → skip (base chunk)
                // i=1 → skip
                //
                // i=2 → s[2] vs s[0] ('1' vs '1')
                // i=3 → s[3] vs s[1] ('2' vs '2')
                // i=4 → s[4] vs s[0] ('1' vs '1')
                // i=5 → s[5] vs s[1] ('2' vs '2')
                if(s[i] != s[i % chunkLength])
                {
                    match = false;
                    break;
                }
            }

            if(match) return true;
        }

        return false;
    }
}
