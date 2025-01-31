namespace AtomicDynamics
{
    public class Lithium : Nucleus
    {
        public const int Z = 3;

        public Lithium() : base(new Elementary[] {
            new Proton { R = new(0.585786437627, 0, 0) },
            new Proton { R = new(0, -0.585786437627, 0) },
            new Proton { R = new(0, 0, -0.585786437627) },
            new Neutron { R = new(0, 0, 0.585786437627) },
            new Neutron { R = new(0, 0.585786437627, 0) },
            new Neutron { R = new(-0.585786437627, 0, 0) },
        }.PackingToFm(4))
        { }

        public override string ToString() => $"Lithium";
    }
}
