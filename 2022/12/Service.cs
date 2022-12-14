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
            var wid = lines[0].Length;
            var len = lines.Length;
            var graph = new Graph<Elevation>(wid, len);
            var start = (Node<Elevation>?)null;
            var end = (Node<Elevation>?)null;

            foreach (var y in Enumerable.Range(0, len))
                foreach (var x in Enumerable.Range(0, wid))
                {
                    graph.AddNode(x, y, Deserialize(lines[y][x]));
                    start = (graph[x, y].Value.IsStart) ? graph[x, y] : start;
                    end = (graph[x, y].Value.IsEnd) ? graph[x, y] : end;
                }

            foreach (var y in Enumerable.Range(0, len))
                foreach (var x in Enumerable.Range(0, wid))
                {
                    if (x > 0) TryAddEdge(graph[x, y], graph[x - 1, y]);
                    if (y > 0) TryAddEdge(graph[x, y], graph[x, y - 1]);
                    if (y < len - 1) TryAddEdge(graph[x, y], graph[x, y + 1]);
                    if (x < wid - 1) TryAddEdge(graph[x, y], graph[x + 1, y]);
                }

            var (dist, path) = graph.ShortestPath(start!, end!);
            return dist;
        }

        private void TryAddEdge(Node<Elevation> a, Node<Elevation> b)
        {
            if (b.Value.Height - a.Value.Height <= 1) a.Neighbors.Add(b);
        }

        private static Elevation Deserialize(char c)
        {
            var heightFrom = (char c) => c - 'a' + 1;

            switch (c)
            {
                case 'S': return new Elevation { Height = heightFrom('a'), IsStart = true, IsEnd = false };
                case 'E': return new Elevation { Height = heightFrom('z'), IsStart = false, IsEnd = true };
                default: return new Elevation { Height = heightFrom(c), IsStart = false, IsEnd = false };
            }
        }

        private void Print(Graph<Elevation> g, Func<Node<Elevation>, bool> test)
        {
            foreach (var y in Enumerable.Range(0, g.Height))
            {
                foreach (var x in Enumerable.Range(0, g.Width))
                {
                    Console.Write(test(g[x, y]) ? "X" : ".");
                }
                Console.WriteLine();
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

        private int ToId(int x, int y, int width)
        {
            return y * width + x;
        }
    }
}