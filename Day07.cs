public class Day07(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{
    protected override string SolvePartOne()
    {
        var result = 0L;
        var input = Input.Input_TwoDCharArray;
        var (rows, cols) = (input.Length, input[0].Length);

        var startPos =
            (from x in Enumerable.Range(0, rows)
             from y in Enumerable.Range(0, cols)
             where input[x][y] == 'S'
             select (x, y))
            .First();

        var beams = new HashSet<(int x, int y)>{ startPos };

        for (int x = startPos.x; x < rows - 1; x++)
        {
            var nextBeams = new HashSet<(int x, int y)>();
            foreach (var beam in beams)
            {
                int y = beam.y;
                char below = input[x + 1][y];
                if (below == '^')
                {
                    result++;
                    if (y - 1 >= 0) nextBeams.Add((x + 1, y - 1));
                    if (y + 1 < cols) nextBeams.Add((x + 1, y + 1));
                }
                else
                {
                    nextBeams.Add((x + 1, y));
                }
            }

            beams = nextBeams;
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


