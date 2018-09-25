using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            var c = new Classes();
            c.Myevent += (s) =>
            {
                Console.WriteLine(s);
                Thread.Sleep(5000);
            };
            c.DoWork();

            Console.ReadKey();
        }

        public class Classes
        {
            public Action<int> Myevent;

            public void DoWork()
            {
                for (int i = 0; i < 500; i++)
                {
                    var c = i;
                    Task.Factory.StartNew((p) =>
                    {
                        int d = (int)p;
                        if (d % 2 == 0)
                        {
                            var a = Myevent;
                            a.Invoke(d);
                        }
                        else
                        {
                            Myevent.Invoke(d);
                        }
                    }, c);
                }
            }
        }
    }
}
