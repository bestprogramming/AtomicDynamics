using Silk.NET.OpenGL;
using System.Numerics;
using static AtomicDynamics.OpenGl.Const;

namespace AtomicDynamics.OpenGl
{
    public class Trace : IDisposable
    {
        public bool Enabled;
        public float MinDistance;
        public Vector3 Position;
        public Vector4 Color = TraceColor;

        private Vertex[]? vertices;
        public Vertex[] Vertices => vertices ??= [.. VerticesList];
        public List<Vertex> VerticesList = [];

        protected BufferObject<Vertex>? Vbo;
        protected BufferObject<uint>? Ebo;
        protected VertexArrayObject<Vertex, uint>? Vao;

        public Trace() { }

        public Trace(bool enabled, float minDistance)
        {
            Enabled = enabled;
            MinDistance = minDistance;
        }

        public virtual void Dispose()
        {
            Ebo?.Dispose();
            Vbo?.Dispose();
            Vao?.Dispose();

            GC.SuppressFinalize(this);
        }

        public unsafe void Bind(GL gl)
        {
            if (Enabled && Vertices.Length > 0)
            {
                Ebo?.Dispose();
                Vbo?.Dispose();
                Vao?.Dispose();

                Vbo = new(gl, Vertices, BufferTargetARB.ArrayBuffer);

                Vao = new(gl);
                Vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 8, 0);
                Vao.VertexAttributePointer(1, 4, VertexAttribPointerType.Float, 8, 3);
                Vao.VertexAttributePointer(2, 1, VertexAttribPointerType.Float, 8, 7);

                Vao?.Bind();
                Vbo?.Bind();
                Ebo?.Bind();

                gl.DrawArrays(PrimitiveType.LineStrip, 0, (uint)Vertices.Length);
            }
        }

        public void Add(Vector3 p, double pointSize = 1)
        {
            VerticesList.Add(new(p, Color, (float)pointSize));
            vertices = null;
        }

        public void SetPosition(Vector3 v)
        {
            if (Enabled)
            {
                if (VerticesList.Count == 0)
                {
                    Position = v;
                    Add(Position, 1);
                }
                else if ((v - Position).Length() > MinDistance)
                {
                    Position = v;
                    Add(Position, 1);
                }
            }
        }
    }
}
