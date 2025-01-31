#if NO_COLLISION_DR_SPIN
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
            foreach (var e in Elementaries)
            {
                V += e.F * dt / M;
            }
        }


        public override void Next(in Big dt)
        {
            R += V * dt;

            foreach (var e in Elementaries)
            {
                e.AbsR = null!;
                e.F = new(0, 0, 0);
            }
        }
    }
}
#endif