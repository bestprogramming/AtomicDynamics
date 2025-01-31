using Silk.NET.OpenGL;
using System.Numerics;
using static AtomicDynamics.Physics;

namespace AtomicDynamics.OpenGl
{
    public class Shape : IDisposable
    {
        public Vertex[] Vertices = [];
        protected uint[] Indices = [];

        protected BufferObject<Vertex>? Vbo;
        protected BufferObject<uint>? Ebo;
        protected VertexArrayObject<Vertex, uint>? Vao;

        public Trace Trace = new();

        public virtual void Dispose()
        {
            Ebo?.Dispose();
            Vbo?.Dispose();
            Vao?.Dispose();
            
            GC.SuppressFinalize(this);
        }

        public virtual void Bind(GL gl)
        {
            Trace.Bind(gl);

            Ebo?.Dispose();
            Vbo?.Dispose();
            Vao?.Dispose();

            if (Vertices.Length > 0)
            {
                Vbo = new(gl, Vertices, BufferTargetARB.ArrayBuffer);
            }

            if (Indices.Length > 0)
            {
                Ebo = new(gl, Indices, BufferTargetARB.ElementArrayBuffer);
            }

            Vao = new(gl);
            Vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 8, 0);
            Vao.VertexAttributePointer(1, 4, VertexAttribPointerType.Float, 8, 3);
            Vao.VertexAttributePointer(2, 1, VertexAttribPointerType.Float, 8, 7);
            
            Vao?.Bind();
            Vbo?.Bind();
            Ebo?.Bind();
        }

        public virtual void SetVerticesPosition(Vector3 r) => throw new NotImplementedException();
    }
}
