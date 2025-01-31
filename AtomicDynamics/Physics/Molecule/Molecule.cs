namespace AtomicDynamics
{
    public class Molecule : Particle
    {
        public Nucleus[] Nucleuses;

        public Molecule(Nucleus[] nucleuses)
        {
            Nucleuses = nucleuses;

            foreach (var n in Nucleuses)
            {
                M += n.M;
            }
        }
    }
}
