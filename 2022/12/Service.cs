namespace Advent22.Day11
{
    interface ISolver<T>
    {
        T Solve(string[] lines);
    }

    class Problem1 : ISolver<int>
    {
        public int Solve(string[] lines)
        {
            var width = lines[0].Length;
            var length = lines.Length;
            var graph = new Graph<Elevation>(width, length);
            var start = (Node<Elevation>?)null;
            var end = (Node<Elevation>?)null;

            foreach (var y in Enumerable.Range(0, length))
                foreach (var x in Enumerable.Range(0, width))
                {
                    var node = new Node<Elevation>(Deserialize(y * width + x, lines[y][x]));
                    graph[x, y] = node;
                    Console.WriteLine(node.Value.Id);

                    start = (graph[x, y].Value.IsStart) ? graph[x, y] : start;
                    end = (graph[x, y].Value.IsEnd) ? graph[x, y] : end;
                    if (x > 0) TryAddEdge(graph, graph[x - 1, y], graph[x, y]);
                    if (y > 0) TryAddEdge(graph, graph[x, y - 1], graph[x, y]);
                }

            var path = graph.ShortestPath(start!, end!).ToList();


            Console.WriteLine(path.Select(n => n.Value.Id.ToString()).Aggregate((a, b) => a != null ? (b != null ? $"{a} {b}" : $"{a}") : $"{b}"));

            foreach (var y in Enumerable.Range(0, length))
            {
                foreach (var x in Enumerable.Range(0, width))
                {
                    Console.Write(path.Any(n => n.Value.Id == (y * width + x)) ? "X" : ".");
                }
                Console.WriteLine();
            }

            return path.Count;
        }

        private void TryAddEdge(Graph<Elevation> g, Node<Elevation> a, Node<Elevation> b)
        {
            if (a.Value.IsReachable(b.Value)) g.AddEdge(a, b);
        }

        private static Elevation Deserialize(int id, char c)
        {
            switch (c)
            {
                case 'S': return new Elevation { Id = id, Height = 1, IsStart = true };
                case 'E': return new Elevation { Id = id, Height = 26, IsEnd = true };
                default: return new Elevation { Id = id, Height = c - 'a' + 1 };
            }
        }

        private void Print(Graph<Elevation> graph)
        {
            foreach (var y in Enumerable.Range(0, graph.Height))
            {
                foreach (var x in Enumerable.Range(0, graph.Width))
                {
                    Console.Write(graph[x, y].Value);
                }
                Console.WriteLine();
            }
        }
    }
}