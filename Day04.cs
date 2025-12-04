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
    public static bool CanRemoveRoll(this (int x, int y) roll, char[][] map) => roll.RollNeighborCount(map) < 4;
    public static int RollNeighborCount(this (int x, int y) roll, char[][] map)
    {
        var count = 0;
        if (roll.HasRollNorthWest(map)) count++;
        if (roll.HasRollNorth(map))     count++;
        if (roll.HasRollNorthEast(map)) count++;
        if (roll.HasRollWest(map))      count++;
        if (roll.HasRollEast(map))      count++;
        if (roll.HasRollSouthWest(map)) count++;
        if (roll.HasRollSouth(map))     count++;
        if (roll.HasRollSouthEast(map)) count++;
        return count;
    }

    public static bool HasRollNorthWest(this (int x, int y) roll, char[][] map) =>
        (roll.x - 1 >= 0) && (roll.y - 1 >= 0)
        && (map[roll.x - 1][roll.y - 1] == '@');
    public static bool HasRollNorth(this (int x, int y) roll, char[][] map) =>
        (roll.x - 1 >= 0)
        && (map[roll.x - 1][roll.y] == '@');
    public static bool HasRollNorthEast(this (int x, int y) roll, char[][] map) =>
        (roll.x - 1 >= 0) && (roll.y < map[0].Length - 1)
        && (map[roll.x - 1][roll.y + 1] == '@');
    public static bool HasRollWest(this (int x, int y) roll, char[][] map) =>
        (roll.y - 1 >= 0)
        && (map[roll.x][roll.y - 1] == '@');
    public static bool HasRollEast(this (int x, int y) roll, char[][] map) =>
        (roll.y < map[0].Length - 1)
        && (map[roll.x][roll.y + 1] == '@');
    public static bool HasRollSouthWest(this (int x, int y) roll, char[][] map) =>
        (roll.x < map.Length - 1) && (roll.y - 1 >= 0)
        && (map[roll.x + 1][roll.y - 1] == '@');
    public static bool HasRollSouth(this (int x, int y) roll, char[][] map) =>
        (roll.x < map.Length - 1)
        && (map[roll.x + 1][roll.y] == '@');
    public static bool HasRollSouthEast(this (int x, int y) roll, char[][] map) =>
        (roll.x < map.Length - 1) && (roll.y < map[0].Length - 1)
        && (map[roll.x + 1][roll.y + 1] == '@');
}
