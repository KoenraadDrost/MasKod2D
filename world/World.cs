using MasKod2D.behaviour;
using MasKod2D.entity;
using MasKod2D.GraphFromBook;
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
        public List<Node> unwalkables = new List<Node>();
        public Vehicle Target { get; set; }
        public Vehicle Player { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public GraphicsDevice GD { get; set; }
        public Graph Graph { get; set; }
        public int F { get; set; } = 100;

        public World(int w, int h, GraphicsDevice gd)
        {
            Width = w;
            Height = h;
            GD = gd;
        }

        public void GenerateGraph()
        {
            Graph = new Graph(12, 10);
            unwalkables.Add(new Node(3, 2, false));
            unwalkables.Add(new Node(3, 3, false));

            unwalkables.Add(new Node(1, 5, false));
            unwalkables.Add(new Node(1, 6, false));

            unwalkables.Add(new Node(4, 5, false));
            unwalkables.Add(new Node(4, 7, false));

            unwalkables.Add(new Node(6, 2, false));
            unwalkables.Add(new Node(7, 4, false));
        }

        public void Populate()
        {
            

            Player = new Vehicle(new Vector2D(100, 100), this, new Texture2D(GD, 20, 20), Color.White);
            Player.MaxSpeed = 100;
            Player.Start = new Node(1, 1, true);
            Player.End = new Node(5, 2, true);
            Player.Pos = new Vector2D(Player.Start.Location.X * F, Player.Start.Location.Y * F);
            Player.SB = new PathFollowingBehaviour(Player);
            //Player.Path = Graph.FindPath(Player);

            Vehicle v = new Vehicle(new Vector2D(400, 400), this, new Texture2D(GD, 20, 20), Color.Orange);
            v.MaxSpeed = 25;
            v.Start = new Node(4, 4);
            v.End = Player.End;
            v.SB = new PathFollowingBehaviour(v);
            entities.Add(v);

            //Target = new Vehicle(new Vector2D(Graph.EndLocation.Location.X, Graph.EndLocation.Location.Y), this, new Texture2D(GD, 20, 20));
        }

        public void Update(float timeElapsed, SpriteBatch spriteBatch)
        {
            Player.Render(spriteBatch);
            Player.Update(0.8f);
            

            foreach (MovingEntity me in entities)
            { 
                me.Update(0.8f);
                me.Render(spriteBatch);
            }

            foreach (Node node in Graph.Map)
            {
                // Color path of player
                foreach (Node n in Player.Path)
                {
                    if (n.Location == node.Location)
                        node.Color = Color.White;
                }

                // Color path of all entities
                foreach (MovingEntity me in entities)
                {
                    foreach (Node n in me.Path)
                    {
                        if (n.Location == node.Location)
                            node.Color = Color.Orange;
                    }
                }

                // Color all unwalkable nodes
                foreach (Node n in unwalkables)
                {
                    if (n.Location == node.Location)
                        node.Color = Color.Red;
                }
                node.Render(spriteBatch, F);
            }

            foreach (StaticEntity se in obstacles)
            {
                se.Render(spriteBatch);
            }
        } 
    }
}
