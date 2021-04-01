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

        public Node(int x, int y, bool isWalkable)
        {
            Location = new Vector2(x, y);
            IsWalkable = isWalkable;
            State = NodeState.Untested;
            ParentNode = null;
            Width = 20;
            Color = Color.Black;
        }

        public float GetTraversalCost(Node location, Node parentNodeLocation, Node endLocation)
        {
            ParentNode = parentNodeLocation;
            G = Vector2.Distance(location.Location, parentNodeLocation.Location);
            H = Vector2.Distance(location.Location, endLocation.Location);
            return F;
        }

        public float SetF(Node location, Node parentNodeLocation, Node endLocation)
        {
            ParentNode = parentNodeLocation;
            G = Vector2.Distance(location.Location, parentNodeLocation.Location);
            H = Vector2.Distance(location.Location, endLocation.Location);
            return F;
        }

        public void Render(GraphicsDevice gd, SpriteBatch spriteBatch)
        {
            Texture2D texture = new Texture2D(gd, Width, Width);
            Color[] data = new Color[Width * Width];

            if (!IsWalkable)
                Color = Color.Red;

            for (int i = 0; i < (Width * Width); ++i) data[i] = Color;
            texture.SetData<Color>(data);
            spriteBatch.Begin();
            spriteBatch.Draw(texture, new Vector2(Location.X * 100, Location.Y * 100), Color);
            spriteBatch.End();
        }
    }

    public enum NodeState { Untested, Open, Closed }
}
