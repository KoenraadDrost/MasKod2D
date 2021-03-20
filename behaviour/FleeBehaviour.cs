using MasKod2D.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasKod2D.behaviour
{
    public class FleeBehaviour : SteeringBehaviour
    {
        public FleeBehaviour(MovingEntity me) : base(me) { }

        public override Vector2D Calculate()
        {
            Vector2D target = ME.MyWorld.Target.Pos;
            Vector2D vehicle = ME.Pos;

            // only flee if the target is within "panic" distance
            // work in distance squared space
            const double PanicDistanceSq = 10 * 10;
            double distanceSq = Math.Sqrt(Math.Pow(vehicle.X - target.X, 2) + Math.Pow(vehicle.Y - target.Y, 2));
            //Console.WriteLine("distancesq: " + distanceSq + " panicdistancesq: " + PanicDistanceSq);

            if (distanceSq > PanicDistanceSq)
            {
                return new Vector2D(0, 0);
            }

            Vector2D vehicleMinTarget = new Vector2D(vehicle.X - target.X, vehicle.Y - target.Y);
            Vector2D normalized = vehicleMinTarget.Normalize();

            Vector2D desiredVelocity = normalized.Multiply(ME.MaxSpeed);

            return desiredVelocity.Sub(ME.Velocity);
        }
    }
}
