namespace AtomicDynamics
{
    public class Beryllium : Nucleus
    {
        public const int Z = 4;

        public Beryllium() : base(new Elementary[] {
            new Proton { R = new(-0.455442510625,0.186711482818,-0.399543152045) },
            new Proton { R = new(0.126932969738,-0.617195950940,0.069863924767) },
            new Proton { R = new(-0.401684484018,0.385811113842,0.302858297529) },
            new Proton { R = new(0.328509540887,0.430484468122,0.329679227278) },
            new Neutron { R = new(0.098056143601,-0.261336791963,-0.569220398893) },
            new Neutron { R = new(0.232440524367,0.437116820745,-0.396010197218) },
            new Neutron { R = new(0.620690844610,-0.098821468427,-0.083072146010) },
            new Neutron { R = new(-0.536068864784,-0.312642498866,0.129648095855) },
            new Neutron { R = new(-0.013434163776,-0.150127175330,0.615796348737) },
        }.PackingToFm(4))
        { }

        public override string ToString() => $"Beryllium";
    }
}
