namespace AtomicDynamics
{
    public static partial class Conversion
    {
        public static double Rad(double deg) => deg * Const.PiOver180;

        public static double Deg(double rad) => rad * Const.Pi180OverPi;


        public static float Rad(float deg) => deg * Const.PiOver180F;

        public static float Deg(float rad) => rad * Const.Pi180OverPiF;
    }
}
