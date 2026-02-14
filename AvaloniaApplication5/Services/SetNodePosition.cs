using System.Collections.Generic;
using System.Linq;
using System;
using System.Numerics;
using Avalonia.Controls;
using Avalonia;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

using AvaloniaApplication5.Models;

namespace AvaloniaApplication5.Services
{
      
    public static class SetNodePosition
    {
        public static float RepulsionStrength = 3000f;
        public static float SpringStrength = 0.05f;
        public static float Damping = 0.9f;
        public static float TimeStep = 0.2f;
        public static float centeringStrength = 0.0005f;

        public static List<Node> SetRandomPosition(List<Node> nodes, double canvasWidth, double canvasHeight)
        {
            Random rand = new Random();
            for(int i=0;i<nodes.Count;i++){ 
                double x = rand.NextDouble() * (canvasWidth - nodes[i].Size.Width);
                double y = rand.NextDouble() * (canvasHeight - nodes[i].Size.Height);
                nodes[i].Position = new Vector2((float)x, (float)y);
            }
            return nodes;
        }

        public static (List<Node>, List<Edge>) SetPosition(List<Node> nodes, List<Edge> edges, double canvasWidth, double canvasHeight, int iterations)
        {
            for(int i=0;i<iterations;i++)
            {
                foreach(var node in nodes)
                {
                    node.Velocity = new Vector2(0,0);
                }

                foreach(var node in nodes)
                {
                    node.Force = new Vector2(0,0);
                    // Apply centering force if needed
                    Vector2 center = new Vector2((float)(canvasWidth/2), (float)(canvasHeight/2));
                    Vector2 toCenter = center - node.Position;
                    node.Force += toCenter * centeringStrength;
                }

                foreach(var node in nodes) // Calculate repulsive forces
                {
                    foreach(var otherNode in nodes)
                    {
                        if (node == otherNode) continue;
                        Vector2 delta = node.Position - otherNode.Position;
                        float distance = delta.Length() + 0.1f; // Prevent division by zero
                        
                        Vector2 direction = Vector2.Normalize(delta);
                        float forceMagnitude = (float)(RepulsionStrength / (distance * distance));

                        Vector2 force = direction * forceMagnitude;
                        node.Force += force;
                        otherNode.Force -= force;
                    }
                }
                
                foreach(var edge in edges) // Calculate attractive forces
                {
                    Vector2 delta = edge.To.Position - edge.From.Position;
                    float distance = delta.Length() + 0.1f; // Prevent division by zero
                    
                    Vector2 direction = Vector2.Normalize(delta);
                    float displacement = (float)(distance - edge.RestLength);

                    Vector2 force = direction * (displacement * SpringStrength);
                    edge.From.Force += force;
                    edge.To.Force -= force;
                }
                
                foreach(var node in nodes) // Update velocities and positions
                {
                    node.Velocity += node.Force * TimeStep;
                    node.Velocity *= Damping;
                    node.Position += node.Velocity * TimeStep;
                    node.Position = new Vector2 (Math.Min(node.Position.X, (float)(canvasWidth - node.Size.Width)), Math.Min(node.Position.Y, (float)(canvasHeight - node.Size.Height)));
                    node.Position = new Vector2 (Math.Max(0, node.Position.X), Math.Max(0, node.Position.Y));
                }
            }
            return (nodes, edges);
        }
    }
}