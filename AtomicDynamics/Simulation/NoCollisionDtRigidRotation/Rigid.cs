#if NO_COLLISION_DT_RIGID_ROTATION
using AtomicDynamics.OpenGl;
using static AtomicDynamics.Big;
using static AtomicDynamics.Physics;
using static AtomicDynamics.OpenGl.Const;
using System.Numerics;

namespace AtomicDynamics
{
    public class Rigid : Particle
    {
        public override Elementary[] Elementaries { get; }

        public Vector3[] VertextPositions;

        public Vector W = new(0, 0, 0);

        public Rigid(Elementary[] elementaries)
        {
            Elementaries = elementaries;

            M = Elementaries.Aggregate(Zero, (c, n) => c + n.M);

            R = new Vector(
                Elementaries.Aggregate(Zero, (c, n) => c + n.R.X * n.M) / M,
                Elementaries.Aggregate(Zero, (c, n) => c + n.R.Y * n.M) / M,
                Elementaries.Aggregate(Zero, (c, n) => c + n.R.Z * n.M) / M);

            foreach (var e in Elementaries)
            {
                e.Rigid = this;

                e.R -= R;
                E += e.E;
            }

            var vertices = new List<Vertex>();

            foreach (var e in Elementaries)
            {
                var (color, pointSize) = 
                    e is Electron ? (Green, ElectronPointSize) :
                    e is Proton ? (Red, ProtonPointSize) :
                    e is Neutron ? (Blue, NeutronPointSize) : throw new NotImplementedException();

                vertices.Add(new(e.R.ToVector3(), color, pointSize));
            }

            Vertices = [.. vertices];

            VertextPositions = Vertices.Select(p => p.Position).ToArray();
        }

        public virtual void SetVertextPosition(int index, Vector3 v) => VertextPositions[index] = v;

        public override void SetVerticesPosition(Vector3 r)
        {
            for (var a = 0; a < Vertices.Length; a++) Vertices[a].Position = VertextPositions[a] + r;
            Trace.SetPosition(r);
        }

        public override void SetV(in Big dt)
        {
            for (var a = 0; a < Elementaries.Length; a++)
            {
                var e = Elementaries[a];

                if (e.F.Length != 0)
                {
                    V += e.F * dt / M;

                    W += GetElementaryW(a, dt);
                }
            }
        }

        public override void Next(in Big dt)
        {
            R += V * dt;

            var ang = Zero;
            var q = Quaternion.Zero;

            if (W.Length != 0)
            {
                ang = W.Length * dt;// * Pow(10, 16);
                q = Quaternion.CreateFromAxisAngle(W.Normal, ang);
            }

            for (var a = 0; a < Elementaries.Length; a++)
            {
                var e = Elementaries[a];

                if (ang != Zero)
                {
                    e.R = Vector.Transform(e.R, q);
                    SetVertextPosition(a, e.R.ToVector3());
                }

                e.AbsR = null!;
                e.F = new(0, 0, 0);
            }
        }

        public Vector GetElementaryW(int index, in Big dt)
        {
            var elementary = Elementaries[index];
            var w = Vector.Cross(elementary.R, elementary.F);

            var i = Zero;

            var elementaryR = Zero;
            for (var a = 0; a < Elementaries.Length; a++)
            {
                var e = Elementaries[a];
                var r = Vector.Perpendicular(e.R, w).Length;
                if (a == index) elementaryR = r;
                i += e.M * r * r;
            }

            w *= dt * elementaryR / i;

            return w;
        }
    }
}
#endif