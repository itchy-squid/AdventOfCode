using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

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

    public class NodeComparer<T> : IEqualityComparer<Node<T>>
    {
        public bool Equals(Node<T>? x, Node<T>? y)
        {
            return x?.Id == y?.Id;
        }

        public int GetHashCode([DisallowNull] Node<T> obj)
        {
            return obj.Id;
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
            this[x, y] = new Node<T>(ToId(x, y), value);
        }

        public (Dictionary<int, int>, Dictionary<int, int>) ShortestPath(Node<T> source)
        {
            var distances = new Dictionary<int, int>();
            var previous = new Dictionary<int, int>();
            var toVisit = new HashSet<Node<T>>();

            foreach (var n in Nodes)
            {
                distances[n.Id] = int.MaxValue;
                previous[n.Id] = -1;
                toVisit.Add(n);
            }
            distances[source.Id] = 0;

            while (toVisit.Any())
            {
                var u = toVisit.MinBy(n => distances[n.Id])!;
                toVisit.Remove(u);

                if (distances[u.Id] == int.MaxValue || distances[u.Id] < 0)
                {
                    var output = new StringBuilder();
                    output = output.AppendLine($"Visiting unreachable node ({ToX(u.Id)},{ToY(u.Id)}) with d={distances[u.Id]}");
                    foreach (var n in u.Neighbors) output = output.AppendLine($"  neighbor at ({ToX(n.Id)},{ToY(n.Id)}) with d={distances[n.Id]}");
                    break;
                }

                var neighbors = toVisit.Intersect(u.Neighbors, new NodeComparer<T>()).ToList();

                Print(u, neighbors, distances);

                foreach (var v in neighbors)
                {
                    var alt = distances[u.Id] + 1;
                    if (alt < distances[v.Id])
                    {
                        distances[v.Id] = alt;
                        previous[v.Id] = u.Id;
                    }
                }
            }

            return (distances, previous);
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

        public void Print(Node<T> curr, List<Node<T>> updating, Dictionary<int, int> distances)
        {
            var gradient = new[] { '$', 'X', 'x', '=', '+', ';', ':', ',', '.', ' ' };

            var output = new StringBuilder();

            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    var equivalentId = ToId(x, y);
                    var hit = updating.Append(curr).Any(n => n.Id == equivalentId);
                    var d = distances[ToId(x, y)];
                    var c = gradient[Math.Min(gradient.Length - 1, d / 57)];

                    output = output.Append(hit ? "X" : c);
                }
                output = output.AppendLine();
            }

            try
            {
                Console.SetCursorPosition(0, 0);
                Console.Write(output.ToString());
            }
            catch
            {
            }
        }

        private int ToId(int x, int y)
        {
            return y * Width + x;
        }

        private int ToX(int id)
        {
            return id % Width;
        }

        private int ToY(int id)
        {
            return (id - ToX(id)) / Width;
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