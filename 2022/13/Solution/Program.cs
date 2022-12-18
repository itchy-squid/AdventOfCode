namespace AdventOfCode.Day13
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var lines = await File.ReadAllLinesAsync(args.Length > 0 ? args[0] : "test.txt");
            var model = Parser.BuildModel(lines).ToArray();
            var answer = new Solver().Solve(model);
            Console.WriteLine(answer);
        }
    }
}