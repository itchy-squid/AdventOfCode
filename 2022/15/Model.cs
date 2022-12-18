using System.Collections;
using System.Text;

namespace _15
{
    public static class ManhattanExtensions
    {
        public static int Distance(this Point p1, Point p2) => Math.Abs(p2.X - p1.X) + Math.Abs(p2.Y - p1.Y);
    }

    public record Range
    {
        public Range(int min, int max)
        {
            this.Min = min;
            this.Max = max;
        }

        public int Min { get; init; }
        public int Max { get; init; }
        public int Size => Max - Min + 1;
        public bool Intersects(int n) => Min <= n && n <= Max;
    }

    public class RangeCollection : IEnumerable<Range>
    {
        private List<Range> _ranges = new List<Range>();

        public void Add(int min, int max)
        {
            var startIdx = _ranges.FindIndex(r => r.Min >= min || r.Max >= min);
            var endIdx = _ranges.FindLastIndex(r => r.Min <= max || r.Max <= max);

            var rangeStart = startIdx < 0 ? min : Math.Min(_ranges[startIdx].Min, min);
            var rangeEnd = endIdx < 0 ? max : Math.Max(_ranges[endIdx].Max, max);
            var newRange = new Range(rangeStart, rangeEnd);

            if (startIdx < 0) _ranges.Add(newRange);
            else
            {
                for (var i = endIdx; i >= startIdx; i--) _ranges.RemoveAt(i);
                _ranges.Insert(startIdx, newRange);
            }
        }

        public IEnumerator<Range> GetEnumerator() => _ranges.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_ranges).GetEnumerator();
    }

    public record Point
    {
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; init; }
        public int Y { get; init; }
    }

    public record Reading
    {
        public Reading(Point signal, Point beacon)
        {
            Beacon = beacon;
            Signal = signal;
        }

        public Point Signal { get; init; }

        public Point Beacon { get; init; }
    }

    public static class RangeExtensions
    {
        public static string Dump(this IEnumerable<Range> ranges)
        {
            var builder = new StringBuilder();

            foreach (var range in ranges)
            {
                builder = builder.Append($"[{range.Min},{range.Max}] ");
            }

            return builder.ToString();
        }
    }
}