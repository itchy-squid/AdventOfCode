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
                    graph.AddNode(x, y, Deserialize(lines[y][x]));
                    start = (graph[x, y].Value.IsStart) ? graph[x, y] : start;
                    end = (graph[x, y].Value.IsEnd) ? graph[x, y] : end;

                    if (y > 0) TryAddEdge(graph, graph[x, y - 1], graph[x, y]);
                    if (x > 0) TryAddEdge(graph, graph[x - 1, y], graph[x, y]);
                }


            Console.WriteLine($"width: {width}");
            foreach (var v in graph.First(n => n.Id == 3464).Neighbors)
            {
                Console.WriteLine($"neighbor at {v.Id}");
            }
            // Print(graph, (_, _, n) => n.Value.IsStart);

            var path = graph.ShortestPath(start!, end!).ToList();

            Console.WriteLine(path.Select(n => n.Id.ToString()).Aggregate((a, b) => a != null ? (b != null ? $"{a} {b}" : $"{a}") : $"{b}"));

            Print(graph, n => path.Any(pn => n.Id == pn.Id));

            return path.Count - 1;
        }

        private void TryAddEdge(Graph<Elevation> g, Node<Elevation> a, Node<Elevation> b)
        {
            if (a.Value.IsReachable(b.Value)) g.AddEdge(a, b);
        }

        private static Elevation Deserialize(char c)
        {
            switch (c)
            {
                case 'S': return new Elevation { Height = 1, IsStart = true, IsEnd = false };
                case 'E': return new Elevation { Height = 26, IsStart = false, IsEnd = true };
                default: return new Elevation { Height = c - 'a' + 1, IsStart = false, IsEnd = false };
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
    }
}