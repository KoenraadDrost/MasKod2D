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
        public Vehicle Target { get; set; }
        public Vehicle Player { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public GraphicsDevice GD { get; set; }
        public Graph Graph { get; set; }
        private int waypointCount = 0;

        public World(int w, int h, GraphicsDevice gd)
        {
            Width = w;
            Height = h;
            GD = gd;
        }

        public void GenerateGraph()
        {
            Graph = new Graph(8, 6);
            Graph.StartLocation = new Node(0, 0, true);
            Graph.EndLocation = new Node(5, 2, true);
            Graph.GetNode(3, 0).IsWalkable = false;
            Graph.GetNode(3, 1).IsWalkable = false;
            Graph.GetNode(3, 2).IsWalkable = false;
            Graph.GetNode(3, 3).IsWalkable = false;
            //Graph.GetNode(3, 4).IsWalkable = false;
        }

        public void Populate()
        {
            /*Vehicle v = new Vehicle(new Vector2D(50, 50), this, new Texture2D(GD, 20, 20));
            entities.Add(v);*/

            Player = new Vehicle(new Vector2D(300, 300), this, new Texture2D(GD, 20, 20));
            Player.Pos = new Vector2D(Graph.StartLocation.Location.X * 99, Graph.StartLocation.Location.Y * 99);

            Target = new Vehicle(new Vector2D(Graph.EndLocation.Location.X, Graph.EndLocation.Location.Y), this, new Texture2D(GD, 20, 20));
        }

        public void Update(float timeElapsed, SpriteBatch spriteBatch)
        {
            GenerateGraph();
            
            List<Node> shortestPath = Graph.FindPath();
            // Calculate the shortest path and set waypoints
            if (waypointCount < shortestPath.Count)
            {
                Node WayPoint = shortestPath[waypointCount];

                //Draw path
                Graph.GetNode(Graph.StartLocation.Location.X, Graph.StartLocation.Location.Y).Color = Color.LightGreen;               
                foreach(Node n in shortestPath)
                {
                    Graph.GetNode(n.Location.X, n.Location.Y).Color = Color.AntiqueWhite;
                }
                Graph.GetNode(Graph.EndLocation.Location.X, Graph.EndLocation.Location.Y).Color = Color.Yellow;

                // Get distance to target
                Vector2D target = Player.MyWorld.Target.Pos;
                Vector2D vehicle = Player.Pos;
                Vector2D dis = new Vector2D(target.X - vehicle.X, target.Y - vehicle.Y);
                
                // Go to next waypoint when close to target
                if(dis.Length() < 1)
                {
                    waypointCount++;
                }

                Player.Render(spriteBatch);
                Player.SB = new PathFollowingBehaviour(Player, WayPoint);
                Player.Update(0.8f);
            }
            

            foreach (MovingEntity me in entities)
            {
                me.SB = new SeekBehaviour(me);
                me.Update(0.8f);
                me.Render(spriteBatch);
            }

            foreach (KeyValuePair<Node, bool> node in Graph.Map)
            {
                node.Key.Render(GD, spriteBatch);
            }

            foreach(StaticEntity se in obstacles)
            {
                se.Render(spriteBatch);
            }
        } 
    }
}
