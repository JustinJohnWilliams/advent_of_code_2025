var days = new IDay[]
{
    new Day01("Day 01: Secret Entrance",        "01.txt", "01.txt", "1066",             "6223"),
    new Day02("Day 02: Gift Shop",              "02.txt", "02.txt", "38158151648",      "45283684555"),
    new Day03("Day 03: Lobby",                  "03.txt", "03.txt", "17403",            "173416889848394"),
    new Day04("Day 04: Printing Department",    "04.txt", "04.txt", "1351",             "8345"),
    new Day05("Day 05: Cafeteria",              "05.txt", "05.txt", "681",              "348820208020395"),
    new Day06("Day 06: Trash Compactor",        "06.txt", "06.txt", "4878670269096",    ""),
    new Day07("Day 07: Laboratories",           "07.txt", "07.txt", "1543",             "40"),
    new Day08("Day 08: Playground",             "08.txt", "08.txt", "131580",           "25272"),
    new Day08("Day 08: Playground",             "08.txt", "08.txt", "131580",           "6844224"),
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
