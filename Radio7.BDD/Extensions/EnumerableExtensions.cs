using System.Collections.Generic;
using System.Linq;

namespace Radio7.BDD.Extensions
{
    public static class EnumerableExtensions
    {
        public static T Second<T>(this IEnumerable<T> source)
        {
            return source.Skip(1).First();
        }

        public static T SecondOrDefault<T>(this IEnumerable<T> source)
        {
            return source.Skip(1).FirstOrDefault();
        }

        public static T Third<T>(this IEnumerable<T> source)
        {
            return source.Skip(2).First();
        }

        public static T ThirdOrDefault<T>(this IEnumerable<T> source)
        {
            return source.Skip(2).FirstOrDefault();
        }
    }
}
