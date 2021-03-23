using MasKod2D.entity;
using MasKod2D.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasKod2D.behaviour
{
    class WanderBehaviour : SteeringBehaviour
    {
        public static Random random = new Random();
        public WanderBehaviour (MovingEntity me) : base(me) { }

        public double ClampedRandDouble()
        {
            // No default RandomClamped double available in C-sharp, cast random int as double to simulate a random double that can be both negative and positive.
            return (double)random.Next(-1000, 1000) / 1000;
    }
        public override Vector2D Calculate()
        {
            Console.WriteLine("WANDER");
            Console.WriteLine("Heading: " + ME.Heading);            

            // Area ahead of entity that is wandered towards.
            const double WanderRadius = 70;
            const double WanderDistance = 100;
            const double WanderJitter = 70;

            Console.WriteLine($"Random test: {ClampedRandDouble()}");

            Vector2D WanderTarget = new Vector2D(ClampedRandDouble() * WanderJitter,
                                                 ClampedRandDouble() * WanderJitter);
            WanderTarget.Normalize();
            Console.WriteLine($"Normalize test: {WanderTarget}");
            WanderTarget.Multiply(WanderRadius);
            Console.WriteLine($"Multiply test: {WanderTarget}");

            Vector2D targetLocal = WanderTarget.Add( new Vector2D(WanderDistance, 0) );

            Vector2D targetWorld = new Vector2D();

            Matrix2D m = new Matrix2D();

            // Distance to current heading
            double distanceSq = Math.Sqrt(Math.Pow(ME.Pos.X - ME.Heading.X, 2) + Math.Pow(ME.Pos.Y - ME.Heading.Y, 2));

            // If entity has reached minimum distance to current heading, change to new heading. Change is done before arriving to keep movement smooth.
            //if (distanceSq < WanderMinDistanceSq)
            //{
            //    // If Random is more frequently used, move into global var.
            //    //Random random = new Random();

            //    double test = random.NextDouble();
            //    Console.WriteLine("random :" + test);

            //    // Set new Heading X-coördinate
            //    Double randomX = Math.Sqrt(random.Next(1, -1) * (WanderMaxDistanceSq - WanderMinDistanceSq) + WanderMinDistanceSq);
            //    Double randomY = Math.Sqrt(random.NextDouble() * (WanderMaxDistanceSq - WanderMinDistanceSq) + WanderMinDistanceSq);

            //    Console.WriteLine("random(x - y): (" + randomX + " - " + randomY + ")");

            //    Vector2D newHeading = new Vector2D(ME.Pos.X, ME.Pos.Y);
            //    //Vector2D worldMax = new Vector2D()

            //    Console.WriteLine("oldHeading: " + newHeading);
            //    Console.WriteLine("world=> width: " + ME.MyWorld.Width + ", height: " + ME.MyWorld.Height);



            //    //Console.WriteLine("randomWanderDistance: " + randomWanderDistance);
            //    if (ME.MyWorld.Width < (int)(randomX + ME.Pos.X))
            //    {
            //        newHeading.X = ME.Pos.X - randomX;
            //    }
            //    else newHeading.X = ME.Pos.X + randomX;


            //    //// Set new Heading Y-coördinate
            //    //// If target coördinate is outside screen range, change direction away from screen edge.
            //    if (ME.MyWorld.Height < (int)(randomY + ME.Pos.Y))
            //    {
            //        newHeading.Y = ME.Pos.Y - randomY;
            //    }
            //    else newHeading.Y = ME.Pos.Y + randomY;

            //    Console.WriteLine("newHeading: " + newHeading);

            //    ME.Heading = newHeading;
            //}

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
