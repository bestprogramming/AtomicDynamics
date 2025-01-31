using static AtomicDynamics.Physics;
using static AtomicDynamics.Big;

namespace AtomicDynamics
{
    internal static class Program
    {
        [STAThread]
        static void Main()
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

        [STAThread]
        static void Main1()
        {
            BohrRatio = BohrRadiusN(40, 1);
            Zoom = 2 / BohrRatio;

            var dr = BohrRadius * Pow(10, -1);
            var s = new Simulation(dr);

            var x = BohrRadius * 10;
            var y = BohrRadius * 30;
            var v = 5.8 * Pow(10, 6);

            //s.Particles.Add(new Helium());
            s.Particles.Add(new Electron() { R = new(x, y, 0), V = new(0, -v, 0), Trace = new(true, 1E-6f) });

            var rigid = new LineStrip([
                new Electron() { R = new(-x, 0, 0), Trace = new(true, 1E-6f) },
                new Electron() { R = new(x, 0, 0), Trace = new(true, 1E-6f) }]);

            s.Particles.Add(rigid);

            var scene = new Scene(s);
            scene.Start();
        }
    }
}