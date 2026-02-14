using Avalonia;
using System;
using System.Numerics;

namespace AvaloniaApplication5.Models
{
    public class Node
    {
        public string Name{get;set;}="";
        public Vector2 Position{get;set;} = new Vector2(0,0);
        public Vector2 Velocity{get;set;} = new Vector2(0,0);
        public Vector2 Force{get;set;} = new Vector2(0,0);
        public Size Size{get;set;} = new Size(100,50);

        public Node(string name, Vector2 position)
        {
            Name = name;
            Position = position;
        }

        public Node()
        {
            
        }
    }
}