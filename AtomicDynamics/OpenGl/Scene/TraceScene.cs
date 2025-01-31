using AtomicDynamics.OpenGl;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System.Numerics;
using Shader = AtomicDynamics.OpenGl.Shader;

namespace AtomicDynamics
{
    public class TraceScene(Action<TraceScene> action) : IDisposable
    {
        public string Title = "Trace Scene";
        public int Width = 1920;
        public int Height = 1080;

        private static bool showUcs = true;
        private readonly Ucs ucs = new(0.01f);
        public readonly Trace Trace = new();

        private static IWindow? window;
        public Vector3 CameraPosition = new(0.63029486f, -0.6481463f, 0.42735794f);
        public Vector3 CameraTarget = new(0.0f, 0.0f, 0.0f);
        public Vector3 CameraUp = new(-0.2979391f, 0.3063774f, 0.90408254f);
        public float CameraYaw = 135.79999f;
        public float CameraPitch = 25.300005f;
        public float CameraZoom = 45f;
        private Vector2 lastPosition;
        private bool perspective = false;

        public void Dispose()
        {
            ucs.Dispose();
            Trace.Dispose();
            GC.SuppressFinalize(this);
        }

        public void Start()
        {
            var options = WindowOptions.Default;
            options.Size = new Vector2D<int>(Width, Height);
            options.Title = Title;
            options.WindowState = WindowState.Maximized;

            window = Window.Create(options);

            window.Load += () =>
            {
                Load(window);
            };
            window.Run();
            window.Dispose();
        }

        private void Load(IWindow window)
        {
            var gl = GL.GetApi(window);

            var shader = new Shader(gl);

            window.Render += (deltaTime) =>
            {
                Render(gl, shader);
            };

            var input = window.CreateInput();
            var keyboard = input.Keyboards[0];
            if (keyboard != null)
            {
                keyboard.KeyDown += (keyboard, key, arg3) =>
                {
                    KeyDown(window, gl, keyboard, key, arg3);
                };
            }
            for (int i = 0; i < input.Mice.Count; i++)
            {
                input.Mice[i].Cursor.CursorMode = CursorMode.Normal;
                input.Mice[i].MouseMove += MouseMove;
                input.Mice[i].Scroll += MouseWheel;
            }
        }

        private unsafe void Render(GL gl, Shader shader)
        {
            action.Invoke(this);

            gl.Enable(EnableCap.Blend);
            gl.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            gl.Enable(EnableCap.CullFace);

            gl.Enable(EnableCap.DepthTest);
            gl.Clear((uint)(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit));

            gl.Enable(EnableCap.ProgramPointSize);

            shader.Use();

            var model = Matrix4x4.Identity; //Matrix4x4.CreateRotationX(0);
            var view = Matrix4x4.CreateLookAt(CameraPosition, CameraTarget, CameraUp);
            var projection = perspective ? Matrix4x4.CreatePerspectiveFieldOfView(Conversion.Rad(CameraZoom), 1f * Width / Height, 0.01f, 100.0f) :
                Matrix4x4.CreateOrthographic(CameraZoom * Width / 100000f, CameraZoom * Height / 100000f, 0.01f, 100.0f);

            shader.SetUniform("uModel", model);
            shader.SetUniform("uView", view);
            shader.SetUniform("uProjection", projection);

            if (showUcs) ucs.Bind(gl);
            Trace.Bind(gl);
        }

        private void KeyDown(IWindow window, GL gl, IKeyboard keyboard, Key key, int arg3)
        {
            if (key == Key.Escape)
            {
                window.Close();
            }
            else if (key == Key.F1)
            {
                showUcs = !showUcs;
            }
            else if (key == Key.F3)
            {
                perspective = !perspective;
            }
        }

        private unsafe void MouseMove(IMouse mouse, Vector2 position)
        {
            if (mouse.IsButtonPressed(MouseButton.Left))
            {
                var lookSensitivity = 0.1f;

                var xOffset = (position.X - lastPosition.X) * lookSensitivity;
                var yOffset = (position.Y - lastPosition.Y) * lookSensitivity;

                CameraYaw += xOffset;
                CameraPitch += yOffset;
                CameraPitch = Math.Clamp(CameraPitch, -89.9999f, 89.9999f);

                CameraPosition = new Vector3(
                    MathF.Sin(Conversion.Rad(CameraYaw)) * MathF.Cos(Conversion.Rad(CameraPitch)),
                    MathF.Cos(Conversion.Rad(CameraYaw)) * MathF.Cos(Conversion.Rad(CameraPitch)),
                    MathF.Sin(Conversion.Rad(CameraPitch)));
                CameraPosition = Vector3.Normalize(CameraPosition);

                var right = Vector3.Normalize(Vector3.Cross(CameraPosition, Vector3.UnitZ));

                CameraUp = Vector3.Normalize(Vector3.Cross(right, CameraPosition));
            }
            else if (mouse.IsButtonPressed(MouseButton.Middle))
            {
                var panSensitivity = 0.000018f * CameraZoom;

                var xOffset = (position.X - lastPosition.X) * panSensitivity;
                var yOffset = (position.Y - lastPosition.Y) * panSensitivity;

                CameraYaw += xOffset;
                CameraPitch += yOffset;
                CameraPitch = Math.Clamp(CameraPitch, -89.9999f, 89.9999f);

                CameraPosition = new Vector3(
                    MathF.Sin(Conversion.Rad(CameraYaw)) * MathF.Cos(Conversion.Rad(CameraPitch)),
                    MathF.Cos(Conversion.Rad(CameraYaw)) * MathF.Cos(Conversion.Rad(CameraPitch)),
                    MathF.Sin(Conversion.Rad(CameraPitch)));
                CameraPosition = Vector3.Normalize(CameraPosition);

                var right = Vector3.Normalize(Vector3.Cross(CameraPosition, Vector3.UnitZ));

                var up = CameraUp * yOffset;

                CameraPosition += right * xOffset + up;
                CameraTarget += right * xOffset + up;

                CameraUp = Vector3.Normalize(Vector3.Cross(right, CameraPosition));
            }

            lastPosition = position;
        }

        private unsafe void MouseWheel(IMouse mouse, ScrollWheel scrollWheel)
        {
            CameraZoom = Math.Clamp(CameraZoom * (1 - scrollWheel.Y * 0.1f), 0.00001f, 90f);
        }
    }
}
