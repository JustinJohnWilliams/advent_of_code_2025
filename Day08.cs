public class Day08(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{
    protected override string SolvePartOne()
    {
        const int K = 1000;
        var result = 1L;
        var input = Input.Input_MultiLineTextArray
            .Select(c => c.Split(',', StringSplitOptions.RemoveEmptyEntries))
            .Select(c => (x: c[0].ToInt64(), y: c[1].ToInt64(), z: c[2].ToInt64()))
            .ToList();

        var n = input.Count;
        var edgeMax = n * (n - 1) / 2;
        var edges = new List<Edge>(edgeMax);

        for (int i = 0; i < n; i++)
        {
            var p1 = input[i];
            for (int j = i + 1; j < n; j++)
            {
                var p2 = input[j];
                var dx = p1.x - p2.x;
                var dy = p1.y - p2.y;
                var dz = p1.z - p2.z;
                var dist = (long)(dx * dx + dy * dy + dz * dz);
                edges.Add(new Edge(i, j, dist));
            }
        }

        edges.Sort((a, b) => a.DistanceSquared.CompareTo(b.DistanceSquared));

        // Create initial circuit for each box
        var circuit = new int[n];
        for(int i = 0; i < n; i++)
        {
            circuit[i] = i;
        }

        var limit = Math.Min(K, edges.Count);
        for(int i = 0; i < limit; i++)
        {
            var e = edges[i];
            MergeCircuits(e.A, e.B, circuit);
        }

        var counts = new Dictionary<int, long>();
        for(int i = 0; i < n; i++)
        {
            var id = circuit[i];
            if(counts.TryGetValue(id, out var size))
            {
                counts[id] = size + 1;
            }
            else
            {
                counts[id] = 1;
            }
        }

        var largest = counts.Values
            .OrderByDescending(c => c)
            .Take(3)
            .ToArray();
        
        for(int i = 0; i < 3; i++)
        {
            var value = i < largest.Length ? largest[i] : 1L;
            result *= value;
        }


        return result.ToString();
    }

    private void MergeCircuits(int a, int b, int[] circuit)
    {
        var ca = circuit[a];
        var cb = circuit[b];

        if(ca == cb) return;

        var toId = Math.Min(ca, cb);
        var fromId = Math.Max(ca, cb);

        for(int i = 0; i < circuit.Length; i++)
        {
            if(circuit[i] == fromId)
            {
                circuit[i] = toId;
            }
        }
    }

    protected override string SolvePartTwo()
    {
        var result = 0L;
        var input = Input.Input_MultiLineTextArray;

        return result.ToString();
    }
}

public class Edge(int a, int b, long distanceSquared)
{
    public int A { get; } = a;
    public int B { get; } = b;
    public long DistanceSquared { get; } = distanceSquared;
}



