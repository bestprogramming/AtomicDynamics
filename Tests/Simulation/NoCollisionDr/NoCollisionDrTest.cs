#if NO_COLLISION_DR
using AtomicDynamics;
using AtomicDynamics.OpenGl;
using Xunit.Abstractions;
using static AtomicDynamics.Physics;
using static AtomicDynamics.Big;

namespace Tests
{
    public class NoCollisionDrTest(ITestOutputHelper output) : BaseTest(output)
    {
        [Fact]
        public void TwoElectronToCenterT1()
        {
            BohrRatio = BohrRadiusN(4, 1);
            Zoom = 2 / BohrRatio;

            var x = BohrRadius * 1;

            var dr = BohrRadius * 0.005;
            var s = new Simulation(dr);
            //s.Particles.Add(new Proton() { Trace = new(true, 1E-6f) });
            //s.Particles.Add(new Hydrogen() { Trace = new(true, 1E-6f) });
            //s.Particles.Add(new Helium() { Trace = new(true, 1E-6f) });
            //s.Particles.Add(new Carbon() { Trace = new(true, 1E-6f) });
            //s.Particles.Add(new Iron() { Trace = new(true, 1E-6f) });
            s.Particles.Add(new Electron() { R = new(x, 0, 0), V = new(-3000000, 0, 0) });
            s.Particles.Add(new Electron() { R = new(-x, 0, 0), V = new(3000000, 0, 0) });

            var scene = new Scene(s);
            scene.Start();
        }

        [Fact]
        public void ProtonElectronT1()
        {
            BohrRatio = BohrRadiusN(4, 1);
            Zoom = 2 / BohrRatio;

            var x = BohrRadius * 2;
            var y = BohrRadius * 1;

            var dr = BohrRadius * 0.005;
            var s = new Simulation(dr);
            s.Particles.Add(new Proton() { Trace = new(true, 1E-6f) });
            //s.Particles.Add(new Hydrogen() { Trace = new(true, 1E-6f) });
            //s.Particles.Add(new Helium() { Trace = new(true, 1E-6f) });
            //s.Particles.Add(new Carbon() { Trace = new(true, 1E-6f) });
            //s.Particles.Add(new Iron() { Trace = new(true, 1E-6f) });
            s.Particles.Add(new Electron() { R = new(x, y, 0), V = new(-1000000, 0, 0), Trace = new(true, 1E-6f) });

            var scene = new Scene(s);
            scene.Start();
        }

        [Fact]
        public void ProtonTwoElectronT1()
        {
            BohrRatio = BohrRadiusN(4, 1);
            Zoom = 2 / BohrRatio;

            var x = BohrRadius * 2;
            var y = BohrRadius * 1;

            var dr = BohrRadius * 0.005;
            var s = new Simulation(dr);
            s.Particles.Add(new Proton() { Trace = new(true, 1E-6f) });
            //s.Particles.Add(new Hydrogen() { Trace = new(true, 1E-6f) });
            //s.Particles.Add(new Helium() { Trace = new(true, 1E-6f) });
            //s.Particles.Add(new Carbon() { Trace = new(true, 1E-6f) });
            //s.Particles.Add(new Iron() { Trace = new(true, 1E-6f) });
            s.Particles.Add(new Electron() { R = new(x, y, 0), V = new(-1000000, 0, 0), Trace = new(true, 1E-6f) });
            s.Particles.Add(new Electron() { R = new(-x, -y, 0), V = new(1000000, 0, 0), Trace = new(true, 1E-6f) });

            var scene = new Scene(s);
            scene.Start();
        }

        [Fact]
        public void NElectronRandomT1()
        {
            BohrRatio = BohrRadiusN(20, 1);
            Zoom = 2 / BohrRatio;


            var dr = BohrRadius * 0.05;
            var s = new Simulation(dr);
            s.Particles.Add(new Proton() { Trace = new(true, 1E-6f) });
            //s.Particles.Add(new Hydrogen() { Trace = new(true, 1E-6f) });
            //s.Particles.Add(new Helium() { Trace = new(true, 1E-6f) });
            //s.Particles.Add(new Carbon() { Trace = new(true, 1E-6f) });
            //s.Particles.Add(new Iron() { Trace = new(true, 1E-6f) });

            var bigR = 10;
            var smallR = 3;

            var minV = 100000;
            var maxV = 200000;

            for (var a = 0; a < 5; a++)
            {
                var e = new Electron
                {
                    R = Rand.Torus(bigR, smallR) * BohrRadius,
                    //V = new Vector(Rand.Double(minV, maxV), Rand.Double(minV, maxV), Rand.Double(minV, maxV)),
                    Trace = new(true, 1E-6f)
                };

                e.V = -e.R.Normal * Rand.Double(minV, maxV);

                s.Particles.Add(e);
            }

            var scene = new Scene(s);
            scene.Start();
        }

        [Fact]
        public void NElectronTangentialSpeedT1()
        {
            BohrRatio = BohrRadiusN(20, 1);
            Zoom = 2 / BohrRatio;

            var dr = BohrRadius * 0.05;
            var s = new Simulation(dr);
            s.Particles.Add(new Proton() { Trace = new(true, 1E-6f) });
            //s.Particles.Add(new Hydrogen() { Trace = new(true, 1E-6f) });
            //s.Particles.Add(new Helium() { Trace = new(true, 1E-6f) });
            //s.Particles.Add(new Carbon() { Trace = new(true, 1E-6f) });
            //s.Particles.Add(new Iron() { Trace = new(true, 1E-6f) });

            var count = 3;
            var ang = Pi2 / count;
            var r = new Vector(BohrRatio * 0.01, 0, 0);
            var v = new Vector(0, 200000, 0);
            var n = new Vector(0, 0, 1);

            for (var a = 0; a < count; a++)
            {
                var q = Quaternion.CreateFromAxisAngle(n, ang * a);

                var e = new Electron
                {
                    R = Vector.Transform(r, q),
                    V = Vector.Transform(v, q),
                    Trace = new(true, 1E-6f),
                };

                //e.V = Vector.Normalize(e.R) * v.Length();

                s.Particles.Add(e);
            }

            var scene = new Scene(s);
            scene.Start();
        }

        [Fact]
        public void HydrogenZ1N0E1()
        {
            BohrRatio = BohrRadiusN(4, 1);
            Zoom = 2 / BohrRatio;

            var dr = BohrRadius * 0.01;
            var s = new Simulation(dr);
            s.Particles.Add(new Proton() { Trace = new(true, 1E-6f) });
            //s.Particles.Add(new Hydrogen() { Trace = new(true, 1E-6f) });
            //s.Particles.Add(new Helium() { Trace = new(true, 1E-6f) });
            //s.Particles.Add(new Carbon() { Trace = new(true, 1E-6f) });
            //s.Particles.Add(new Iron() { Trace = new(true, 1E-6f) });

            var count = 1;
            var ang = Pi2 / count;

            var n = 1;
            var z = Hydrogen.Z;
            var radius = BohrRadiusN(n, z);
            var speed = ElectronSpeed(z, radius);

            var r = new Vector(radius, 0, 0);
            var v = new Vector(0, speed, 0);
            var normal = new Vector(0, 0, 1);

            for (var a = 0; a < count; a++)
            {
                var q = Quaternion.CreateFromAxisAngle(normal, ang * a);

                var e = new Electron
                {
                    R = Vector.Transform(r, q),
                    V = Vector.Transform(v, q),
                    Trace = new(true, 1E-6f),
                };

                //e.V = Vector.Normalize(e.R) * v.Length();

                s.Particles.Add(e);
            }

            var scene = new Scene(s);
            scene.Start();
        }
    }
}
#endif