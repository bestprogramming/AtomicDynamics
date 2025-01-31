using Silk.NET.OpenGL;
using static AtomicDynamics.Physics;

namespace AtomicDynamics.OpenGl
{
    public class Ucs : Shape
    {
        public Ucs(float length)
        {
            Vertices =
            [
                new(new(0.0f, 0.0f, 0.0f), new(1.0f, 0.0f, 0.0f, 0.4f)),
                new(new(length, 0.0f, 0.0f), new(1.0f, 0.0f, 0.0f, 0.4f)),

                new(new(0.0f, 0.0f, 0.0f), new(0.0f, 1.0f, 0.0f, 0.4f)),
                new(new(0.0f, length, 0.0f), new(0.0f, 1.0f, 0.0f, 0.4f)),

                new(new(0.0f, 0.0f, 0.0f), new(0.0f, 0.0f, 1.0f, 0.4f)),
                new(new(0.0f, 0.0f, length), new(0.0f, 0.0f, 1.0f, 0.4f)),
            ];
        }

        public unsafe override void Bind(GL gl)
        {
            base.Bind(gl);
            gl.DrawArrays(PrimitiveType.Lines, 0, (uint)Vertices.Length);
        }
    }
}
