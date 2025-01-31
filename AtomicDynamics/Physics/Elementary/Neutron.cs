using static AtomicDynamics.Physics;
using static AtomicDynamics.OpenGl.Const;
using Silk.NET.OpenGL;
using System.Numerics;
using static AtomicDynamics.Big;

namespace AtomicDynamics
{
    public class Neutron : Elementary
    {
        public static new readonly Big E = Zero;
        public static new readonly Big M = Parse("1.6749275005685") * Pow(10, -27);
        public static new readonly Big Radius = Parse("0.8") * Fm;

        public override Elementary[] Elementaries => new[] { this };

        public Neutron() : base(M, E, Radius)
        {
            Vertices =
            [
                new(Vector3.Zero, Red, 3.0f),
            ];
        }

        public override string ToString() => $"Neutron{Rigid?.GetType()?.Name?.RoundBrackets()}";
    }
}
