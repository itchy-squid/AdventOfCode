namespace _2022._17
{

    //  y|
    //   |
    //   |
    //  0+-------
    //   0      x

    public enum Direction
    {
        Right,
        Left,
        Down
    }

    public record Point
    {
        public int X { get; init; }
        public int Y { get; init; }
    }

    public record Rectangle
    {
        public Rectangle(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            Width = width;
            Height = height;
        }

        public int X { get; init; }
        public int Y { get; init; }
        public int Width { get; init; }
        public int Height { get; init; }

        public bool Overlaps(Rectangle target)
        {
            return !((target.X + target.Width < X)
                || (target.X > X + Width)
                || (target.Y + target.Height < Y)
                || (target.Y > Y + Height));
        }
    }

    public record Shape
    {
        public int Width { get; private init; }
        public int Height { get; private init; }
        private bool[,] _squares;

        public Shape(int width, int height, bool[,] squares)
        {
            Width = width;
            Height = height;
            _squares = squares;
        }

        public bool IsAt(int localX, int localY)
        {
            return _squares[localX, localY];
        }
    }

    public class Rock
    {
        public Rock(int x, int y, Shape shape)
        {
            _x = x;
            _y = y;
            _shape = shape;
        }

        private int _x;
        private int _y;
        private readonly Shape _shape;

        public bool CanMove(Direction direction, IEnumerable<Rock> rocks)
        {
            var x = _x;
            var y = _y;

            switch (direction)
            {
                case Direction.Left: x = Math.Max(0, x - 1); break;
                case Direction.Right: x = Math.Min(7 - _shape.Width, x + 1); break;
                case Direction.Down: y = Math.Min(0, y - 1); break;
                default: throw new NotSupportedException();
            }

            if (_x == x && _y == y) return false;
            return !rocks.Any(r => this.Collides(r));
        }

        public Rectangle BoundingBox => new Rectangle(_x, _y, _shape.Width, _shape.Height);

        public bool Collides(Rock target)
        {
            var s = this.BoundingBox;
            var t = target.BoundingBox;

            if (!s.Overlaps(t)) return false;

            for (var x = Math.Max(s.X, t.X); x <= Math.Min(s.X + s.Width, t.X + t.Width); x++)
            {
                for (var y = Math.Max(s.Y, t.Y); y <= Math.Min(s.Y + s.Height, t.Y + t.Height); y++)
                {
                    if (this.Collides(x, y) && target.Collides(x, y)) return true;
                }
            }

            return false;
        }

        private bool Collides(int x, int y) => this._shape.IsAt(x - this._x, y - this._y);
    }

    public class Model
    {
        private List<Rock> _rocks = new List<Rock>();
    }
}