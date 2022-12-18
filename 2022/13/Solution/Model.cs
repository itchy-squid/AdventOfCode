using System.Text;

public record Model
{
    public ListNode Left { get; init; }
    public ListNode Right { get; init; }

    public Model(ListNode left, ListNode right)
    {
        Left = left;
        Right = right;
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