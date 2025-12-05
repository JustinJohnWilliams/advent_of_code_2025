public class Day04(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{
    protected override string SolvePartOne()
    {
        var result = 0;
        var input = Input.Input_TwoDCharArray;

        for (int x = 0; x < input.Length; x++)
        {
            for (int y = 0; y < input[x].Length; y++)
            {
                if (input[x][y].ToString() != "@") continue;

                if ((x, y).CanRemoveRoll(input)) result++;
            }
        }

        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        var result = 0L;
        var input = Input.Input_TwoDCharArray;

        var round = 0;
        while (true)
        {
            round++;
            var toRemove = new List<(int x, int y)>();

            for (int x = 0; x < input.Length; x++)
            {
                for (int y = 0; y < input[x].Length; y++)
                {
                    if (input[x][y] != '@') continue;

                    if ((x, y).CanRemoveRoll(input)) toRemove.Add((x, y));
                }
            }

            if (toRemove.Count == 0) break;
            foreach (var (x, y) in toRemove)
            {
                input[x][y] = '.';
                result++;
            }
        }

        return result.ToString();
    }
}

public static class DayExtensions
{
    private static readonly List<(int x, int y)> Deltas =
    [
        (-1, -1), (-1, 0), (-1, 1),
        ( 0, -1),          ( 0, 1),
        ( 1, -1), ( 1, 0), ( 1, 1),
    ];

    public static bool CanRemoveRoll(this (int x, int y) roll, char[][] map) => roll.RollNeighborCount(map) < 4;
    public static int RollNeighborCount(this (int x, int y) roll, char[][] map)
    {
        var count = 0;
        foreach (var (dx, dy) in Deltas)
        {
            var nx = roll.x + dx;
            var ny = roll.y + dy;
            if(nx >= 0 && nx < map.Length && ny >= 0 && ny < map[0].Length
                && map[nx][ny] == '@')
            {
                count++;
            }
        }

        return count;
    }
}
