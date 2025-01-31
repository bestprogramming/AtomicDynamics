using AtomicDynamics;
using AtomicDynamics.OpenGl;
using System.Numerics;
using Xunit.Abstractions;
using static AtomicDynamics.Physics;
using static AtomicDynamics.Big;


namespace Tests
{
    //http://hyperphysics.phy-astr.gsu.edu/hbase/spin.html
    public class ElectronSpinTest
    {
        //l:angular momentum quantum number, L: angular momentum, L = Sqrt(l * (l + 1)) * HDiv2Pi
        //ml:magnetic quantum number, ml=-l,-l+1...l-1,l  Lz = ml * HDiv2Pi
        //Principal quantum number: electron's state, energy levels , n:1.2.3.4 ... K,L,M,N //
        //Angular Momentum Quantization: L = n * H / Pi2 http://hyperphysics.phy-astr.gsu.edu/hbase/Bohr.html#c2

        public static readonly Big S = Sqrt3 / 2 * HDiv2Pi; //9.132860E-35
        public static readonly Big Sz = One / 2 * HDiv2Pi; //5.272859E-35


        [Fact]
        public void T1()
        {            
            var mb = E * HDiv2Pi / (2 * Electron.M);//Bohr magneton:-9.274010E-24 //http://hyperphysics.phy-astr.gsu.edu/hbase/spin.html
            var l = 0;
            var m = Sqrt(l * (l + 1)) * mb; //Orbital Magnetic Moment:-1.311543E-23 //http://hyperphysics.phy-astr.gsu.edu/hbase/quantum/orbmag.html#c2

            var j = 1;
            var s = 1;
            var gl = 1 + (j * (j + 1) + s * (s + 1) - l * (l + 1)) / (2 * (j + 1)); //Lande' g-Factor
            var g = Parse("2.002319304386"); //The Electron Spin g-factor //http://hyperphysics.phy-astr.gsu.edu/hbase/quantum/zeeman.html#c5

            var n = 1;
            var z = 1;
            var r = BohrRadiusN(n, z);
            var v = ElectronSpeed(z, r);
            var b = M0 * z * E * v / (4 * Pi * r * r); //Effective Magnetic Field of Orbit:-1.251682E+1 //http://hyperphysics.phy-astr.gsu.edu/hbase/quantum/hydfin.html#c2


            var aa = 2 * m * b / HDiv2Pi;
            var we = 1.7608 * Pow(10, 11); //Electron spin rad/s //http://hyperphysics.phy-astr.gsu.edu/hbase/magnetic/larmor.html#c1
            var wp = 2.6753 * Pow(10, 11); //Proton spin rad/s 
        }
    }
}