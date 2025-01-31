using System.Collections.Concurrent;
using System.Diagnostics;

namespace AtomicDynamics
{
    public static partial class Log
    {
        public static readonly object DebugLock = new();


        private static int counter = 1;
        public static void W(object? s)
        {
            Trace.WriteLine($"{s}   {Interlocked.Increment(ref counter)}");
        }

        private static readonly ConcurrentDictionary<string, Big> maxBigs = [];
        public static void Max(Big value, string key = "max")
        {
            if (maxBigs.TryGetValue(key, out Big v))
            {
                if (value > v)
                {
                    W(value.ToString("E"));
                    maxBigs.TryUpdate(key, value, v);
                }
            }
            else
            {
                W(value.ToString("E"));
                maxBigs.TryAdd(key, value);
            }
        }
    }
}
