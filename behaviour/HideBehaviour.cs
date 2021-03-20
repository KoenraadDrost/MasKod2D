using MasKod2D.entity;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MasKod2D.behaviour
{
    public class HideBehaviour : SteeringBehaviour
    {
        
        public HideBehaviour(MovingEntity me) : base(me) { }

        public override Vector2D Calculate()
        {
            Vector2D target = ME.MyWorld.Target.Pos;
            List<StaticEntity> obstacles = ME.MyWorld.obstacles;
            double DistToClosest = Double.MaxValue;
            Vector2D BestHidingSpot = new Vector2D();
            int spotNum = 0;

            for(int i = 0; i < ME.MyWorld.obstacles.Count; i++)
            {
                // Calculate center of obstacle
                /*Vector2D center = new Vector2D();
                center.X = obstacles[i].Pos.X - (obstacles[i].Texture.Width / 2);
                center.Y = obstacles[i].Pos.Y - (obstacles[i].Texture.Height / 2);*/

                // Calculate the position of the hiding spot for this obstacle
                Vector2D HidingSpot = GetHidingPosition(obstacles[i].Pos, obstacles[i].Radius, target);

                // Work in distance-squared space to find the closest hiding spot to the agent
                Vector2 HidingSpotToVector2 = new Vector2((float)HidingSpot.X, (float)HidingSpot.Y);
                Vector2 m_pVehicle = new Vector2((float)ME.Pos.X, (float)ME.Pos.Y);
                double dist = Vector2.Distance(HidingSpotToVector2, m_pVehicle);

                Console.WriteLine("Distance to hiding spot " + i + ": " + dist);

                if(dist < DistToClosest)
                {
                    DistToClosest = dist; 
                    BestHidingSpot = HidingSpot;

                    spotNum = i;
                }
            }
            Console.WriteLine();

            // If no suitable obstacles found then evade the target
            if(DistToClosest == Double.MaxValue)
            {
                Console.WriteLine("NO OBSTACLE FOUND");
                ME.SB = new FleeBehaviour(ME);
            }

            Console.WriteLine("BestHidingSpot: " + spotNum);
            ME.SB = new ArriveBehaviour(ME, BestHidingSpot, Deceleration.fast);
            return ME.SB.Calculate();
        }
         
        public Vector2D GetHidingPosition(Vector2D posOb, double radiusOb, Vector2D posTarget)
        {
            // Calculate how far away the agent is to be from the chosen obstacle's bounding radius
            const double DistanceFromBoundary = 30.0;   
            double DistAway = radiusOb + DistanceFromBoundary;

            // Calculate the heading toward the object from the target
            Vector2D obMinTarget = new Vector2D(posOb.X - posTarget.X, posOb.Y - posTarget.Y);
            Vector2D ToOb = obMinTarget.Normalize();

            // Scale it to size and add to the obstacle's position to get the hiding spot
            Vector2D ToObMultiplyDistAway = ToOb.Multiply(DistAway);

            return ToObMultiplyDistAway.Add(posOb);
        }
    }
}
