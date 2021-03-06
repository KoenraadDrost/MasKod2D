﻿using MasKod2D.entity;
using MasKod2D.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasKod2D.behaviour
{
    class WanderBehaviour : SteeringBehaviour
    {
        Vector2D targetWorld;

        public static Random random = new Random();
        public WanderBehaviour (MovingEntity me) : base(me) { }

        public double ClampedRandDouble()
        {
            // No default RandomClamped double available in C-sharp, cast random int as double to simulate a random double that can be both negative and positive.
            return (double)random.Next(-1000, 1000) / 1000;
        }

        public Vector2D PointToWorldSpace(Vector2D point,
                                            Vector2D entityHeading,
                                            Vector2D entitySide,
                                            Vector2D entityPosition)
        {
            Vector2D TransVector = point;

            Matrix2D TransMatrix = new Matrix2D();

            TransMatrix = TransMatrix.RotateMatrix(entityHeading, entitySide);

            TransMatrix = TransMatrix.Translate(entityPosition.X, entityPosition.Y);

            //TransMatrix.TransformVector2Ds(TransVector);

            return TransVector;
        }

        public override Vector2D Calculate()
        {
            Console.WriteLine("WANDER");
            Console.WriteLine("Heading: " + ME.Heading);

            // Area ahead of entity that is wandered towards.
            const double WanderRadius = 250;
            const double WanderDistance = 300;
            const double WanderJitter = 250;
            double distanceSq = 0;

            if (targetWorld != null)
            {
                distanceSq = Math.Sqrt(Math.Pow(ME.Pos.X - targetWorld.X, 2) + Math.Pow(ME.Pos.X - targetWorld.Y, 2));
            }

            if (distanceSq < 10)
            {
                // Random target ( random x and y between -50 and +50 )
                Vector2D WanderTarget = new Vector2D(ClampedRandDouble() * WanderJitter,
                                                     ClampedRandDouble() * WanderJitter); // example: X = 45.341, Y = -11.576
                // Convert to target in wander area.
                WanderTarget.Normalize();
                WanderTarget.Multiply(WanderRadius);

                // Add distance relative to entity.
                Vector2D targetLocal = WanderTarget.Add(new Vector2D(WanderDistance, 0));

                // Convert to target in world space.
                targetWorld = PointToWorldSpace(targetLocal,
                                                            ME.Heading,
                                                            ME.Side,
                                                            ME.Pos);
            }

            return targetWorld - ME.Pos;
        }
    }
}
