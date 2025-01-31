using System.Numerics;

namespace AtomicDynamics.OpenGl
{
    public struct Vertex(Vector3 position, Vector4 color, float pointSize = 1)
    {
        public Vector3 Position = position;
        public Vector4 Color = color;
        public float PointSize = pointSize;

        public readonly Vertex SetW(float w) => new(Position, new(Color.X, Color.Y, Color.Z, w), PointSize);
        public readonly Vertex SetColor(Vector4 color) => new(Position, color, PointSize);
    }
}
