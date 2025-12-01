var days = new IDay[]
{
    new Day01("Day 01: Secret Entrance", "01.txt", "01.txt", "1066", ""),
};

var sb = new StringBuilder();
var starCount = 0;
var table = new ConsoleTable("Day", "Result", "Time (ms)", "");
var sw = Stopwatch.StartNew();

foreach (var day in days)
{
    var p1 = day.PartOne();
    var p2 = day.PartTwo();

    if (day.Expected1.IsNotNullOrEmpty())
    {
        if (p1.result != day.Expected1) table.AddRow($"---ERROR---", $"{p1.result} != {day.Expected1}", "", "");
        else table.AddRow($"{day.Name}", p1.result, p1.ms, day.SilverStar() ? "✩" : day.GoldStar() ? "★" : "");
    }
    if (day.Expected2.IsNotNullOrEmpty())
    {
        if (p2.result != day.Expected2) table.AddRow($"⤷ ---ERROR---", $"{p2.result} != {day.Expected2}", "", "");
        else table.AddRow($"⤷ Part 2", p2.result, p2.ms, day.GoldStar() ? "★" : "");
    }

    if(day.SilverStar())
    {
        sb.Append('✩');
        starCount++;
    }

    if(day.GoldStar())
    {
        sb.Append('★');
        starCount+=2;
    }
}

sw.Stop();
var ms = sw.Elapsed.TotalMilliseconds;

table.AddRow("", "", "", "-");
table.AddRow("TOTAL", sb.ToString(), ms, $"{starCount}");

table.Write(Format.MarkDown);
