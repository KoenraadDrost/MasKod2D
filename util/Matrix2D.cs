using System;
using System.Collections.Generic;
using System.Text;

namespace MasKod2D.util
{
    class Matrix2D
    {
        public float[,] mat = new float[2, 2];

        public Matrix2D()
        {
            // Create default identity  2 * 2 Matrix2D
            this.mat = Identity().mat;
        }

        public Matrix2D(float m11, float m12,
                      float m21, float m22)
        {
            // Create Matrix2D of 2 * 2
            mat[0, 0] = m11; mat[0, 1] = m12;
            mat[1, 0] = m21; mat[1, 1] = m22;
        }

        public Matrix2D(Vector2D v) : this((float)v.X, 0, (float)v.Y, 0)
        {
            // Only fill first column of size 2 matrix
        }

        public Matrix2D(int size)
        {
            // Create Matrix of size x * y
            mat = new float[size, size];

            // Set all values to 0
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    mat[x, y] = 0;
                }
            }
        }

        public static Matrix2D operator +(Matrix2D m1, Matrix2D m2)
        {
            return new Matrix2D(m1.mat[0, 0] + m2.mat[0, 0], m1.mat[0, 1] + m2.mat[0, 1],
                               m1.mat[1, 0] + m2.mat[1, 0], m1.mat[1, 1] + m2.mat[1, 1]);
        }

        public static Matrix2D operator -(Matrix2D m1, Matrix2D m2)
        {
            return new Matrix2D(m1.mat[0, 0] - m2.mat[0, 0], m1.mat[0, 1] - m2.mat[0, 1],
                               m1.mat[1, 0] - m2.mat[1, 0], m1.mat[1, 1] - m2.mat[1, 1]);
        }

        public static Matrix2D operator *(Matrix2D m1, float f)
        {
            return new Matrix2D(m1.mat[0, 0] * f, m1.mat[0, 1] * f,
                               m1.mat[1, 0] * f, m1.mat[1, 1] * f);
        }

        public static Matrix2D operator *(float f, Matrix2D m1)
        {
            return m1 * f;
        }

        public static Matrix2D operator *(Matrix2D m1, Matrix2D m2)
        {
            int mSize = m1.mat.GetLength(0);

            Matrix2D m3 = new Matrix2D(mSize);

            for (int x = 0; x < mSize; x++)
            {
                for (int y = 0; y < mSize; y++)
                {
                    // Sum of position (x,y)
                    float sum = 0;

                    for (int i = 0; i < mSize; i++)
                    {
                        // Add to sum: Matrix1 (row x, column i) * Matrix2 (row i, column y)
                        sum += m1.mat[x, i] * m2.mat[i, y];
                    }

                    // Add value to return matrix.
                    m3.mat[x, y] = sum;
                }
            }

            return m3;
        }

        public static Vector2D operator *(Matrix2D m1, Vector2D v)
        {
            return new Vector2D(m1.mat[0, 0] * v.X + m1.mat[0, 1] * v.Y,
                               m1.mat[1, 0] * v.X + m1.mat[1, 1] * v.Y);
        }

        public static Matrix2D Identity()
        {
            Matrix2D m = new Matrix2D(2);

            for (int i = 0; i < 2; i++)
            {
                m.mat[i, i] = 1;
            }

            return m;
        }

        public static Vector2D ToVector2D(Matrix2D m)
        {
            return new Vector2D(m.mat[0, 0], m.mat[1, 0]);
        }

        public override string ToString()
        {
            string result = "";

            int mSize = this.mat.GetLength(0);

            for (int x = 0; x < mSize; x++)
            {
                for (int y = 0; y < mSize; y++)
                {
                    // Sum of position ( x,y)
                    result += $"[{mat[x, y]}] ";
                }
            }

            return result;
        }

        public static Matrix2D ScaleMatrix(float s)
        {
            Matrix2D m = new Matrix2D();

            // loop through columns
            for (int i = 0; i < m.mat.GetLength(0); i++)
            {
                // loop through rows
                for (int j = 0; j < m.mat.GetLength(1); j++)
                {
                    // multiply value with given float.
                    m.mat[i, j] *= s;
                }
            }
            return m;
        }

        public static Matrix2D RotateMatrix(float degrees)
        {
            Matrix2D m = new Matrix2D();
            double polar = (degrees / 180) * Math.PI;

            m.mat[0, 0] = (float)Math.Cos(polar);
            m.mat[0, 1] = -(float)Math.Sin(polar);
            m.mat[1, 0] = (float)Math.Sin(polar);
            m.mat[1, 1] = (float)Math.Cos(polar);

            return m;
        }
    }
}
