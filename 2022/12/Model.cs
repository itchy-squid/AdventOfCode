namespace Advent22.Day11
{

    public record Elevation
    {
        public int Id { get; init; }
        public int Height { get; init; }
        public bool IsStart { get; init; }
        public bool IsEnd { get; init; }

        public static implicit operator char(Elevation e)
        {
            return (char)('a' + e.Height - 1);
        }

        public bool IsReachable(Elevation e)
        {
            return Math.Abs(this.Height - e.Height) <= 1;
        }
    }

    public class Node<T>
    {
        public T Value { get; private init; }
        public List<Node<T>> Neighbors { get; private init; }

        public Node(T value)
        {
            Value = value;
            Neighbors = new List<Node<T>>();
        }
    }

    public class Graph<T>
    {
        private Node<T>[,] Nodes { get; init; }
        public int Width { get; init; }
        public int Height { get; init; }

        public Node<T> this[int x, int y]
        {
            get
            {
                return Nodes[x, y];
            }
            set
            {
                Nodes[x, y] = value;
            }
        }

        public Graph(int width, int height)
        {
            Width = width;
            Height = height;
            Nodes = new Node<T>[width, height];
        }

        public void AddEdge(Node<T> a, Node<T> b)
        {
            a.Neighbors.Add(b);
            b.Neighbors.Add(a);
        }

        public Stack<Node<T>> ShortestPath(Node<T> source, Node<T> target)
        {
            var distances = new Dictionary<Node<T>, int>();
            var previous = new Dictionary<Node<T>, Node<T>?>();
            var toVisit = new List<Node<T>>();

            foreach (var n in Nodes)
            {
                distances[n] = int.MaxValue;
                previous[n] = null;
                toVisit.Add(n);
            }

            distances[source] = 0;
            while (toVisit.Any())
            {
                var u = toVisit.MinBy(n => distances[n])!;
                toVisit.Remove(u);

                foreach (var v in u.Neighbors.Intersect(toVisit))
                {
                    var alt = distances[u] + 1;
                    if (alt < distances[v])
                    {
                        distances[v] = alt;
                        previous[v] = u;
                    }
                }
            }

            var path = new Stack<Node<T>>();
            for (var n = target; n != source && n != null; n = n == null ? null : previous[n])
            {
                path.Push(target!);
            }

            return path;
        }
    }
}

public record Point
{
    public int X { get; init; }
    public int Y { get; init; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}