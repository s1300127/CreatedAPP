
using Avalonia.Controls;
using AvaloniaApplication5.Controls;
using AvaloniaApplication5.Models;

namespace AvaloniaApplication5.Services
{
    public static class EdgeFactory
    {
        public static EdgeControl Create(Edge edge)
        {
            EdgeControl created = new EdgeControl(edge);
            created.ZIndex=1;
            return created;
        }
    }
}