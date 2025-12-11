public class Day11(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{
    protected override string SolvePartOne()
    {
        var result = 0L;
        var points = Input.Input_MultiLineTextArray
            .Select(c => c.SplitAndRemoveEmpty(':'))
            .ToDictionary(
                parts => parts[0],
                parts => parts[1].SplitAndRemoveEmpty(' ')
            );

        //points.ToList().ForEach(c => Console.WriteLine($"{c.Key}: {string.Join(',', c.Value)}"));

        var cache = new Dictionary<string, long>();
        var visited = new HashSet<string>();

        result += CountPaths("you", points, cache, visited);

        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        var result = 0L;
        var input = Input.Input_MultiLineTextArray;

        return result.ToString();
    }

    private long CountPaths(string node, Dictionary<string, string[]> graph, Dictionary<string, long> cache, HashSet<string> visited)
    {
        if(node == "out") return 1;

        if(cache.TryGetValue(node, out var cached)) return cached;

        if(!graph.TryGetValue(node, out var outputs) || outputs.Length == 0)
        {
            cache[node] = 0;
            return 0;
        }

        if(!visited.Add(node)) throw new Exception("Cyclical uh oh");

        var total = 0L;

        foreach(var next in outputs)
        {
            total += CountPaths(next, graph, cache, visited);
        }

        visited.Remove(node);
        cache[node] = total;
        return total;
    }
}
