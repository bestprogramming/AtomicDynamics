#if DEBUG
using AtomicDynamics.OpenGl;
using static AtomicDynamics.Big;
using static AtomicDynamics.Physics;
using static AtomicDynamics.OpenGl.Const;

namespace AtomicDynamics
{
    public class Rigid : Particle
    {
        public override Elementary[] Elementaries { get; }

        public Rigid(Elementary[] elementaries)
        {
            Elementaries = elementaries;

            foreach (var e in Elementaries)
            {
                e.Rigid = this;

                e.R -= R;
                M += e.M;
                E += e.E;
            }

            var vertices = new List<Vertex>();
            foreach (var p in Elementaries.OfType<Proton>())
            {
                vertices.Add(new(p.R.ToVector3(), Red, 3.0f));
            }
            foreach (var n in Elementaries.OfType<Neutron>())
            {
                vertices.Add(new(n.R.ToVector3(), Blue, 3.0f));
            }
            Vertices = [.. vertices];

            R = new Vector(
                Elementaries.Aggregate(Zero, (c, n) => c + n.R.X * n.M) / M,
                Elementaries.Aggregate(Zero, (c, n) => c + n.R.Y * n.M) / M,
                Elementaries.Aggregate(Zero, (c, n) => c + n.R.Z * n.M) / M
            );
        }
    }
}
#endif