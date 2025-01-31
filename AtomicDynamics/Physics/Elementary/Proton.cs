using static AtomicDynamics.Physics;
using static AtomicDynamics.OpenGl.Const;
using Silk.NET.OpenGL;
using System.Numerics;
using static AtomicDynamics.Big;

namespace AtomicDynamics
{
    public class Proton : Elementary
    {
        public static new readonly Big E = -Physics.E;
        public static new readonly Big M = Parse("1.6726219259552") * Pow(10, -27);
        public static new readonly Big Radius = Parse("0.841419") * Fm;

        public override Elementary[] Elementaries => new[] { this };

        public Proton() : base(M, E, Radius) 
        {
            Vertices =
            [
                new(Vector3.Zero, Red, 3.0f),
            ];
        }

        public override string ToString() => $"Proton{Rigid?.GetType()?.Name?.RoundBrackets()}";
    }
}
