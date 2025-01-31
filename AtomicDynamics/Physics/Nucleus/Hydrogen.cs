namespace AtomicDynamics
{
    public class Hydrogen : Nucleus
    {
        public const int Z = 1;

        public Hydrogen() : base(new Elementary[] {
            new Proton { R = new(0, 0, 0) },
        }.PackingToFm(4))
        { }

        public override string ToString() => $"Hydrogen";
    }
}
