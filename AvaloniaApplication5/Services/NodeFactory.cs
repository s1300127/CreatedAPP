using Avalonia.Controls;
using AvaloniaApplication5.Controls;
using AvaloniaApplication5.Models;

namespace AvaloniaApplication5.Services
{
    public static class NodeFactory
    {
        public static NodeControl Create(Node node, string name)
        {
            NodeControl created = new NodeControl(node);
            created.ZIndex=2;
            return created;
        }
    }
}