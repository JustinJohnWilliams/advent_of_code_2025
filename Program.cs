var days = new IDay[]
{
    new Day01("Day 01: TODO",         "01.txt", "01.txt",             "", ""),
};

var table = new ConsoleTable("Day", "Problem", "Result", "Time (ms)");
var sw = Stopwatch.StartNew();

foreach (var day in days)
{
    if (day.Result1.IsNotNullOrEmpty())
    {
        var p1 = day.PartOne();
        if (p1.result != day.Result1) table.AddRow($"---ERROR---", "1", $"{p1.result} != {day.Result1}", "");
        else table.AddRow($"{day.Name}", "1", p1.result, p1.ms);
    }
    if (day.Result2.IsNotNullOrEmpty())
    {
        var p2 = day.PartTwo();
        if (p2.result != day.Result2) table.AddRow($"---ERROR---", "2", $"{p2.result} != {day.Result2}", "");
        else table.AddRow($"{day.Name}", "2", p2.result, p2.ms);
    }
}

sw.Stop();
var ms = sw.Elapsed.TotalMilliseconds;

table.AddRow($"TOTAL", "", "", ms);

table.Write(Format.MarkDown);
