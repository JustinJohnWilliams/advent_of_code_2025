public class Day01(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{    
    protected override string SolvePartOne()
    {
        var result = 0;
        var input = Input.Input_MultiLineTextArray;

        var position = 50;
        foreach(var num in input)
        {
            var dir = num[..1];
            var amt = num[1..].ToInt32();
            if(dir == "L")
            {
                position = (position - amt) % 100;
                if(position < 0) position += 100;
            }
            if(dir == "R")
            {
                position = (position + amt) % 100;
            }

            if(position == 0) result++;
        }

        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        var result = 0;
        var input = Input.Input_MultiLineTextArray;

        var position = 50;
        foreach(var num in input)
        {
            var dir = num[..1];
            var amt = num[1..].ToInt32();

            result += CountZeros(position, amt, dir);
            if(dir == "L")
            {
                position = (position - amt) % 100;
                if(position < 0) position += 100;
            }
            if(dir == "R")
            {
                position = (position + amt) % 100;
            }
        }

        return result.ToString();
    }

    private int CountZeros(int position, int distance, string dir)
    {
        var zeros = 0;

        var fullTurns = distance / 100;
        zeros += fullTurns;

        int remainder = distance % 100;
        if (remainder == 0)
            return zeros;

        int stepDir = dir == "L" ? -1 : 1;
        int pos = position;

        for (int i = 0; i < remainder; i++)
        {
            pos += stepDir;
            if (pos < 0) pos += 100;
            else if (pos >= 100) pos -= 100;

            if (pos == 0)
                zeros++;
        }

        return zeros;
    }
}
