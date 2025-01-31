#if NO_COLLISION_DR_RIGID_ROTATION
using AtomicDynamics;
using AtomicDynamics.OpenGl;
using System.Numerics;
using Xunit.Abstractions;
using static AtomicDynamics.Physics;
using static AtomicDynamics.Big;

namespace Tests
{
    public class NoCollisionDrRigidRotationTest(ITestOutputHelper output) : BaseTest(output)
    {
        [Fact]
        public void LineStripT1()
        {
            BohrRatio = 0.5;
            Zoom = 2 / BohrRatio;

            var dr = Pow(10, -4);
            var s = new Simulation(dr);

            var x = 0.01;
            var y = 0.1;
            var v = Pow(10, 1);

            //s.Particles.Add(new Helium());
            s.Particles.Add(new Proton() { R = new(x, y, 0), V = new(0, -v, 0), Trace = new(true, 1E-6f) });

            var rigid = new LineStrip([
                new Proton() { R = new(-x, 0, 0), Trace = new(true, 1E-6f) },
                new Electron() { R = new(x, 0, 0), Trace = new(true, 1E-6f) }]);

            s.Particles.Add(rigid);

            var scene = new Scene(s);
            scene.Start();
        }
    }
}
#endif