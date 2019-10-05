using System;
using System.Threading;

namespace Neptune
{
    public class Run
    {
        public static void For(TimeSpan span)
        {
            var ms = Convert.ToInt32(span.TotalMilliseconds);
            Thread.Sleep(ms);
        }
    }
}