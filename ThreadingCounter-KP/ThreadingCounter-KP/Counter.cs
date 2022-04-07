using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Lib
{
    public class Counter
    {
        public readonly object thread_control = new object();
        uint count;
        public Counter()
        {
            count = 0;
        }
        public void setCounter(uint newval)
        {
            lock (thread_control)
            {
                count = newval;
            }
        }

        public uint GetCounter()
        {
            Console.Write("1");
            return count;
        }

        public void Loop()
        {
            while (true)
            {
                lock (thread_control)
                {
                    ++count;
                    Thread.Sleep(50);
                    Console.Write("\r" + count);
                }
            }
        }

    }
}
