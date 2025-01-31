using Silk.NET.OpenGL;
using System.Numerics;

namespace AtomicDynamics.OpenGl
{
    public class Shader : IDisposable
    {
        private readonly uint handle;
        private readonly GL gl;

        public Shader(GL gl)
        {
            this.gl = gl;

            var vertex = LoadShader(ShaderType.VertexShader, @"#version 330 core
layout (location = 0) in vec3 inPosition;
layout (location = 1) in vec4 inColor;
layout (location = 2) in float inPointSize;

uniform mat4 uModel;
uniform mat4 uView;
uniform mat4 uProjection;

out vec4 color;

void main(void)
{
    gl_Position = uProjection * uView * uModel * vec4(inPosition, 1.0);
    color = inColor;
    gl_PointSize = inPointSize;
}");

            var fragment = LoadShader(ShaderType.FragmentShader, @"#version 330
out vec4 outColor;
in vec4 color;

void main()
{
    outColor = color;
}");
            
            handle = gl.CreateProgram();
            gl.AttachShader(handle, vertex);
            gl.AttachShader(handle, fragment);
            gl.LinkProgram(handle);
            gl.GetProgram(handle, GLEnum.LinkStatus, out var status);
            if (status == 0)
            {
                throw new Exception($"Program failed to link with error: {this.gl.GetProgramInfoLog(handle)}");
            }
            gl.DetachShader(handle, vertex);
            gl.DetachShader(handle, fragment);
            gl.DeleteShader(vertex);
            gl.DeleteShader(fragment);
        }

        public void Use()
        {
            gl.UseProgram(handle);
        }

        public void SetUniform(string name, int value)
        {
            int location = gl.GetUniformLocation(handle, name);
            if (location == -1)
            {
                throw new Exception($"{name} uniform not found on shader.");
            }
            gl.Uniform1(location, value);
        }

        public unsafe void SetUniform(string name, Matrix4x4 value)
        {
            int location = gl.GetUniformLocation(handle, name);
            if (location == -1)
            {
                throw new Exception($"{name} uniform not found on shader.");
            }
            gl.UniformMatrix4(location, 1, false, (float*) &value);
        }

        public void SetUniform(string name, float value)
        {
            int location = gl.GetUniformLocation(handle, name);
            if (location == -1)
            {
                throw new Exception($"{name} uniform not found on shader.");
            }
            gl.Uniform1(location, value);
        }

        public void SetUniform(string name, Vector4 value)
        {
            int location = gl.GetUniformLocation(handle, name);
            if (location == -1)
            {
                throw new Exception($"{name} uniform not found on shader.");
            }
            gl.Uniform4(location, value.X, value.Y, value.Z, value.W);
        }

        public void Dispose()
        {
            gl.DeleteProgram(handle);
            GC.SuppressFinalize(this);
        }

        private uint LoadShader(ShaderType type, string src)
        {
            var handle = gl.CreateShader(type);
            gl.ShaderSource(handle, src);
            gl.CompileShader(handle);
            var infoLog = gl.GetShaderInfoLog(handle);
            if (!string.IsNullOrWhiteSpace(infoLog))
            {
                throw new Exception($"Error compiling shader of type {type}, failed with error {infoLog}");
            }

            return handle;
        }
    }
}
