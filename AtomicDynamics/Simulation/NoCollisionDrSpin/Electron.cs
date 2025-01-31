#if NO_COLLISION_DR_SPIN
using Silk.NET.OpenGL;
using static AtomicDynamics.Physics;
using static AtomicDynamics.OpenGl.Const;
using System.Numerics;
using static AtomicDynamics.Big;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AtomicDynamics
{
    public class Electron : Elementary
    {
        public static new readonly Big E = Physics.E;
        public static new readonly Big M = Parse("9.109383713928") * Pow(10, -31);
        public static new readonly Big Radius = Parse("2.8179403227") * Fm;
        
        public override Elementary[] Elementaries => new[] { this };


        private readonly Vector?[] rs = new Vector?[3];


        public Electron() : base(M, E, Radius)
        {
            Vertices =
            [
                new(Vector3.Zero, Green, 3.0f),
            ];
        }

        public override string ToString() => $"Electron";

        public unsafe override void Bind(GL gl)
        {
            base.Bind(gl);
            gl.DrawArrays(PrimitiveType.Points, 0, (uint)Vertices.Length);
        }


        public override void SetV(in Big dt)
        {
            Vector? c = null;
            if (rs[0] is null)
            {
                rs[0] = R;
            }
            else if (rs[1] is null)
            {
                rs[1] = R;
            }
            else if (rs[2] is null)
            {
                rs[2] = R;
            }
            else
            {
                rs[0] = rs[1];
                rs[1] = rs[2];
                rs[2] = R;

                //c = Vector.CircularCenter(rs[0]!, rs[1]!, rs[2]!);
                c = Vector.Zero;
            }

            if (c is null)
            {
                V += F * dt / M;
            }
            else
            {
                var ur = (R - c).Normal;
                V += F * dt / M;
                var q = Quaternion.CreateFromAxisAngle(ur, Rand.Double(-0.005, 0.005));
                V = Vector.Transform(V, q);
            }
        }

        public override void Next(in Big dt)
        {
            R += V * dt;
            absR = null;
            F = new(0, 0, 0);
        }
    }
}
#endif