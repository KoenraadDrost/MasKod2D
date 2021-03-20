using MasKod2D.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasKod2D.behaviour
{
    enum Deceleration
    {
        slow = 3,
        normal = 2,
        fast = 1
    }

    public class ArriveBehaviour : SteeringBehaviour
    {
        public ArriveBehaviour(MovingEntity me) : base(me) { }

        public override Vector2D Calculate()
        {
            Vector2D target = ME.MyWorld.Target.Pos;
            Vector2D vehicle = ME.Pos;

            Vector2D ToTarget = new Vector2D(target.X - vehicle.X, target.Y - vehicle.Y);

            // calculate the distance to the target position
            double dist = ToTarget.Length();

            if (dist > 0)
            {
                // because Deceleration is enumerated as an int,
                // this value is required to provide fine tweaking of the deceleration
                const double DecelerationTweaker = .25;

                // calculate the speed required to reach the target given the desired deceleration
                double speed = dist / ((double)Deceleration.normal * DecelerationTweaker);

                // make sure the velocity does not exceed the max
                speed -= ME.MaxSpeed;

                // from here proceed just like Seek except we don't need to normalize the ToTarget Vector
                // because we have already gone to the trouble of calculating its length: dist
                Vector2D targetMultiplySpeed = ToTarget.Multiply(speed);
                Vector2D desiredVelocity = targetMultiplySpeed.divide(dist);


                return desiredVelocity.Sub(ME.Velocity);
            }

            return new Vector2D(0, 0);
        }
    }
}
