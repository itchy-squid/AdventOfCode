namespace AdventOfCode.Day14
{
    public class Parser
    {
        public Simulation BuildModel(IEnumerable<string> lines)
        {
            var model = new Simulation();
            foreach (var l in lines)
            {
                UpdateModel(model, l);
            }

            return model;
        }

        private void UpdateModel(Simulation m, string line)
        {
            var positions = line.Split(" -> ").Select(l => l.Split(',').Select(n => int.Parse(n)).ToArray()).ToList();
            var enumerator = positions.GetEnumerator();

            enumerator.MoveNext();
            var last = enumerator.Current;

            while (enumerator.MoveNext())
            {
                m.AddRock(last[0], last[1], enumerator.Current[0], enumerator.Current[1]);
                last = enumerator.Current;
            }
        }
    }

    public class Solver
    {
        public int Solve(Simulation s)
        {
            int i;
            for (i = 0; s.TryAddSand(500, 0); i++)
            {
                // new Renderer().Render(s);
            }

            return i;
        }
    }

    public class Renderer
    {
        public void Render(Simulation model)
        {
            try
            {
                Console.SetCursorPosition(0, 0);
            }
            catch
            {
            }

            for (var y = model.Top; y <= model.Bottom; y++)
            {
                for (var x = model.Left; x <= model.Right; x++)
                {
                    var c = model.IsRock(x, y) ? '#' : model.IsSand(x, y) ? 'o' : '.';
                    Console.Write(c);
                }
                Console.WriteLine();
            }
        }
    }
}