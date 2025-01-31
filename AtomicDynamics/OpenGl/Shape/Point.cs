using Silk.NET.OpenGL;
using System.Numerics;

namespace AtomicDynamics.OpenGl
{
    public class Point : Shape
    {
        public Point(double x, double y, double z, Vector4 color, double pointSize = 1)
        {
            Vertices =
            [
                new(new((float)x, (float)y, (float)z), color, (float)pointSize),
            ];
        }

        public unsafe override void Bind(GL gl)
        {
            base.Bind(gl);
            gl.DrawArrays(PrimitiveType.Points, 0, (uint)Vertices.Length);
        }
    }

}
