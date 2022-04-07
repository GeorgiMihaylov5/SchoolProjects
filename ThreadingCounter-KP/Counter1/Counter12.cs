using System;

namespace Counter1
{
    public class Counter12
    {
        private readonly object counterLock = new object();
        private int counter;

        public Counter12()
        {
            this.counter = 0;
        }
        public void Loop()
        {
            while (true)
            {
                lock (counterLock)
                {
                    Console.Write("\r" + counter++ / 10000);
                }
            }
        }
        public void setCounter(int newval)
        {
            lock (counterLock)
            {
                counter = newval;
            }
        }
    }
}
