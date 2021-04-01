using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MasKod2D
{
    public abstract class BaseGameEntity
    {
        public Vector2D Pos { get; set; }
        public float Scale { get; set; }
        public World MyWorld { get; set; }
        public Texture2D Texture { get; set; }

        public BaseGameEntity(Vector2D pos, World w, Texture2D t)
        {
            Pos = pos;
            MyWorld = w;
            Texture = t;
        }

        public abstract void Update(float delta);

        public virtual void Render(SpriteBatch spriteBatch)
        {
            
        }


    }
    


    

    
}
