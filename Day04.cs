public class Day04(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{
    protected override string SolvePartOne()
    {
        var result = 0;
        var input = Input.Input_TwoDCharArray;

        var rolls = new HashSet<(int x, int y)>();
        for (int x = 0; x < input.Length; x++)
        {
            for (int y = 0; y < input[x].Length; y++)
            {
                if (input[x][y].ToString() == "@") rolls.Add((x, y));
            }
        }

        foreach (var roll in rolls)
        {
            var neighborCount = 0;
            if (roll.HasRollNorthWest(input)) neighborCount++;
            if (roll.HasRollNorth(input)) neighborCount++;
            if (roll.HasRollNorthEast(input)) neighborCount++;
            if (roll.HasRollWest(input)) neighborCount++;
            if (roll.HasRollEast(input)) neighborCount++;
            if (roll.HasRollSouthWest(input)) neighborCount++;
            if (roll.HasRollSouth(input)) neighborCount++;
            if (roll.HasRollSouthEast(input)) neighborCount++;

            if(neighborCount < 4) result++;
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

public static class DayExtensions
{
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