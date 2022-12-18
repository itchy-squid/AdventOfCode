using System.Text.RegularExpressions;
using _15;

var LINE = args.Length > 1 ? Int32.Parse(args[1]) : 10;
var FILE = args.Any() ? args[0] : "file.txt";

var calcDist = (int x1, int y1, int x2, int y2) => Math.Abs(x2 - x1) + Math.Abs(y2 - y1);

var lines = await File.ReadAllLinesAsync(FILE);
var ranges = new RangeCollection();
var beacons = new List<int>();

var pattern = @"-?\d+";

foreach (var line in lines)
{
    var pt = Regex.Matches(line, pattern).Where(m => m.Success).Select(m => Int32.Parse(m.Value)).ToArray();
    var dist = calcDist(pt[0], pt[1], pt[2], pt[3]);
    var delta = dist - Math.Abs(pt[1] - LINE);

    if (delta >= 0) ranges.Add(pt[0] - delta, pt[0] + delta);
    if (pt[3] == LINE) beacons.Add(pt[2]);
}

var size = ranges.Sum(r => r.Size);
foreach (var beacon in beacons.Distinct()) size -= ranges.Where(r => r.Intersects(beacon)).Count();

Console.WriteLine(size);



