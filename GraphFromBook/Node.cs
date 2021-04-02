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

        public Node(int x, int y)
        {
            Location = new Vector2(x, y);
            State = NodeState.Untested;
            ParentNode = null;
            Width = 5;
            Color = Color.Black;
        }

        public Node(int x, int y, bool isWalkable)
        {
            Location = new Vector2(x, y);
            IsWalkable = isWalkable;
            State = NodeState.Untested;
            ParentNode = null;
            Width = 5;
            Color = Color.Black;
        }

        public float GetTraversalCost(Node node, Node parentNode, Node end)
        {
            ParentNode = parentNode;
            G = Vector2.Distance(node.Location, parentNode.Location);
            H = Vector2.Distance(node.Location, end.Location);
            return F;
        }

        public void Render(SpriteBatch spriteBatch, int f)
        {
            int x = Convert.ToInt32(Location.X * f);
            int y = Convert.ToInt32(Location.Y * f);

            Point location = new Point(x, y);
            Point size = new Point(f);
            Rectangle rec = new Rectangle(location, size);

            Vector2 center = new Vector2(x, y);
            float radius = 10f;
            int sides = 4;

            spriteBatch.Begin();
            //Primitives2D.DrawRectangle(spriteBatch, rec, Color.Black);
            Primitives2D.DrawCircle(spriteBatch, center, radius, sides, Color);
            spriteBatch.End();
        }
    }

    public enum NodeState { Untested, Open, Closed }
}
