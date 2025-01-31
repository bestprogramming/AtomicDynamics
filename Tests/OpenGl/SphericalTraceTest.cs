using AtomicDynamics;
using AtomicDynamics.OpenGl;
using System.Numerics;
using Xunit.Abstractions;
using static AtomicDynamics.Physics;
using static AtomicDynamics.Big;

namespace Tests
{
    public class SphericalTraceTest(ITestOutputHelper output) : BaseTest(output)
    {
        [Fact]
        public void T1()
        {
            var phi = 0.0f;
            var teta = 0.0f;
            var r = 0.01f;

            void action(TraceScene s)
            {
                phi += 0.1f;
                teta += 0.01f;

                var (sinPhi, cosPhi) = MathF.SinCos(phi);
                var (sinTeta, cosTeta) = MathF.SinCos(teta);
                var x = r * sinTeta * cosPhi;
                var y = r * sinTeta * sinPhi;
                var z = r * cosTeta;
                s.Trace.Add(new(x, y, z));
            }

            var s = new TraceScene(action);
            s.Trace.Enabled = true;
            s.Trace.MinDistance = 0.0001f;
            s.Start();
        }
    }
}