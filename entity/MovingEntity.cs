using SteeringCS.behaviour;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.entity
{

    public abstract class MovingEntity : BaseGameEntity
    {
        public Vector2D Velocity { get; set; }
        public float Mass { get; set; }
        public float MaxSpeed { get; set; }

        public SteeringBehaviour SB { get; set; }

        public MovingEntity(Vector2D pos, World w) : base(pos, w)
        {
            Mass = 30;
            MaxSpeed = 150;
            Velocity = new Vector2D();
        }

        public override void Update(float timeElapsed)
        {
            // calculate the combined force from each steering behavior in the vehicle's list
            //SB = new SeekBehaviour(this);
            //SB = new FleeBehaviour(this);
            SB = new ArriveBehaviour(this);
            Vector2D steeringForce = SB.Calculate();

            // acceleration = force / mass
            Vector2D acceleration = steeringForce.divide(Mass);

            // update velocity
            Velocity.Add(acceleration.Multiply(timeElapsed));

            // make sure vehicle does not exceed maximum velocity
            Velocity.truncate(MaxSpeed);

            // update the position
            Pos = Pos.Add(Velocity.Multiply(timeElapsed));

            Console.WriteLine(ToString());
        }

        public override string ToString()
        {
            return String.Format("{0}", Velocity);
        }
    }
}
