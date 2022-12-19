namespace AdventOfCode.Day14
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var file = args.Length == 0 ? "test.txt" : args[0];
            var lines = await File.ReadAllLinesAsync(file);
            Console.WriteLine(new Solver().Problem1(new Parser().BuildModel(lines)));
            Console.WriteLine(new Solver().Problem2(new Parser().BuildModel(lines)));
        }
    }
}