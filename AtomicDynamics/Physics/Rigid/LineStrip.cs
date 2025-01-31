using AtomicDynamics.OpenGl;
using static AtomicDynamics.Big;
using static AtomicDynamics.Physics;
using static AtomicDynamics.OpenGl.Const;
using Silk.NET.OpenGL;
using System.Numerics;

namespace AtomicDynamics
{
    public class LineStrip : Rigid
    {
        public LineStrip(Elementary[] elementaries) : base(elementaries)
        {
            Vertices = [.. Vertices, .. Vertices.Select(p => p.SetW(0.3f)) ];
            VertextPositions = Vertices.Select(p => p.Position).ToArray();
        }

        public override string ToString() => $"LineStrip";

        public override void SetVertextPosition(int index, Vector3 v)
        {
            VertextPositions[index] = v;
            VertextPositions[index + Elementaries.Length] = v;
        }

        public unsafe override void Bind(GL gl)
        {
            base.Bind(gl);
            var length = Elementaries.Length;
            gl.DrawArrays(PrimitiveType.Points, 0, (uint)length);
            gl.DrawArrays(PrimitiveType.LineStrip, 2, (uint)length);
        }
    }
}
