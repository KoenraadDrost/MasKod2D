using MasKod2D.behaviour;
using MasKod2D.entity;
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
        public List<StaticEntity> obstacles = new List<StaticEntity>();
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
            Vehicle v = new Vehicle(new Vector2D(50,50), this, new Texture2D(GD, 20, 20));
            entities.Add(v);

            Target = new Vehicle(new Vector2D(300, 300), this, new Texture2D(GD, 20, 20));

            // Top left
            Obstacle obstacle = new Obstacle(new Vector2D(150, 100), this, new Texture2D(GD, 50, 50));
            obstacles.Add(obstacle);

            // Top right
            Obstacle obstacle2 = new Obstacle(new Vector2D(250, 100), this, new Texture2D(GD, 50, 50));
            obstacles.Add(obstacle2);

            // Bottom left
            Obstacle obstacle3 = new Obstacle(new Vector2D(150, 200), this, new Texture2D(GD, 50, 50));
            obstacles.Add(obstacle3);

            // Bottom right
            Obstacle obstacle4 = new Obstacle(new Vector2D(250, 200), this, new Texture2D(GD, 50, 50));
            obstacles.Add(obstacle4);
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
