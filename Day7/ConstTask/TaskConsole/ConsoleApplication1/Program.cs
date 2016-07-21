using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Hashtable hash = new Hashtable();
            hash.Add("3","a");
            hash.Add("2",1);
            hash.Add("1",1.45);
            foreach(var val in hash.Values)
            {
                Console.WriteLine(val.GetType());
            }
        }
    }
}
