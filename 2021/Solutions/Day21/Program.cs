using AdventOfCode.Solutions.Tools;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Day21
{
    public class Program
    {
        public static void Main()
        {
            var input = Input.ReadAllLines("Day21","test.txt");

            var result = Solve(input, new DeterministicDie(100));
            Console.WriteLine(result);
            Console.WriteLine();

            //var result2 = Solve(input, 50);
            //Console.WriteLine(result2);
            //Console.WriteLine();
        }

        public static int Solve(IEnumerable<string> lines, IDie die)
        {
            var regex = new Regex("Player [0-9]+ starting position: (?<position>[0-9]+)");

            var players = lines
                .Select(line => regex.Match(line).Groups["position"].Value)
                .Select(str => new Player(Int32.Parse(str)))
                .ToArray();

            var i = -1;
            do
            {
                i = (i + 1) % players.Length;
                players[i].Move(die);
                Console.WriteLine($"{i}: {players[i].Score}");

            } while (players[i].Score < 1000);

            return die.NRolls * players[(i + 1) % players.Length].Score;
        }
    }

    public interface IDie
    {
        int NRolls { get; }
        int Roll();
    }

    public class DeterministicDie : IDie
    {
        private readonly int _size;
        private int _next;

        public int NRolls => _next;

        public DeterministicDie(int size)
        {
            _size = size;
        }

        public int Roll()
        {
            _next = (_next % _size) + 1;
            return _next;
        }
    }

    public class Player
    {
        public int Position { get; private set; }
        public int Score { get; private set; }

        public Player(int start)
        {
            Score = 0;
            Position = start % 10;
        }

        public void Move(IDie die)
        {
            Position += Enumerable.Range(0, 3).Select(i => die.Roll()).Sum();
            Position %= 10;
            Score += Position == 0 ? 10 : Position;
        }
    }
}
