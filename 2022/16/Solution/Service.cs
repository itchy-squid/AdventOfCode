namespace AdventOfCode.Day13
{
    public class Parser
    {
        public static Model BuildModel(string[] lines)
        {
            var model = new Model();
            foreach (var line in lines)
            {
                BuildModel(model, line);
            }
            return model;
        }

        public static Model BuildModel(Model m, string line)
        {
            throw new NotImplementedException();
        }
    }

    public class Problem1
    {
        public int Solve(Model model)
        {
            throw new NotImplementedException();
        }
    }

    public class Problem2
    {
        public int Solve(Model model)
        {
            throw new NotImplementedException();
        }
    }
}