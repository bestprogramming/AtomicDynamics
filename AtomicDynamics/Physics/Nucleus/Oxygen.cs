﻿namespace AtomicDynamics
{
    public class Oxygen : Nucleus
    {
        public const int Z = 8;

        public Oxygen() : base(new Elementary[] {
            new Proton { R = new(0.000000000000, 0.000000000000, 0.000000000000) },
            new Neutron { R = new(0.505126125522, 0.224227033540, -0.411489991352) },
            new Proton { R = new(-0.000000036727, -0.000000034634, 0.689024076675) },
            new Neutron { R = new(-0.525013839268, 0.414619344286, -0.164940735293) },
            new Proton { R = new(0.673906632780, -0.000000084500, 0.143541034319) },
            new Neutron { R = new(0.482073889397, -0.387802074551, -0.303263077827) },
            new Proton { R = new(-0.545390246599, -0.315324654832, -0.279059167933) },
            new Neutron { R = new(-0.636676420026, -0.023251285394, 0.262405587114) },
            new Proton { R = new(-0.077892846145, 0.344593429731, -0.591559169435) },
            new Neutron { R = new(-0.050847076378, -0.670245746815, -0.151457558207) },
            new Proton { R = new(0.355150581792, -0.512828059316, 0.292625398879) },
            new Neutron { R = new(-0.290865438385, 0.472680084392, 0.408319743350) },
            new Proton { R = new(0.029501882843, 0.683576452004, -0.081283770903) },
            new Neutron { R = new(0.330507985008, 0.445862771856, 0.408319775123) },
            new Proton { R = new(-0.255612272926, -0.492637360995, 0.408319696708) },
            new Neutron { R = new(-0.034233078691, -0.274567470698, -0.631026923828) },
        }.PackingToFm(4))
        { }

        public override string ToString() => $"Oxygen";
    }
}
