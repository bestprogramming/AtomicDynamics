using AtomicDynamics.OpenGl;
using static AtomicDynamics.Physics;
using static AtomicDynamics.Big;

namespace AtomicDynamics
{
    public class Particle : Shape
    {
        private Vector r = new(0, 0, 0);
        public Vector R
        {
            get => r;
            set
            {
                r = value;
                SetVerticesPosition(r.ToVector3());
            }
        }

        public Vector V = new(0, 0, 0);
        public Big M = Zero;
        public Big E = Zero;
        public Big Radius = Zero;

        public virtual Elementary[] Elementaries { get; } = [];

        public Particle() { }

        public Particle(in Big m, in Big e, Big radius)
        {
            M = m;
            E = e;
            Radius = radius;
        }

        public virtual void SetV(in Big dt) => throw new NotImplementedException();

        public virtual void Next(in Big dt) => throw new NotImplementedException();

        public static IEnumerable<Interaction> GetInteractions(IEnumerable<Particle> particles)
        {
            for (var a = 0; a < particles.Count() - 1; a++)
            {
                var p1 = particles.ElementAt(a);
                for (var b = a + 1; b < particles.Count(); b++)
                {
                    var p2 = particles.ElementAt(b);
                    
                    foreach (var e1 in p1.Elementaries)
                    {
                        foreach (var e2 in p2.Elementaries)
                        {
                            yield return new(e1, e2);
                        }
                    }
                }
            }
        }
    }
}
