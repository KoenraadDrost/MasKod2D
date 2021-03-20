using MasKod2D.entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasKod2D.behaviour
{
    class WanderBehaviour : SteeringBehaviour
    {
        public WanderBehaviour (MovingEntity me) : base(me) { }
        public override Vector2D Calculate()
        {
            Console.WriteLine("WANDER");
            Console.WriteLine("Heading: " + ME.Heading);

            // Area ahead of entity that is wandered towards.
            const double WanderMinDistanceSq = 10 * 10;
            const double WanderMaxDistanceSq = 80 * 80;

            // Distance to current heading
            double distanceSq = Math.Sqrt(Math.Pow(ME.Pos.X - ME.Heading.X, 2) + Math.Pow(ME.Pos.Y - ME.Heading.Y, 2));

            // If entity has reached minimum distance to current heading, change to new heading. Change is done before arriving to keep movement smooth.
            if (distanceSq < WanderMinDistanceSq)
            {
                // If Random is more frequently used, move into global var.
                Random random = new Random();

                double test = random.NextDouble();
                Console.WriteLine("random :" + test);

                // Set new Heading X-coördinate
                Double randomX = Math.Sqrt(random.NextDouble() * (WanderMaxDistanceSq - WanderMinDistanceSq) + WanderMinDistanceSq);
                Double randomY = Math.Sqrt(random.NextDouble() * (WanderMaxDistanceSq - WanderMinDistanceSq) + WanderMinDistanceSq);

                Console.WriteLine("random(x - y): (" + randomX + " - " + randomY + ")");

                Vector2D newHeading = new Vector2D(ME.Pos.X, ME.Pos.Y);
                //Vector2D worldMax = new Vector2D()

                Console.WriteLine("oldHeading: " + newHeading);
                Console.WriteLine("world=> width: " + ME.MyWorld.Width + ", height: " + ME.MyWorld.Height);



                //Console.WriteLine("randomWanderDistance: " + randomWanderDistance);
                if (ME.MyWorld.Width < (int)(randomX + ME.Pos.X))
                {
                    newHeading.X = ME.Pos.X - randomX;
                }
                else newHeading.X = ME.Pos.X + randomX;


                //// Set new Heading Y-coördinate
                //// If target coördinate is outside screen range, change direction away from screen edge.
                if (ME.MyWorld.Height < (int)(randomY + ME.Pos.Y))
                {
                    newHeading.Y = ME.Pos.Y - randomY;
                }
                else newHeading.Y = ME.Pos.Y + randomY;

                Console.WriteLine("newHeading: " + newHeading);

                ME.Heading = newHeading;
            }

            // Approach heading.
            Vector2D target = ME.Heading;
            Vector2D vehicle = ME.Pos;

            Vector2D targetMinVehicle = new Vector2D(target.X - vehicle.X, target.Y - vehicle.Y); // Vector(100, 100)
            Vector2D normalized = targetMinVehicle.Normalize(); // Vector(0.71, 0.71)

            Vector2D desiredVelocity = normalized.Multiply(ME.MaxSpeed);


            return desiredVelocity.Sub(ME.Velocity);
        }
    }
}
