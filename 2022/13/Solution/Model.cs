using System.Text;

public record Model
{
    public INode Left { get; init; }
    public INode Right { get; init; }

    public Model(INode left, INode right)
    {
        Left = left;
        Right = right;
    }

    public bool IsOrdered()
    {
        //         If both values are integers, the lower integer should come first. If the left integer is lower than the right integer, 
        // the inputs are in the right order. If the left integer is higher than the right integer, the inputs are not in the right order. 
        // Otherwise, the inputs are the same integer; continue checking the next part of the input.

        // If both values are lists, compare the first value of each list, then the second value, and so on. If the left list runs out of 
        // items first, the inputs are in the right order. If the right list runs out of items first, the inputs are not in the right order. 
        // If the lists are the same length and no comparison makes a decision about the order, continue checking the next part of the input.

        // If exactly one value is an integer, convert the integer to a list which contains that integer as its only value, then retry the 
        // comparison. For example, if comparing [0,0,0] and 2, convert the right value to [2] (a list containing 2); the result is then 
        // found by instead comparing [0,0,0] and [2].

        throw new NotImplementedException();
    }
}

public interface INode
{
}

public record IntNode : INode
{
    public int Value { get; private init; }

    public IntNode(int n)
    {
        Value = n;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}

public record ListNode : INode
{
    public List<INode> Values { get; private init; }

    public ListNode()
    {
        Values = new List<INode>();
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.Append('[');
        for (var i = 0; i < Values.Count; i++)
        {
            builder.Append(Values[i]);
            if (i < Values.Count - 1) builder.Append(',');
        }
        builder.Append(']');
        return builder.ToString();
    }
}