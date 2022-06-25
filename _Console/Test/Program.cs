using System;
using System.Numerics;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Vector3 v1 = new Vector3(3, 0, 2);
            Vector3 v2 = new Vector3(5, 3, 1);

            Console.WriteLine(v1 + v2);
            Console.WriteLine(v1 - v2);

        }
    }
}
