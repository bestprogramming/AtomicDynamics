using static AtomicDynamics.Big;

namespace AtomicDynamics
{
    public static partial class Rand
    {
        public static double Double(double min, double max) => random.NextDouble() * (max - min) + min;
        public static double NextDouble() => random.NextDouble();
        public static int Int(int min, int max) => random.Next(min, max);
        public static bool Bool() => random.Next(0, 2) == 1;
        public static int Sign() => random.Next(0, 2) == 1 ? 1 : -1;
        public static Vector Torus(in Big bigR, in Big smallR)
        {
            var teta = Double(0, Const.Pi2);
            var phi = Double(0, Const.Pi2);
            SinCos(teta, out Big sinTeta, out Big cosTeta);
            SinCos(phi, out Big sinPhi, out Big cosPhi);
            return new(
                (bigR + smallR * cosTeta) * cosPhi,
                (bigR + smallR * cosTeta) * sinPhi,
                smallR * sinTeta);
        }
    }
}
