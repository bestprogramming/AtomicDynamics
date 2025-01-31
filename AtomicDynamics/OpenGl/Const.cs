using System.Numerics;

namespace AtomicDynamics.OpenGl
{
    public static class Const
    {
        public static readonly Vector4 Red = new(1.0f, 0.0f, 0.0f, 1.0f);
        public static readonly Vector4 Green = new(0.0f, 1.0f, 0.0f, 1.0f);
        public static readonly Vector4 Blue = new(0.0f, 0.0f, 1.0f, 1.0f);
        public static readonly Vector4 Brown = new(0.58f, 0.29f, 0.0f, 1.0f);
        public static readonly Vector4 TraceColor = new(1.0f, 1.0f, 0.0f, 0.1f);

        public const float ElectronPointSize = 3.0f;
        public const float ProtonPointSize = 3.0f;
        public const float NeutronPointSize = 3.0f;
    }
}
