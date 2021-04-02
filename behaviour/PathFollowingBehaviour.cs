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
                Node waypoint = ME.Path[0];

                Vector2D target = new Vector2D(waypoint.Location.X * ME.MyWorld.F, waypoint.Location.Y * ME.MyWorld.F);
                Vector2D vehicle = ME.Pos;

                Vector2D dis = new Vector2D(target.X - vehicle.X, target.Y - vehicle.Y);

                if (dis.Length() < 50)
                    ME.Start = waypoint;

                Vector2D disNormalized = dis.Normalize();
                Vector2D desiredVelocity = disNormalized.Multiply(ME.MaxSpeed);

                return desiredVelocity.Sub(ME.Velocity);
            }

            return new Vector2D();
        }

        
    }
}
