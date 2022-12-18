namespace AdventOfCode.Day13
{
    public class Parser
    {
        public static IEnumerable<ListNode> BuildModel(string[] lines)
        {
            for (int i = 0; i < lines.Length; i += 3)
            {
                yield return BuildModel(lines[i]);
                yield return BuildModel(lines[i + 1]);
            }
        }

        public static ListNode BuildModel(string line)
        {
            var root = new ListNode();
            var curr = root;

            var history = new Stack<ListNode?>();
            history.Push(null);

            for (var i = 0; i < line.Length; i++)
            {
                switch (line[i])
                {
                    case '[':
                        history.Push(curr);
                        curr = new ListNode();
                        history.Peek()!.Values.Add(curr);
                        break;

                    case ',': break;

                    case ']':
                        curr = history.Pop();
                        break;

                    default:
                        var value = new String(line.Substring(i).TakeWhile(c => char.IsDigit(c)).ToArray());
                        curr!.Values.Add(new IntNode(Int32.Parse(value)));
                        break;
                }
            }

            return (root.Values[0] as ListNode)!;
        }
    }

    public class Solver
    {
        public int Solve(IEnumerable<ListNode> models)
        {
            var sum = 0;
            var c = new NodeComparer();
            var enumerator = models.GetEnumerator();
            for (var i = 0; enumerator.MoveNext(); i++)
            {
                var left = enumerator.Current;
                enumerator.MoveNext();
                var right = enumerator.Current;

                if (-1 == c.Compare(left, right)) sum += (i + 1);
            }

            return sum;
        }
    }

    public class NodeComparer : IComparer<INode>
    {

        public int Compare(INode? left, INode? right)
        {
            if (right == null) return left == null ? 0 : 1;
            if (left == null) return -1;

            // If both values are integers, the lower integer should come first. If the left integer is lower than the right integer, the inputs 
            // are in the right order. If the left integer is higher than the right integer, the inputs are not in the right order. Otherwise, the 
            // inputs are the same integer; continue checking the next part of the input.
            if (left is IntNode lInt && right is IntNode rInt)
            {
                return Math.Sign(lInt.Value - rInt.Value);
            }

            // If both values are lists, compare the first value of each list, then the second value, and so on. If the left list runs out of items 
            // first, the inputs are in the right order. If the right list runs out of items first, the inputs are not in the right order. If the lists 
            // are the same length and no comparison makes a decision about the order, continue checking the next part of the input.
            else if (left is ListNode lList && right is ListNode rList)
            {
                for (var i = 0; i < lList.Values.Count; i++)
                {
                    if (i >= rList.Values.Count) return 1;
                    var comparison = Compare(lList.Values[i], rList.Values[i]);
                    if (comparison != 0) return comparison;
                }

                return Math.Sign(lList.Values.Count - rList.Values.Count);
            }

            // If exactly one value is an integer, convert the integer to a list which contains that integer as its only value, then retry the comparison.
            // For example, if comparing [0,0,0] and 2, convert the right value to [2] (a list containing 2); the result is then found by instead 
            // comparing [0,0,0] and [2].
            else if (left is IntNode leftI && right is ListNode rightL)
            {
                var temp = new ListNode();
                temp.Values.Add(leftI);
                return Compare(temp, rightL);
            }

            else if (right is IntNode rightI && left is ListNode leftL)
            {
                var temp = new ListNode();
                temp.Values.Add(rightI);
                return Compare(left, temp);
            }

            throw new InvalidOperationException($"{left}\r\n{right}\r\n");
        }
    }
}