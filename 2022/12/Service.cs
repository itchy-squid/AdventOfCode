namespace Advent22.Day11
{
    class ProblemSolver
    {
        private Graph<Elevation> _graph;
        private Dictionary<int, int> _distances;
        private int _start;
        private int _end;

        public ProblemSolver(string[] lines)
        {
            var wid = lines[0].Length;
            var len = lines.Length;
            _graph = new Graph<Elevation>(wid, len);
            var start = (Node<Elevation>?)null;
            var end = (Node<Elevation>?)null;

            foreach (var y in Enumerable.Range(0, len))
                foreach (var x in Enumerable.Range(0, wid))
                {
                    _graph.AddNode(x, y, Deserialize(lines[y][x]));
                    if (_graph[x, y].Value.IsStart) start = _graph[x, y];
                    if (_graph[x, y].Value.IsEnd) end = _graph[x, y];
                }

            foreach (var y in Enumerable.Range(0, len))
                foreach (var x in Enumerable.Range(0, wid))
                {
                    if (x > 0) TryAddEdge(_graph[x, y], _graph[x - 1, y]);
                    if (y > 0) TryAddEdge(_graph[x, y], _graph[x, y - 1]);
                    if (y < len - 1) TryAddEdge(_graph[x, y], _graph[x, y + 1]);
                    if (x < wid - 1) TryAddEdge(_graph[x, y], _graph[x + 1, y]);
                }

            (_distances, _) = _graph!.ShortestPath(end!);
            _start = start!.Id;
            _end = end!.Id;
        }

        public int Solve1()
        {
            return _distances![_start];
        }

        public int Solve2()
        {
            var startingPoints = _graph.Where(n => n.Value.Height == 1).Select(n => n.Id);
            return _distances.Where(kpv => startingPoints.Contains(kpv.Key)).Select(kvp => kvp.Value).Min();
        }

        private void TryAddEdge(Node<Elevation> a, Node<Elevation> b)
        {
            if (b.Value.Height - a.Value.Height <= 1) b.Neighbors.Add(a);
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
    }
}