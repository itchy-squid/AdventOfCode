namespace AdventOfCode.Day13
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var lines = await File.ReadAllLinesAsync(args.Length > 0 ? args[0] : "test.txt");
            var model = Parser.BuildModel(lines);

            var answer1 = new Problem1().Solve(model);
            Console.WriteLine(answer1);

            var answer2 = new Problem2().Solve(model);
            Console.WriteLine(answer2);
        }
    }
}