using MasKod2D.entity;
using MasKod2D.GraphFromBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasKod2D.behaviour
{
    public class PathFollowingBehaviour : SteeringBehaviour
    {
        public PathFollowingBehaviour(MovingEntity me) : base(me) 
        {

        }

        public override Vector2D Calculate()
        {
            if (ME.Start != ME.End)
            {
                ME.Path = ME.MyWorld.Graph.FindPath(ME);

                if (ME.Path.Count > 0)
                {
                    Node waypoint = ME.Path[0];

                    Vector2D target = new Vector2D(waypoint.Location.X * ME.MyWorld.Scale, waypoint.Location.Y * ME.MyWorld.Scale);
                    Vector2D vehicle = ME.Pos;

                    Vector2D dis = new Vector2D(target.X - vehicle.X, target.Y - vehicle.Y);

                    if (dis.Length() < 25)
                        ME.Start = waypoint;

                    Vector2D disNormalized = dis.Normalize();
                    Vector2D desiredVelocity = disNormalized.Multiply(ME.MaxSpeed);

                    return desiredVelocity.Sub(ME.Velocity);
                }
            }

            return new Vector2D();
        }

        
    }
}
