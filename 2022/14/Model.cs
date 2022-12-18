namespace AdventOfCode.Day14
{
    public class FakoGrid
    {
        private HashSet<string> _model = new HashSet<string>();

        public void Add(int x, int y)
        {
            _model.Add($"{x},{y}");
        }

        public bool Contains(int x, int y)
        {
            return _model.Contains($"{x},{y}");
        }
    }

    public class Simulation
    {
        public int Left { get; private set; }
        public int Top { get; private set; }
        public int Right { get; private set; }
        public int Bottom { get; private set; }

        private FakoGrid _rocks;
        private FakoGrid _sand;

        public Simulation()
        {
            Left = Int32.MaxValue;
            Top = Int32.MaxValue;
            Right = Int32.MinValue;
            Bottom = Int32.MinValue;

            _rocks = new FakoGrid();
            _sand = new FakoGrid();
        }

        public void AddRock(int x1, int y1, int x2, int y2)
        {
            Left = new[] { x1, x2, Left }.Min();
            Top = new[] { y1, y2, Top }.Min();
            Right = new[] { x1, x2, Right }.Max();
            Bottom = new[] { y1, y2, Bottom }.Max();

            var deltaX = Math.Sign(x2 - x1);
            var deltaY = Math.Sign(y2 - y1);

            int x = x1, y = y1;
            while (true)
            {
                if (!_rocks.Contains(x, y)) _rocks.Add(x, y);
                if (x == x2 && y == y2) break;
                x += deltaX;
                y += deltaY;
            }
        }

        // public void AddInfiniteBottom(int delta)
        // {
        //     this.InfiniteBottom = Bottom + delta;
        // }

        public bool TryAddSand(int x, int y)
        {
            if (Collides(x, y)) return false;

            while (true)
            {
                if (x < Left) return false;
                if (x > Right) return false;
                if (y > Bottom) return false;

                if (!Collides(x, y + 1)) y = y + 1;
                else if (!Collides(x - 1, y + 1)) x = x - 1;
                else if (!Collides(x + 1, y + 1)) x = x + 1;
                else
                {
                    Left = Math.Min(x, Left);
                    Right = Math.Max(x, Right);
                    Top = Math.Min(y, Top);
                    _sand.Add(x, y);
                    return true;
                }
            }
        }

        public bool IsSand(int x, int y)
        {
            return _sand.Contains(x, y);
        }

        public bool IsRock(int x, int y)
        {
            return _rocks.Contains(x, y);
        }

        public bool Collides(int x, int y)
        {
            return IsRock(x, y) || IsSand(x, y);
        }
    }
}