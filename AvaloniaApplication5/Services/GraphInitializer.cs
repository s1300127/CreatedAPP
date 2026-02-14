using System.Collections.Generic;
using System.Linq;
using AvaloniaApplication5.Models;

namespace AvaloniaApplication5.Services
{
    public static class GraphInitializer
    {
        public static (List<Node>, List<Edge>) CreateTestGraph(double width, double height)
        {
            var nodes = new List<Node>();
            var edges = new List<Edge>();

            for(int i=0;i<10;i++)nodes.Add(new Node());

            for(int i=0;i<9;i++)edges.Add(new Edge(nodes[i],nodes[i+1]));

            SetNodePosition.SetRandomPosition(nodes, width, height);
            SetNodePosition.SetPosition(nodes, edges, width, height, 30000);

            return (nodes, edges);
        }

        public static (List<Node>, List<Edge>) Initialize(double width, double height, ParsedGraph parsed)
        {
            var nodes = parsed.Nodes;
            var edges = parsed.Edges;

            SetNodePosition.SetRandomPosition(nodes, width, height);
            SetNodePosition.SetPosition(nodes, edges, width, height, 30000);

            return (nodes, edges);
        }
    }
}