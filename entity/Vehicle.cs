using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasKod2D.entity
{
    public class Vehicle : MovingEntity
    {
        public Color VColor { get; set; }

        public Vehicle(Vector2D pos, World w, Texture2D t) : base(pos, w, t)
        {
            Velocity = new Vector2D(0, 0);
            Scale = 5;

            Color[] data = new Color[20 * 20];
            for (int i = 0; i < (20 * 20); ++i) data[i] = Color.Chocolate;
            Texture.SetData<Color>(data);

        }

        public override void Render(GraphicsDevice g)
        {
            /*double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;
            double size = Scale * 2;


            Pen p = new Pen(VColor, 2);
            g.DrawEllipse(p, new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));
            g.DrawEllipse(p, new Rectangle((int)leftCorner - 45, (int)rightCorner - 45, (int)size * 10, (int)size * 10));
            g.DrawLine(p, (int)Pos.X, (int)Pos.Y, (int)Pos.X + (int)(Velocity.X * 2), (int)Pos.Y + (int)(Velocity.Y * 2));*/

        }
    }
}
