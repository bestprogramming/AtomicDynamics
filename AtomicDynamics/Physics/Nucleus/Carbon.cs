namespace AtomicDynamics
{
    public class Carbon : Nucleus
    {
        public const int Z = 6;

        public Carbon() : base(new Elementary[] {
            new Proton { R = new(-0.312953453157, -0.194223049240, -0.542141573475)},
            new Proton { R = new(0.328798930069, -0.401518943804, -0.400317054022)},
            new Proton { R = new(-0.198148819914, -0.624542542919, -0.016235751922)},
            new Proton { R = new(0.198148819914, 0.624542542919, 0.016235751922)},
            new Proton { R = new(-0.328798930069, 0.401518943804, 0.400317054022)},
            new Proton { R = new(0.312953453157, 0.194223049240, 0.542141573475)},
            new Neutron { R = new(0.207941863214, 0.256353742692, -0.566235712862)},
            new Neutron { R = new(-0.393699661867, 0.439917824102, -0.284697781292)},
            new Neutron { R = new(0.644677506554, 0.104506020969, -0.055220888379)},
            new Neutron { R = new(-0.644677506554, -0.104506020969, 0.055220888379)},
            new Neutron { R = new(0.393699661867, -0.439917824102, 0.284697781292)},
            new Neutron { R = new(-0.207941863214, -0.256353742692, 0.566235712862)},
        }.PackingToFm(1000))
        { }

        public override string ToString() => $"Carbon";
    }
}
