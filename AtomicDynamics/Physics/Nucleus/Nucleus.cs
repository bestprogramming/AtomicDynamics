using static AtomicDynamics.Physics;
using AtomicDynamics.OpenGl;
using static AtomicDynamics.OpenGl.Const;
using static AtomicDynamics.Big;
using Silk.NET.OpenGL;
using System.Numerics;

namespace AtomicDynamics
{
    public class Nucleus : Rigid
    {
        public Nucleus(Elementary[] elementaries) : base(elementaries) { }

        public override string ToString() => $"Nucleus";

        public unsafe override void Bind(GL gl)
        {
            base.Bind(gl);
            gl.DrawArrays(PrimitiveType.Points, 0, (uint)Vertices.Length);
        }
    }
}
