public class Day01(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{    
    protected override string SolvePartOne()
    {
        var result = 0;
        var input = Input.Input_MultiLineTextArray;

        input.ToList().ForEach(Console.WriteLine);

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

        return result.ToString();
    }
}
