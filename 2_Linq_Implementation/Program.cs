using System;
using System.Collections;
using System.Collections.Generic;
using _2_Linq_Implementation;
//using System.Linq;

namespace _2_Linq_Implementation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] array = { 10, 5, 3, 4, 9 };

            //var text1 = array.Select((x, i) => x.ToString());
            //var text2 = array.Select(x => x.ToString());


            //int[] x = { 1, 2, 3 };
            //int[] y = { 4, 5, 6 };

            //var numbers = from a in x
            //              from b in y
            //              select new { a, b };

            //foreach (var n in numbers)
            //{
            //    Console.WriteLine(n);
            //}


            int[] x = { 1, 2, 3, 4, 5, 4, 1, 7 };
            int[] y = { 4, 5, 4, 6, 7, 8 };

            var v = x.Count();
            //var z = x.Reverse();

            var union = x.Union(y);
            var except = x.Except(y);
            //var intersect = x.Intersect(y);
            //var concat = x.Concat(y);


            //int num1 = x.First();
            //int num2 = x.FirstOrDefault();
            //int num3 = x.Last();
            //int num4 = x.LastOrDefault();
            //int num5 = x.Single(x => x > 6);
            //int num6 = x.SingleOrDefault(x => x > 6);

            //var skip1 = x.Skip(-2);
            //var skip2 = x.SkipLast(2);
            //var skip3 = x.SkipWhile(x => x < 3);
            //var skip4 = x.SkipWhile((x, i) => x > i);
            //
            //
            //var take1 = x.Take(2);
            //var take2 = x.TakeLast(-2);
            //var take3 = x.TakeWhile(x => x < 3);
            //var take4 = x.TakeWhile((x, i) => x > i);


            //var distinct = x.Distinct();

            // string[] pages = new string[1000];
            // int currentPage = 0, totalPages = pages.Length, pageSize = 100;
            //
            //
            //
            // while (currentPage <= totalPages / pageSize)
            // {
            //      var data = pages.Skip(currentPage * pageSize).Take(pageSize);
            //      currentPage++;
            //      Console.ReadKey();
            // }

        }
    }
}
