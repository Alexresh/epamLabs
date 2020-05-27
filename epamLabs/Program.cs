using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace epamLabs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            int[] range = new int[] { 1, 2, 3 };
            DynArray<int> a = new DynArray<int>();

            Console.WriteLine(a.ToString());
            a.Add(8);
            Console.WriteLine(a.ToString());
            a.AddRange(range);
            Console.WriteLine(a.ToString());
            a.Insert(120, 7);
            Console.WriteLine(a.ToString());
            a.Insert(120, 0);
            Console.WriteLine(a.ToString());
            a.Insert(120, 4);
            Console.WriteLine(a.ToString());
            a.Add(2);
            Console.WriteLine(a.ToString());
            a.Remove(120);
            Console.WriteLine(a.ToString());
            if (!a.Remove(90)) Console.WriteLine("Элемента нет");
            a.Filter((o) => o > 2);
            Console.WriteLine(a.ToString());
            a.Sort(a.compareTwoInt);
            Console.WriteLine(a.ToString());

            Console.ReadKey();
            
            
        }
    }
}
