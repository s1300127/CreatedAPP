

using System.Collections.Generic;
using Avalonia.Controls;
using AvaloniaApplication5.Controls;
using AvaloniaApplication5.Models;

namespace AvaloniaApplication5.Services
{
    public class GraphRenderer
    {
        private readonly Canvas _canvas;
        private readonly Dictionary<Node,NodeControl> _nodeControls = new ();
        private readonly List<EdgeControl> _edgeControls = new();

        public GraphRenderer(Canvas canvas)
        {
            _canvas = canvas;
        }

        public void AddNodes(IEnumerable<Node> nodes)
        {
            foreach(var node in nodes)
            {
                var nc = NodeFactory.Create(node, node.Name);
                _nodeControls[node] = nc;
                _canvas.Children.Add(nc);
            }
        }

        public void AddEdges(IEnumerable<Edge> edges)
        {
            foreach(var edge in edges)
            {
                var ec = EdgeFactory.Create(edge);
                _edgeControls.Add(ec);
                _canvas.Children.Add(ec);

                _nodeControls[edge.From].PositionChanged += ec.UpdatePosition;
                _nodeControls[edge.To].PositionChanged += ec.UpdatePosition;
            }
        }
    }
}