#if NO_COLLISION_DT
using AtomicDynamics.OpenGl;
using AtomicDynamics;
using Xunit.Abstractions;
using static AtomicDynamics.Physics;
using static AtomicDynamics.Big;

namespace Tests
{
    public class SimulationTest(ITestOutputHelper output) : BaseTest(output)
    {
        [Fact]
        public void HydrogenZ1N0E1()
        {
            BohrRatio = BohrRadiusN(4, 1);
            Zoom = 2 / BohrRatio;

            var dt = Pow(10, -18);
            var s = new Simulation(dt);
            //s.Particles.Add(new Proton() { Trace = new(true, 1E-6f) });
            s.Particles.Add(new Hydrogen() { Trace = new(true, 1E-6f) });
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