public class Day10(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{    
    protected override string SolvePartOne()
    {
        var result = 0L;
        var input = Input.Input_MultiLineTextArray;

        foreach(var line in input)
        {
            var (targetMask, buttons) = ParseMachine(line);
            result += SolveMachine(targetMask, buttons);
        }

        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        var result = 0;
        var input = Input.Empty;

        return result.ToString();
    }

    // bitwise mask
    // e.g. [.###.#] -> 011101 ->  (2^1) + (2^2) + (2^3) + (2^5) -> (2 + 4 + 8 + 32) -> 46
    //      (0,1,2,3,4) -> (2^0) + (2^1) + (2^2) + (2^3) + (2^4) -> (1 + 2 + 4 + 8 + 16) -> 31
    public (ulong targetMask, List<ulong> buttons) ParseMachine(string line)
    {
        var parts = line.SplitAndRemoveEmpty(' ');
        var patternToken = parts[0];
        var pattern = patternToken.Trim('[', ']');

        ulong targetMask = 0;
        for(int i = 0; i < pattern.Length; i++)
        {
            if(pattern[i] == '#') targetMask |= 1UL << i;
        }

        var buttons = new List<ulong>();
        for(int i = 1; i < parts.Length - 1; i++)
        {
            var token = parts[i];
            var inside = token.Trim('(', ')').Trim();

            ulong mask = 0;
            var indices = inside.SplitAndRemoveEmpty(',');
            foreach(var idxText in indices)
            {
                var bit = idxText.ToInt32();
                mask |= 1UL << bit;
            }

            buttons.Add(mask);
        }

        return (targetMask, buttons);
    }

    // BFS 
    // e.g. "desired" [.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2)
    //       targetMask: 46, buttons: (31, 25, 55, 6)
    // Press a button once -> lights on. Press it twice -> lights off.
    // This means every solution will be some subset of buttons being pressed exactly once. We never have to consider "Press first button twice and third button 5 times. "twice" is equal to none, "5" is equal to one
    // This makes the possibilities finite (2^L) L: number of lights
    // This boils the problem down to:
    //      We have a graph with up to 2^L nodes (light configurations)
    //      Edges = "press button X", which sends you from state S to S ^ buttonMask
    //      Find the shortest path (fewest presses) from state 0 ([......]) to targetMask ([.###.#])
    // First Round:
    //  Press each button once: {B0,B1,B2,B3}
    // Second Round:
    //  Press each subset of two: ({B0}, {B1}), ({B0, B2}), ({B0,B3}) | ({B1, B2}), ({B1, B3}) | ({B2,B3})
    //  ({B1}, {B2}) -> (0,3,4) (0,1,2,4,5) -> masks: (25) (55) -> 25 XOR 55 = *46*
    // Tree View:
    /*
    Layer 0
      0
    Layer 1
      62   = 0 ^ B0
      25   = 0 ^ B1
      55   = 0 ^ B2
       6   = 0 ^ B3
    Layer 2
      From 62 (B0):
        39 = 62 ^ B1
         9 = 62 ^ B2
        56 = 62 ^ B3
      From 25 (B1):
        46 = 25 ^ B2   <== TARGET FOUND
    */
    public int SolveMachine(ulong targetMask, IReadOnlyList<ulong> buttons)
    {
        if(targetMask == 0UL) return 0;

        var dist = new Dictionary<ulong, int>();
        var queue = new Queue<ulong>();

        const ulong startState = 0UL;
        dist[startState] = 0;
        queue.Enqueue(startState);

        while(queue.Count > 0)
        {
            var state = queue.Dequeue();
            var presses = dist[state];

            if(state == targetMask)
            {
                return presses;
            }

            foreach(var button in buttons)
            {
                var next = state ^ button;
                if(!dist.ContainsKey(next))
                {
                    dist[next] = presses + 1;
                    queue.Enqueue(next);
                }
            }
        }

        return -1;
    }
}
