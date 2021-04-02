using MasKod2D.GraphFromBook;
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

        public Vehicle(Vector2D pos, World w, Texture2D t, Color color) : base(pos, w, t)
        {
            Velocity = new Vector2D(0, 0);
            Scale = 5;
            VColor = color;
            Color[] data = new Color[20 * 20];
            for (int i = 0; i < (20 * 20); ++i) data[i] = VColor;
            t.SetData<Color>(data);

        }

        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Texture, new Vector2((float)Pos.X, (float)Pos.Y), VColor);
            spriteBatch.End();
        }
    }
}
