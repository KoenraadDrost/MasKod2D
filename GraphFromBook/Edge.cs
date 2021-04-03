using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasKod2D.GraphFromBook
{
    public class Edge
    {
        public Node From { get; set; }
        public Node To { get; set; }

        public Edge(Node from, Node to)
        {
            From = from;
            To = to;
        }

        public void Render(SpriteBatch spriteBatch)
        {
            Vector2 p1 = new Vector2(From.Location.X, From.Location.Y);
            Vector2 p2 = new Vector2(To.Location.X, To.Location.Y);
            spriteBatch.Begin();
            Primitives2D.DrawLine(spriteBatch, p1, p2, Color.White);
            
            spriteBatch.End();
        }
    }
}
