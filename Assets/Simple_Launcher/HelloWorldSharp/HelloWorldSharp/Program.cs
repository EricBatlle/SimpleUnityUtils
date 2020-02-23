using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string txt = "Initiated with args:" + '\n';
            if (args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    txt += "Argument " + i + ": " + args[i]+ '\n';
                }
                Console.Out.Write(txt);
            }
            Console.WriteLine('\n'+"Press something to close");
            Console.ReadKey();
        }
    }
}
