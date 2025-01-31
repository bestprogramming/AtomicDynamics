using Silk.NET.OpenGL;
using System.Numerics;
using static AtomicDynamics.Big;
using static AtomicDynamics.Physics;

namespace AtomicDynamics
{
    public class Elementary : Particle
    {
        public Rigid? Rigid;
        
        protected Vector? absR;
        public Vector AbsR
        {
            get
            {
                absR ??= Rigid != null ? Rigid.R + R : R;
                return absR;
            }
            set => absR = value;
        }

        public Vector F = new(0, 0, 0);

        public Elementary() { }
        public Elementary(in Big m, in Big e, Big radius) : base(m, e, radius) { }

        public override string ToString() => $"Elementary";

        public unsafe override void Bind(GL gl)
        {
            base.Bind(gl);
            gl.DrawArrays(PrimitiveType.Points, 0, (uint)Vertices.Length);
        }

        public override void SetVerticesPosition(Vector3 r)
        {
            Vertices[0].Position = r;
            Trace.SetPosition(r);
        }

        public override void SetV(in Big dt) => V += F * dt / M;

        public override void Next(in Big dt)
        {
            R += V * dt;

            absR = null;
            F = new(0, 0, 0);
        }

        public static Big Gmm(Elementary e1, Elementary e2)
        {
            if (e1 is Electron && e2 is Electron) return GMeMe;
            else if (e1 is Electron && e2 is Proton || e1 is Proton && e2 is Electron) return GMeMp;
            else if (e1 is Electron && e2 is Neutron || e1 is Neutron && e2 is Electron) return GMeMn;
            else if (e1 is Proton && e2 is Proton) return GMpMp;
            else if (e1 is Proton && e2 is Neutron || e1 is Neutron && e2 is Proton) return GMpMn;
            else if (e1 is Neutron && e2 is Neutron) return GMnMn;

            return Zero;
        }

        public static Big Kee(Elementary e1, Elementary e2)
        {
            if (e1 is Electron && e2 is Electron) return KEeEe;
            else if (e1 is Electron && e2 is Proton || e1 is Proton && e2 is Electron) return KEeEp;
            else if (e1 is Proton && e2 is Proton) return KEpEp;

            return Zero;
        }

        public static Vector GetF(Elementary e1, Elementary e2)
        {
            var r = e1.AbsR - e2.AbsR;
            var d2 = r.X * r.X + r.Y * r.Y + r.Z * r.Z;
            var l = Sqrt(d2);
            var n = r / l;
            var fg = GActive ? n * Gmm(e1, e2) / d2 : Vector.Zero;
            var fe = EActive ? n * Kee(e1, e2) / d2 : Vector.Zero;
            Log.Max(fg.Length);
            return fg + fe;
        }
    }
}
