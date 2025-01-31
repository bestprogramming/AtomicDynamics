#if !NO_COLLISION_DR_SPIN
using Silk.NET.OpenGL;
using static AtomicDynamics.Physics;
using static AtomicDynamics.OpenGl.Const;
using System.Numerics;
using static AtomicDynamics.Big;

namespace AtomicDynamics
{
    public class Electron : Elementary
    {
        public static new readonly Big E = Physics.E;
        public static new readonly Big M = Parse("9.109383713928") * Pow(10, -31);
        public static new readonly Big Radius = Parse("2.8179403227") * Fm;

        public override Elementary[] Elementaries => new[] { this };

        public Electron() : base(M, E, Radius)
        {
            Vertices =
            [
                new(Vector3.Zero, Green, 3.0f),
            ];
        }

        public override string ToString() => $"Electron";
    }
}
#endif