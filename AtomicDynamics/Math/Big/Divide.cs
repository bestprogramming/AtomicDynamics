using System.Numerics;

namespace AtomicDynamics
{
    public partial struct Big
    {
        public static Big operator /(in Big dividend, in Big divisor) => dividend.nan || divisor.nan || divisor == Zero ? NaN : dividend.bi * pow / divisor.bi;
        public static Big operator /(in Big dividend, in BigInteger divisor) => dividend.nan || divisor == 0 ? NaN : dividend.bi / divisor;
        public static Big operator /(in Big dividend, byte divisor) => dividend.nan || divisor == 0 ? NaN : dividend.bi / divisor;
        public static Big operator /(in Big dividend, sbyte divisor) => dividend.nan || divisor == 0 ? NaN : dividend.bi / divisor;
        public static Big operator /(in Big dividend, short divisor) => dividend.nan || divisor == 0 ? NaN : dividend.bi / divisor;
        public static Big operator /(in Big dividend, ushort divisor) => dividend.nan || divisor == 0 ? NaN : dividend.bi / divisor;
        public static Big operator /(in Big dividend, int divisor) => dividend.nan || divisor == 0 ? NaN : dividend.bi / divisor;
        public static Big operator /(in Big dividend, uint divisor) => dividend.nan || divisor == 0 ? NaN : dividend.bi / divisor;
        public static Big operator /(in Big dividend, long divisor) => dividend.nan || divisor == 0 ? NaN : dividend.bi / divisor;
        public static Big operator /(in Big dividend, ulong divisor) => dividend.nan || divisor == 0 ? NaN : dividend.bi / divisor;
        public static Big operator /(in Big dividend, float divisor) => dividend.nan || divisor == 0 ? NaN : dividend.bi * pow / new Big(divisor).bi;
        public static Big operator /(in Big dividend, double divisor) => dividend.nan || divisor == 0 ? NaN : dividend.bi * pow / new Big(divisor).bi;
        public static Big operator /(in Big dividend, decimal divisor) => dividend.nan || divisor == 0 ? NaN : dividend.bi * pow / new Big(divisor).bi;
        
        public static Big operator /(in BigInteger dividend, in Big divisor) => divisor.nan || divisor.bi == 0 ? NaN : dividend * pow2 / divisor.bi;
    }
}
