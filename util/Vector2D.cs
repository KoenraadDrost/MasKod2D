using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS
{
   
    public class Vector2D
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vector2D() : this(0,0)
        {
        }

        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double Length()
        {
            double xPow = Math.Pow(X, 2);
            double yPow = Math.Pow(Y, 2);
            return Math.Sqrt(xPow + yPow);
        }

        public double LengthSquared()
        {
            return Math.Sqrt(Length());
        }

        public Vector2D Add(Vector2D v)
        {
            this.X += v.X;
            this.Y += v.Y;
            return this;
        }

        public Vector2D Sub(Vector2D v)
        {
            this.X -= v.X;
            this.Y -= v.Y;
            return this;
        }

        public Vector2D Multiply(double value)
        {
            this.X *= value;
            this.Y *= value;
            return this;
        }

        public Vector2D divide(double value)
        {
            this.X = X / value;
            this.Y = Y / value;
            return this;
        }

        public Vector2D Normalize()
        {
            X = X / Length();
            Y = Y / Length();
            return this;
        }

        public Vector2D truncate(double maX)
        {
            if (Length() > maX)
            {
                Normalize();
                Multiply(maX);
            }
            return this;
        }
        
        public Vector2D Clone()
        {
            return new Vector2D(this.X, this.Y);
        }
        
        public override string ToString()
        {
            return String.Format("({0},{1})", X, Y);
        }
    }


}
