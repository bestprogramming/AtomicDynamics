using System.Numerics;

namespace AtomicDynamics
{
    public static partial class ExtensionMethods
    {
        public static bool IsReal(this Big b) => !b.IsNaN;

        public static BigInteger Sqrt(this BigInteger input)
        {
            if (input < 144838757784765629) // 1.448e17 = ~1<<57
            {
                uint resultUInt = (uint)Math.Sqrt((ulong)input);
                if ((input <= 4503599761588224) && ((ulong)resultUInt * resultUInt > (ulong)input)) // 4.5e15 = ~1<<52
                {
                    resultUInt--;
                }
                return resultUInt;
            }

            double inputDouble = (double)input;
            if (inputDouble < 8.5e37) // 8.5e37 is V<sup>2</sup>long.max * long.max
            {
                ulong resultULong = (ulong)Math.Sqrt(inputDouble);
                BigInteger result = (resultULong + ((ulong)(input / resultULong))) >> 1;
                return (result * result >= input) ? result : result - 1;
            }

            if (inputDouble < 4.3322e127)
            {
                BigInteger resultBigInt = (BigInteger)Math.Sqrt(inputDouble);
                resultBigInt = (resultBigInt + (input / resultBigInt)) >> 1;
                if (inputDouble > 2e63)
                {
                    resultBigInt = (resultBigInt + (input / resultBigInt)) >> 1;
                }
                return (resultBigInt * resultBigInt >= input) ? resultBigInt : resultBigInt - 1;
            }

            int bitLength = (int)BigInteger.Log(input, 2.0);
            BigInteger testBitLen = BigInteger.Pow(2, bitLength);
            BigInteger testBitLenPlus1 = BigInteger.Pow(2, bitLength + 1);

            BigInteger diff1 = input - testBitLen;
            BigInteger diff2 = input - testBitLenPlus1;

            if (diff1 > diff2)
            {
                bitLength++;
            }

            int wantedPrecision = (bitLength + 1) / 2;
            int bitLengthMod = bitLength + (bitLength & 1) + 1;

            // Do the first sqrt on hardware
            long tempLong = (long)(input >> (bitLengthMod - 63));
            double tempSqrtDouble = Math.Sqrt(tempLong);
            ulong valLong = (ulong)BitConverter.DoubleToInt64Bits(tempSqrtDouble) & 0x1fffffffffffffL;
            if (valLong == 0)
            {
                valLong = 1UL << 53;
            }

            // Classic Newton iterations
            BigInteger valBigInt = ((BigInteger)valLong << (53 - 1)) + (input >> bitLengthMod - (3 * 53)) / valLong;
            int size = 106;
            for (; size < 256; size <<= 1)
            {
                valBigInt = (valBigInt << (size - 1)) + (input >> bitLengthMod - (3 * size)) / valBigInt;
            }

            if (inputDouble > 4e254)// 1 << 845
            {
                int numOfNewtonSteps = (int)Math.Log((uint)(wantedPrecision / size), 2.0) + 2;

                // Apply starting size 
                int wantedSize = (wantedPrecision >> numOfNewtonSteps) + 2;
                int needToShiftBy = size - wantedSize;
                valBigInt >>= needToShiftBy;
                size = wantedSize;
                do
                {
                    //Newton plus iterations 
                    int shiftBits = bitLengthMod - (3 * size);
                    BigInteger valSquared = (valBigInt * valBigInt) << (size - 1);
                    BigInteger valShifted = (input >> shiftBits) - valSquared;
                    valBigInt = (valBigInt << size) + (valShifted / valBigInt);
                    size *= 2;
                } while (size < wantedPrecision);
            }

            // There are a few extra digits here, lets save them
            int oversizedBy = size - wantedPrecision;
            BigInteger saveDroppedDigitsBigInt = valBigInt & ((BigInteger.One << oversizedBy) - 1);
            int undersizedBy = (oversizedBy < 64) ? (oversizedBy >> 2) + 1 : (oversizedBy - 32);
            ulong saveDroppedDigitsULong = (ulong)(saveDroppedDigitsBigInt >> undersizedBy);

            // Shrink result to wanted precision 
            valBigInt >>= oversizedBy;

            // Detect if should round up
            if ((saveDroppedDigitsULong == 0) && (valBigInt * valBigInt > input))
            {
                valBigInt--;
            }
            return valBigInt;
        }

        public static BigInteger NthRoot(this BigInteger source, int root)
        {
            if (root < 1) throw new Exception("Root must be greater than or equal to 1");
            if (source.Sign == -1) throw new Exception("Value must be a positive integer");

            if (source == BigInteger.One) { return BigInteger.One; }
            if (source == BigInteger.Zero) { return BigInteger.Zero; }
            if (root == 1) { return source; }

            BigInteger upperbound = source;
            BigInteger lowerbound = BigInteger.Zero;

            while (true)
            {
                BigInteger nval = (upperbound + lowerbound) >> 1;
                BigInteger testPow = BigInteger.Pow(nval, root);

                if (testPow > source) upperbound = nval;
                if (testPow < source) lowerbound = nval;
                if (testPow == source)
                {
                    lowerbound = nval;
                    break;
                }
                if (lowerbound == upperbound - 1) break;
            }

            return lowerbound;
        }
    }
}
