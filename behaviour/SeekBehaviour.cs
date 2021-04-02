using MasKod2D.entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasKod2D.behaviour
{
    public class SeekBehaviour : SteeringBehaviour
    {
        public SeekBehaviour(MovingEntity me) : base(me) {}
        public override Vector2D Calculate()
        {
            Console.WriteLine("SEEK");
            Vector2D target = ME.MyWorld.Target.Pos;
            Vector2D vehicle = ME.Pos;

            //Vector2D normalize = target.Sub(position).Normalize();
            Vector2D targetMinVehicle = new Vector2D(target.X - vehicle.X, target.Y - vehicle.Y); // Vector(100, 100)
            Vector2D normalized = targetMinVehicle.Normalize(); // Vector(0.71, 0.71)

            Vector2D desiredVelocity = normalized.Multiply(ME.MaxSpeed); // Vector(106, 106)
            //Console.WriteLine("desired velocity: " + normalize.ToString());

            return desiredVelocity.Sub(ME.Velocity);
        }
    }
}
