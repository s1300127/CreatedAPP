using Avalonia.Controls;
using Avalonia.Input;
using Avalonia;
using AvaloniaApplication5.Models;
using System;
using System.Numerics;

namespace AvaloniaApplication5.Controls{
    public partial class NodeControl : UserControl
    {
        private bool _isDragging;
        private Avalonia.Point _dragStart;
        private Avalonia.Point _originalPos;
        public event Action? PositionChanged;
        public Node Node {get;}

        public NodeControl(Node node)
        {
            InitializeComponent();
            DataContext = node;
            Node=node;
            
            Width = node.Size.Width;
            Height = node.Size.Height;

            Canvas.SetLeft(this, node.Position.X);
            Canvas.SetTop(this, node.Position.Y);
            
            this.Name = node.Name;

            PointerPressed += OnPressed;
            PointerMoved += OnMoved;
            PointerReleased += OnReleased;
        }

        private void OnPressed(object? sender, PointerPressedEventArgs e)
        {
            if(Parent is Canvas parent)
            {
                _isDragging = true;
                _dragStart = e.GetPosition(parent);
                _originalPos = new Avalonia.Point(Canvas.GetLeft(this),Canvas.GetTop(this));
                e.Pointer.Capture(this);
            }
        }

        private void OnMoved(object? sender, PointerEventArgs e)
        {
            if(_isDragging && Parent is Canvas parent)
            {
                var current = e.GetPosition(parent);
                var dx = current.X - _dragStart.X;
                var dy = current.Y - _dragStart.Y;

                Canvas.SetLeft(this, _originalPos.X + dx);
                Canvas.SetTop(this, _originalPos.Y + dy);

                Node.Position = new System.Numerics.Vector2(
                    (float)(Canvas.GetLeft(this)),
                    (float)(Canvas.GetTop(this)));
                
                PositionChanged?.Invoke();
            }
        }

        private void OnReleased(object? sender, PointerReleasedEventArgs e)
        {
            _isDragging = false;
            e.Pointer.Capture(null);
        }
    }
}