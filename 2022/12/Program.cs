namespace Advent22.Day11
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var lines = await File.ReadAllLinesAsync(args[0]);
            var answer = new Problem1().Solve(lines!);
            Console.WriteLine(answer);
        }
    }
}