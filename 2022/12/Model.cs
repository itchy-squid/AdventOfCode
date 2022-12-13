using System.Collections;
using System.Linq;

namespace Advent22.Day11
{

    public record Elevation
    {
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
        public int Id { get; private init; }
        public T Value { get; private init; }
        public List<Node<T>> Neighbors { get; private init; }

        public Node(int id, T value)
        {
            Id = id;
            Value = value;
            Neighbors = new List<Node<T>>();
        }
    }

    public class Graph<T> : IEnumerable<Node<T>>
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
            private set
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

        public void AddNode(int x, int y, T value)
        {
            this[x, y] = new Node<T>(y * Width + x, value);
        }

        public void AddEdge(Node<T> a, Node<T> b)
        {
            a.Neighbors.Add(b);
            b.Neighbors.Add(a);
        }

        public Stack<Node<T>> ShortestPath(Node<T> source, Node<T> target)
        {
            var distances = new Dictionary<int, int>();
            var previous = new Dictionary<int, Node<T>?>();
            var toVisit = new List<Node<T>>();

            foreach (var n in Nodes)
            {
                distances[n.Id] = int.MaxValue;
                previous[n.Id] = null;
                toVisit.Add(n);
            }

            distances[source.Id] = 0;
            while (toVisit.Any())
            {
                var u = toVisit.MinBy(n => distances[n.Id])!;
                toVisit.Remove(u);

                foreach (var v in u.Neighbors.Intersect(toVisit))
                {
                    var alt = distances[u.Id] + 1;
                    if (alt < distances[v.Id])
                    {
                        distances[v.Id] = alt;
                        previous[v.Id] = u;
                    }
                }
            }

            var path = new Stack<Node<T>>();
            for (var n = target; n != null; n = n != null ? previous[n.Id] : null)
            {
                path.Push(n!);
            }

            return path;
        }

        public IEnumerator<Node<T>> GetEnumerator()
        {
            return Enumerable.Range(0, Width)
                .SelectMany(x => Enumerable.Range(0, Height).Select((y) => (x, y)))
                .Select(pt => this[pt.Item1, pt.Item2])
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Nodes.GetEnumerator();
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