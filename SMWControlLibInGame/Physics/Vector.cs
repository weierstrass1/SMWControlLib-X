using System;
using System.Collections.Generic;
using System.Text;

namespace SMWControlLibInGame.Physics
{
    public struct Vector
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public Vector(float x)
        {
            X = x;
            Y = 0;
            Z = 0;
        }
        public Vector(float x, float y)
        {
            X = x;
            Y = y;
            Z = 0;
        }
        public Vector(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public static float DotProd(Vector a, Vector b)
        {
            return (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z);
        }
        public static Vector CrossProd(Vector a, Vector b)
        {
            return new Vector((a.Y * b.Z) - (b.Y * a.Z), (b.X * a.Z) - (a.X * b.Z), (a.X * b.Y) - (a.Y * b.X));
        }
        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }
        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }
        public static Vector operator *(Vector a, float b)
        {
            return new Vector(a.X * b, a.Y * b, a.Z * b);
        }
        public static Vector operator *(float b, Vector a)
        {
            return new Vector(a.X * b, a.Y * b, a.Z * b);
        }
        public static Vector operator /(Vector a, float b)
        {
            return new Vector(a.X / b, a.Y / b, a.Z / b);
        }
    }
}
