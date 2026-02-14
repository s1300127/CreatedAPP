using System.Collections.Generic;
using System.Linq;

namespace AvaloniaApplication5.Models{
    public class ParsedGraph
    {
        public List<Node> Nodes{get; set;}
        public List<Edge> Edges{get; set;}

        public ParsedGraph(Node[] nodes, Edge[] edges)
        {
            Nodes = nodes.ToList();
            Edges = edges.ToList();
        }
    }
}