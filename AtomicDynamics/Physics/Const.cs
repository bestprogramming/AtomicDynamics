using static AtomicDynamics.Big;

namespace AtomicDynamics
{
    //https://en.wikipedia.org/wiki/List_of_physical_constants
    public static partial class Physics
    {
        public static bool EActive = true;
        public static bool GActive = false;

        public static readonly Big Fm = Pow(10, -15);

        public static readonly Big C = Parse("299792458");
        public static readonly Big C_2 = C * C;
        public static readonly Big H = Parse("6.62607015") * Pow(10, -34);
        public static readonly Big HDiv2Pi = H / Pi2; //1.054572E-34
        public static readonly Big HDiv2Pi_2 = HDiv2Pi * HDiv2Pi;
        public static readonly Big M0 = Parse("1.2566370612720") * Pow(10, -6);
        public static readonly Big E0 = One / (M0 * C_2);

        public static readonly Big E = Parse("-1.602176634") * Pow(10, -19);
        public static readonly Big E_2 = E * E;
        public static readonly Big Ke = One / (4 * Pi * E0);

        public static readonly Big G = Parse("-6.6743015") * Pow(10, -11);
        public static readonly Big GMeMe = G * Electron.M * Electron.M;
        public static readonly Big GMeMp = G * Electron.M * Proton.M;
        public static readonly Big GMeMn = G * Electron.M * Neutron.M;
        public static readonly Big GMpMp = G * Proton.M * Proton.M;
        public static readonly Big GMpMn = G * Proton.M * Neutron.M;
        public static readonly Big GMnMn = G * Neutron.M * Neutron.M;

        public static readonly Big K = Parse("8.987551792314") * Pow(10, 9);
        public static readonly Big KEeEe = K * Electron.E * Electron.E;
        public static readonly Big KEeEp = K * Electron.E * Proton.E;
        public static readonly Big KEpEp = K * Proton.E * Proton.E;

        public static readonly Big BohrRadius = HDiv2Pi * HDiv2Pi / (Ke * E_2 * Electron.M); //5.291772E-11
        public static Big BohrRadiusN(int n, int z) => n * n * BohrRadius / z;
        public static Big BohrRatio = BohrRadiusN(4, 1);
        public static Big Zoom = 2 / BohrRatio;

        public static Big ElectronSpeed(int z, Big r) => Sqrt(z * Ke * E_2 / (Electron.M * r));

        public static readonly Big PlankTime = Parse("5.39124760") * Pow(10, -44);
    }
}
