using Silk.NET.OpenGL;

namespace AtomicDynamics.OpenGl
{
    public class VertexArrayObject<TVertexType, TIndexType> : IDisposable
        where TVertexType : unmanaged
        where TIndexType : unmanaged
    {
        private readonly uint handle;
        private readonly GL gl;

        public VertexArrayObject(GL gl)
        {
            this.gl = gl;

            handle = this.gl.GenVertexArray();
            Bind();
        }

        public unsafe void VertexAttributePointer(uint index, int count, VertexAttribPointerType type, uint vertexSize, int offSet)
        {
            //gl.VertexAttribPointer(index, count, type, false, vertexSize * (uint) sizeof(TVertexType), (void*) (offSet * sizeof(TVertexType)));
            gl.VertexAttribPointer(index, count, type, false, vertexSize * sizeof(float), (void*)(offSet * sizeof(float)));
            gl.EnableVertexAttribArray(index);
        }

        public void Bind()
        {
            gl.BindVertexArray(handle);
        }

        public void Dispose()
        {
            gl.DeleteVertexArray(handle);
        }
    }
}
