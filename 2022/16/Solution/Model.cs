using System.Text;

public record Model
{
    private Dictionary<string, Node> _nodesByName;

    public Model()
    {
        _nodesByName = new Dictionary<string, Node>();
    }

    public void AddValve(string name, int flow)
    {
        Node(name).Flow = flow;
    }

    private Node Node(string name)
    {
        var node = _nodesByName[name];
        if (_nodesByName[name] == null)
        {
            node = new Node(name);
        }

        return node;
    }

    public void AddTunnel(string name, string target)
    {
        var sourceNode = Node(name);
        var targetNode = Node(target);

        sourceNode.Tunnels.Add(targetNode);
        targetNode.Tunnels.Add(sourceNode);
    }
}

public record Node
{
    public string Name { get; private init; }
    public int? Flow { get; set; }
    public List<Node> Tunnels { get; private init; }

    public Node(string name)
    {
        Name = name;
        Tunnels = new List<Node>();
    }

    // public override string ToString()
    // {
    //     var builder = new StringBuilder();
    //     builder.Append('[');
    //     for (var i = 0; i < Tunnels.Count; i++)
    //     {
    //         builder.Append(Tunnels[i]);
    //         if (i < Tunnels.Count - 1) builder.Append(',');
    //     }
    //     builder.Append(']');
    //     return builder.ToString();
    // }
}