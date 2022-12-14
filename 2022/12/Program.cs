namespace Advent22.Day11
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var lines = await File.ReadAllLinesAsync(args[0]);
            var solver = new ProblemSolver(lines!);
            Console.WriteLine(solver.Solve1());
            Console.WriteLine(solver.Solve2());
        }
    }
}