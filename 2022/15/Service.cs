
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
            var ranges = new RangeCollection();
            var beacons = new List<int>();

            foreach (var reading in readings)
            {
                if (reading.TryGetIntersectingRange(line, out var range)) ranges.Add(range!);
                if (reading.Beacon.Y == line) beacons.Add(reading.Beacon.X);
            }

            var size = ranges.Sum(r => r.Size);
            foreach (var beacon in beacons.Distinct()) size -= ranges.Where(r => r.Intersects(beacon)).Count();

            return size;
        }
    }

    public class Problem2
    {
        public long Solve(IEnumerable<Reading> readings, int searchArea)
        {
            var ranges = Enumerable.Range(0, searchArea + 1).Select(i => new RangeCollection()).ToArray();

            foreach (var reading in readings)
            {
                var dist = reading.Signal.Distance(reading.Beacon);
                var minY = Math.Max(reading.Signal.Y - dist, 0);
                var maxY = Math.Min(reading.Signal.Y + dist, searchArea);

                for (var y = minY; y <= maxY; y++)
                {
                    if (reading.TryGetIntersectingRange(y, out var range))
                    {
                        if (range!.Max < 0) continue;
                        if (range!.Min > searchArea) continue;
                        ranges[y].Add(Math.Max(range!.Min, 0), Math.Min(range!.Max, searchArea));
                    }
                }
            }

            var beaconY = ranges!.TakeWhile(rs => rs.Sum(r => r.Size) != searchArea).Count();
            var beaconX = 0;
            foreach (var range in ranges[beaconY])
            {
                if (range.Intersects(beaconX)) beaconX = range.Max + 1;
            }

            return beaconX * (long)4000000 + beaconY;
        }
    }
}