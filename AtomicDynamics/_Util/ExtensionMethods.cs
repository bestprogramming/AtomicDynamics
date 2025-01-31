using static AtomicDynamics.Physics;

namespace AtomicDynamics
{
    public static partial class ExtensionMethods
    {
        public static string? RoundBrackets(this string? s)
        {
            if (s == null) return null;
            return $"({s})";
        }

        public static string? BoxBrackets(this string? s)
        {
            if (s == null) return null;
            return $"[{s}]";
        }

        public static string? CurlyBrackets(this string? s)
        {
            if (s == null) return null;
            return $"{{{s}}}";
        }

        public static Elementary[] PackingToFm(this Elementary[] elementaries, double fm)
        {
            //fm *= 1000; //to see
            for (var a = 0; a < elementaries.Length; a++)
            {
                elementaries[a].R *= fm * Fm;
            }
            return elementaries;
        }
    }
}
