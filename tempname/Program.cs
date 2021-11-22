using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _ArrayList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            _ArrayList arrayList = new _ArrayList();
            arrayList.Add(1);
            arrayList.Add(2);
            arrayList.Add("3");
            arrayList.Add(true);
            foreach(var item in arrayList)
            {
                Console.Write($"{item} ");
            }
        }
    }
}
