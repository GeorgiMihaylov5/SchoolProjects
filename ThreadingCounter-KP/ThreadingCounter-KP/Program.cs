using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Lib;

namespace Appl
{
    class Program
    {
        static public Counter ob;
        static Thread t1;
        static void ViewMenu(bool clear)
        {
            // if(t1.ThreadState == ThreadState.Running)
            //t1.Suspend();
            {
                if (clear) Console.Clear();
                Console.Write("\nSelect operation:\nN: Start\nA: Abort\nR: Resume\nP: Suspend\nI: ReInit\nESC: Exit\n");
            }
            //if (t1.ThreadState == ThreadState.Suspended) 
            //t1.Resume();
        }

        static void Main(string[] args)
        {
            ob = new Counter();
            // ob.Loop();

            ThreadStart ManageThread = new ThreadStart(ob.Loop);

            ViewMenu(false);
            ConsoleKeyInfo key = new ConsoleKeyInfo();

            do
            {
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey(true);

                    switch (key.Key)
                    {
                        case ConsoleKey.N:
                            ViewMenu(true);
                            if (t1 == null)
                            {
                                t1 = new Thread(ManageThread);
                                t1.Start();
                            }
                            break;
                        case ConsoleKey.A:
                            ViewMenu(true);
                            if (t1 != null)
                            {
                                t1.Abort();
                                t1 = null;
                            }
                            break;
                        case ConsoleKey.R:
                            ViewMenu(true);
                            ThreadState tmp = t1.ThreadState;
                            if (t1 != null && t1.ThreadState == ThreadState.Suspended)
                                t1.Resume();
                            break;
                        case ConsoleKey.P:
                            ViewMenu(true);

                            if (t1 != null && t1.ThreadState != ThreadState.Suspended)
                                t1.Suspend();
                            break;

                        case ConsoleKey.I:
                            lock (ob.thread_control)
                            {
                                ViewMenu(true);
                                Console.Write("Set new value: ");
                                uint newvalue = uint.Parse(Console.ReadLine());
                                ob.setCounter(newvalue);
                            }
                            break;
                        default:
                            break;
                    }

                }
            } while (key.Key != ConsoleKey.Escape);
        }
    }
}
