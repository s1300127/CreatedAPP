using Avalonia.Controls.Shapes;
using Avalonia.Media;
using AvaloniaApplication5.Models;

namespace AvaloniaApplication5.Controls
{
    public class EdgeControl : Line
    {
        public Edge Edge {get;}

        public EdgeControl(Edge edge)
        {
            Edge=edge;

            Stroke = Brushes.Black;
            StrokeThickness = 2;

            UpdateColor();
            UpdatePosition();
        }

        public void UpdatePosition()
        {
            StartPoint = new Avalonia.Point(Edge.From.Position.X + Edge.From.Size.Width /2,
                                            Edge.From.Position.Y + Edge.From.Size.Height /2);
            EndPoint = new Avalonia.Point(Edge.To.Position.X + Edge.To.Size.Width /2,
                                            Edge.To.Position.Y + Edge.To.Size.Height /2);
        }

        public void UpdateColor()
        {
            if(Edge.Protocol != ProtocolType.TCP)
            {
                Stroke = Brushes.Black;
                return;
            }
            double t = Edge.LossRate;

            if(t<0.3)Stroke = Brushes.Green;
            else if(t<0.7)Stroke = Brushes.Yellow;
            else Stroke = Brushes.Red;
        }
    }
}