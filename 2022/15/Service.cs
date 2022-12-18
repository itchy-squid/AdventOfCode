
using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace _15
{

    public class Parser
    {
        public static IEnumerable<Reading> BuildModel(string[] lines)
        {
            var pattern = @"-?\d+";
            var readings = ImmutableList<Reading>.Empty;

            foreach (var line in lines)
            {
                var pt = Regex.Matches(line, pattern).Where(m => m.Success).Select(m => Int32.Parse(m.Value)).ToArray();
                readings = readings.Add(new Reading(new Point(pt[0], pt[1]), new Point(pt[2], pt[3])));
            }

            return readings;
        }
    }

    public class Problem1
    {
        public int Solve(IEnumerable<Reading> readings, int line)
        {
            var calcDist = (int x1, int y1, int x2, int y2) => Math.Abs(x2 - x1) + Math.Abs(y2 - y1);

            var ranges = new RangeCollection();
            var beacons = new List<int>();

            foreach (var reading in readings)
            {
                var dist = reading.Signal.Distance(reading.Beacon);
                var delta = dist - Math.Abs(reading.Signal.Y - line);

                if (delta >= 0) ranges.Add(reading.Signal.X - delta, reading.Signal.X + delta);
                if (reading.Beacon.Y == line) beacons.Add(reading.Beacon.X);
            }

            var size = ranges.Sum(r => r.Size);
            foreach (var beacon in beacons.Distinct()) size -= ranges.Where(r => r.Intersects(beacon)).Count();

            return size;
        }
    }

    public class Problem2
    {
        public int Solve(IEnumerable<Reading> readings, int searchArea)
        {
            return 0;
        }
    }
}