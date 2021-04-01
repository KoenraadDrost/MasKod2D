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
            Vector2D target = ME.MyWorld.Target.Pos;
            Vector2D vehicle = ME.Pos;
            
            Vector2D dis = new Vector2D(target.X - vehicle.X, target.Y - vehicle.Y);
           
            //Console.WriteLine(dis.Length());
            Vector2D disNormalized = dis.Normalize();
            Vector2D desiredVelocity = disNormalized.Multiply(ME.MaxSpeed);
            return desiredVelocity.Sub(ME.Velocity);
        }
    }
}
