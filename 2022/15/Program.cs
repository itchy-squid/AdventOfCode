using _15;

var modes = new Dictionary<string, Configuration>() { { "test", Configurations.Test }, { "input", Configurations.Input } };
var mode = args.Any() ? modes[args[0]] : Configurations.Test;

var lines = await File.ReadAllLinesAsync(mode.Filename);
var model = Parser.BuildModel(lines);

Console.WriteLine(new Problem1().Solve(model, mode.Line));
Console.WriteLine(new Problem2().Solve(model, mode.Search));

public partial class Program
{
    public record Configuration
    {
        public string Filename { get; init; } = null!;
        public int Line { get; init; }
        public int Search { get; init; }
    }

    public static class Configurations
    {
        public static Configuration Test = new Configuration() { Filename = "test.txt", Line = 10, Search = 20 };
        public static Configuration Input = new Configuration() { Filename = "input.txt", Line = 2000000, Search = 4000000 };
    }
}



