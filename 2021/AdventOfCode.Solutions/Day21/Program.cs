using AdventOfCode.Solutions.DataStructures;
using AdventOfCode.Solutions.Tools;
using System.Collections.Immutable;
using System.Text;

namespace AdventOfCode.Solutions.Day21
{
    public class Program
    {
        public static void Main()
        {
            var input = Input.ReadAllLines("Day21");

            var result = Solve(input);
            Console.WriteLine(result);
            Console.WriteLine();

            //var result2 = Solve(input, 50);
            //Console.WriteLine(result2);
            //Console.WriteLine();
        }

        public static int Solve(IEnumerable<string> lines)
        {
            var die = new Die();
            var players = new Player[] { new(2), new(1) };

            for(var i = 0; players[(i + 1) % players.Length].Score < 1000; i = (i + 1) % players.Length)
            {
                players[i % players.Length].Move(die);
            }

            return die.NRolls * players.Where(p => p.Score < 1000).First().Score;
        }
    }

    public class Die
    {
        private int _next;

        public int NRolls => _next;

        public int Roll()
        {
            return ++_next;
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

        public void Move(Die die)
        {
            Position += Enumerable.Range(0, 3).Select(i => die.Roll()).Sum();
            Position %= 10;
            Score += (Position == 0) ? 10 : Position;
        }
    }
}
