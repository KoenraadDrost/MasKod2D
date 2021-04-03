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
        public int Scale { get; set; } = 50;

        public World(int w, int h, GraphicsDevice gd)
        {
            Width = w;
            Height = h;
            GD = gd;
        }

        public void GenerateGraph()
        {
            Graph = new Graph(Width / Scale, Height / Scale);
            Graph.Scale = Scale;
            unwalkables.Add(new Node(2, 2, false, Scale));
            unwalkables.Add(new Node(2, 3, false, Scale));
            unwalkables.Add(new Node(3, 2, false, Scale));
            unwalkables.Add(new Node(3, 3, false, Scale));
            unwalkables.Add(new Node(4, 2, false, Scale));
            unwalkables.Add(new Node(4, 3, false, Scale));

            unwalkables.Add(new Node(2, 10, false, Scale));
            unwalkables.Add(new Node(2, 11, false, Scale));
            unwalkables.Add(new Node(3, 10, false, Scale));
            unwalkables.Add(new Node(3, 11, false, Scale));
            unwalkables.Add(new Node(4, 10, false, Scale));
            unwalkables.Add(new Node(4, 11, false, Scale));

            unwalkables.Add(new Node(8, 6, false, Scale));
            unwalkables.Add(new Node(8, 7, false, Scale));
            unwalkables.Add(new Node(9, 6, false, Scale));
            unwalkables.Add(new Node(9, 7, false, Scale));

            unwalkables.Add(new Node(13, 2, false, Scale));
            unwalkables.Add(new Node(13, 3, false, Scale));
            unwalkables.Add(new Node(14, 2, false, Scale));
            unwalkables.Add(new Node(14, 3, false, Scale));
            unwalkables.Add(new Node(15, 2, false, Scale));
            unwalkables.Add(new Node(15, 3, false, Scale));

            unwalkables.Add(new Node(13, 10, false, Scale));
            unwalkables.Add(new Node(13, 11, false, Scale));
            unwalkables.Add(new Node(14, 10, false, Scale));
            unwalkables.Add(new Node(14, 11, false, Scale));
            unwalkables.Add(new Node(15, 10, false, Scale));
            unwalkables.Add(new Node(15, 11, false, Scale));

        }

        public void Populate()
        {
            Player = new Vehicle(new Vector2D(100, 100), this, new Texture2D(GD, 20, 20), Color.White);
            Player.MaxSpeed = 100;
            Player.Start = new Node(1, 1, true, Scale);
            Player.End = new Node(5, 2, true, Scale);
            Player.Pos = new Vector2D(Player.Start.Location.X * Scale, Player.Start.Location.Y * Scale);
            Player.SB = new PathFollowingBehaviour(Player);
            //Player.Path = Graph.FindPath(Player); 

            Vehicle v = new Vehicle(new Vector2D(400, 400), this, new Texture2D(GD, 20, 20), Color.Orange);
            v.MaxSpeed = 25;
            v.Start = new Node(4, 4, Scale);
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
                    {
                        node.Color = Color.White;
                        node.Render(spriteBatch);
                    }
                        
                }

                // Color path of all entities
                foreach (MovingEntity me in entities)
                {                    
                    foreach (Node n in me.Path)
                    {
                        if (n.Location == node.Location)
                        {
                            node.Color = Color.Orange;
                            node.Render(spriteBatch);
                        }
                            
                    }
                }

                // Color all unwalkable nodes
                foreach (Node n in unwalkables)
                {
                    if (n.Location == node.Location)
                    {
                        node.Color = Color.Red;
                        node.Render(spriteBatch);
                    }
                        
                }

                if(Graph.IsVisible)
                    node.Render(spriteBatch);
            }

            foreach (StaticEntity se in obstacles)
            {
                se.Render(spriteBatch);
            }
        } 
    }
}
