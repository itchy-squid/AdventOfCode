namespace AdventOfCode.Day14
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var file = args.Length == 0 ? "test.txt" : args[0];
            var lines = await File.ReadAllLinesAsync(file);
            var model = new Parser().BuildModel(lines);
            var answer = new Solver().Solve(model);
            Console.WriteLine(answer);
        }
    }
}