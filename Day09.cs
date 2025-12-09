public class Day09(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{
    protected override string SolvePartOne()
    {
        var result = 0L;
        var points = Input.Input_MultiLineTextArray
            .Select(c => c.SplitAndRemoveEmpty(','))
            .Select(c => (x: c[0].ToInt32(), y: c[1].ToInt32()));
        
        var biggest =
            (from p1 in points
             from p2 in points
             where !p1.Equals(p2)
             let width = (long)Math.Abs(p2.x - p1.x) + 1
             let height = (long)Math.Abs(p2.y - p1.y) + 1
             let area = width * height
             select (p1, p2, area))
            .MaxBy(c => c.area);

        result += biggest.area;

        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        var result = 0L;
        var input = Input.Input_MultiLineTextArray;

        return result.ToString();
    }
}
