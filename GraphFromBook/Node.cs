using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MasKod2D.GraphFromBook
{
    public class Node
    {
        public Vector2 Location { get; private set; }
        public bool IsWalkable { get; set; }
        public float G { get; private set; }
        public float H { get; private set; }
        public float F { get { return G + H; } }
        public NodeState State { get; set; }
        public Node ParentNode { get; set; }
        public int Width { get; set; }
        public Color Color { get; set; }
        public int Scale { get; set; }

        public Node(int x, int y, int scale)
        {
            Location = new Vector2(x, y);
            State = NodeState.Untested;
            ParentNode = null;
            Width = 5;
            Color = Color.Black;
            Scale = scale;
        }

        public Node(int x, int y, bool isWalkable, int scale)
        {
            Location = new Vector2(x, y);
            IsWalkable = isWalkable;
            State = NodeState.Untested;
            ParentNode = null;
            Width = 5;
            Color = Color.Black;
            Scale = scale;
        }

        public float GetTraversalCost(Node node, Node parentNode, Node end)
        {
            ParentNode = parentNode;
            G = Vector2.Distance(node.Location, parentNode.Location);
            H = Vector2.Distance(node.Location, end.Location);
            return F;
        }

        public void Render(SpriteBatch spriteBatch)
        {
            int offset = Scale / 2;
            int x = Convert.ToInt32(Location.X * Scale);
            int y = Convert.ToInt32(Location.Y * Scale);

            Point location = new Point(x, y);
            Point size = new Point(Scale);
            Rectangle rec = new Rectangle(location, size);

            Vector2 center = new Vector2(x + offset, y + offset);
            float radius = 10f;
            int sides = 4;

            spriteBatch.Begin();
            Primitives2D.DrawRectangle(spriteBatch, rec, Color.Black);
            Primitives2D.DrawCircle(spriteBatch, center, radius, sides, Color);
            spriteBatch.End();
        }
    }

    public enum NodeState { Untested, Open, Closed }
}
