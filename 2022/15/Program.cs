using System.Text.RegularExpressions;
using _15;

var LINE = 2000000;
var FILE = "input.txt";

var calculateDistance = (int x1, int y1, int x2, int y2) => Math.Abs(x2 - x1) + Math.Abs(y2 - y1);

var lines = await File.ReadAllLinesAsync(FILE);
var ranges = new RangeCollection();
var beacons = new List<int>();

var pattern = @"-?\d+";

foreach (var line in lines)
{
    var pt = Regex.Matches(line, pattern).Where(m => m.Success).Select(m => Int32.Parse(m.Value)).ToArray();
    var dist = calculateDistance(pt[0], pt[1], pt[2], pt[3]);
    Console.WriteLine($"s:({pt[0]},{pt[1]})  \tb:({pt[2]},{pt[3]})  \td:{dist}");

    var delta = dist - Math.Abs(pt[1] - LINE);

    if (delta >= 0)
    {
        Console.WriteLine($"Adding range [{pt[0] - delta},{pt[0] + delta}]");
        ranges.Add(pt[0] - delta, pt[0] + delta);
        Console.WriteLine(ranges.Dump());
        Console.WriteLine();
    }

    if (pt[3] == LINE) beacons.Add(pt[2]);
}

var size = 0;
foreach (var range in ranges)
{
    size += range.Size;
}

foreach (var beacon in beacons.Distinct())
{
    foreach (var range in ranges)
    {
        if (range.Intersects(beacon)) size -= 1;
    }
}

Console.WriteLine(ranges.Dump());
Console.WriteLine(size);



