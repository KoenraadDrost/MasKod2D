using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasKod2D.entity
{
    public class StaticEntity : BaseGameEntity
    {
        public double Radius { get; set; }
        public StaticEntity(Vector2D pos, World w, Texture2D t) : base(pos, w, t)
        {
            Radius = 50;
            Color[] data = new Color[50 * 50];
            for (int i = 0; i < (50 * 50); ++i) data[i] = Color.Blue;
            Texture.SetData<Color>(data);
        }
        public override void Update(float delta)
        {
            throw new NotImplementedException();
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Texture, new Vector2((float)Pos.X, (float)Pos.Y), Color.White);
            spriteBatch.End();
        }

    }
}
