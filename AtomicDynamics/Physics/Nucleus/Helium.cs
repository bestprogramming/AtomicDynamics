namespace AtomicDynamics
{
    public class Helium : Nucleus
    {
        public const int Z = 2;

        public Helium() : base(new Elementary[] {
            new Proton { R = new(-0.259513023994, 0.449489742783, -0.183503419072) },
            new Proton { R = new(-0.259513023994, -0.449489742783, -0.183503419072) },
            new Neutron { R = new(0.519026047988, 0, -0.183503419072) },
            new Neutron { R = new(0, 0, 0.550510257217) },
        }.PackingToFm(4))
        { }

        public override string ToString() => $"Helium";
    }
}
