using MasKod2D.behaviour;
using MasKod2D.entity;
using MasKod2D.state;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasKod2D
{
    public class World
    {
        public List<MovingEntity> entities = new List<MovingEntity>();
        public Vehicle Target { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public GraphicsDevice GD { get; set; }

        public World(int w, int h, GraphicsDevice gd)
        {
            Width = w;
            Height = h;
            GD = gd;
        }

        public void populate()
        {
            Vehicle v = new Vehicle(new Vector2D(100,100), this, new Texture2D(GD, 20, 20));
            entities.Add(v);

            Target = new Vehicle(new Vector2D(100, 60), this, new Texture2D(GD, 20, 20));
            Target.VColor = Color.DarkRed;
            Target.Pos = new Vector2D(200, 200);

            // TODO: Move to individual entity later
            StateMachine sM = new StateMachine();
            Console.WriteLine($"Statemachine test: ");
            sM.RunScript();
        }

        /*public void Update(float timeElapsed)
        {
            foreach (MovingEntity me in entities)
            {
                me.SB = new SeekBehaviour(me); // restore later
                me.Update(timeElapsed);
            }  
        }

        public void Render(Graphics g)
        {
            entities.ForEach(e => e.Render(g));
            Target.Render(g);
        }*/
    }
}
