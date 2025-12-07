public class Day06(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{
    protected override string SolvePartOne()
    {
        var result = 0L;
        var input = Input.Input_MultiLineTextArray;

        var nums = input.Take(input.Length - 1)
                    .SelectMany(c => c.SplitAndRemoveEmpty(' '))
                    .Select(long.Parse)
                    .ToArray();
        
        var operators = input.Last().SplitAndRemoveEmpty(' ');

        var rows = input.Take(input.Length - 1).Count();
        var cols = operators.Length;

        for(int col = 0; col < cols; col++)
        {
            var op = operators[col];
            var sum = 0L;
            var product = 1L;
            for(int row = 0; row < rows; row++)
            {
                var idx = row * cols + col;
                sum += nums[idx];
                product *= nums[idx];
            }
            result += op == "+" ? sum : product;
        }
        
        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        var result = 0L;
        var input = Input.Input_MultiLineTextArray;

        return result.ToString();
    }
}

