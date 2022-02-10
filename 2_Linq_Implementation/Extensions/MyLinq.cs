using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;


namespace _2_Linq_Implementation.Extensions
{
    internal static class MyLinq
    {
        public static IEnumerable<T> Reverse<T>(this IEnumerable<T> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            var list = source as IList<T>;
            for (int i = list.Count - 1; i >= 0; i--)
                yield return list[i];
        }

        public static IEnumerable<TResult> Select<TSource, TResult>(
            this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            foreach (var item in source)
                yield return selector(item);
        }
        
        public static IEnumerable<TResult> Select<TSource, TResult>(
            this IEnumerable<TSource> source, Func<TSource, int, TResult> selector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            int index = -1;
            foreach (var item in source)
            {
                index++;
                yield return selector(item, index);
            }
        }

        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            
            return new HashSet<T>(source);
        }

        public static IEnumerable<T> Union<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));

            HashSet<T> set = new(first);

            IEnumerator<T> e = second.GetEnumerator();
            while (e.MoveNext()) 
                set.Add(e.Current);
            
            return set;
            
            //Or
            //return new HashSet<T>(System.Linq.Enumerable.Concat(first, second));
        }

        public static IEnumerable<T> Intersect<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));
            
            HashSet<T> set = new(first);

            foreach (var itemSecond in second)
                if (set.Contains(itemSecond)) { yield return itemSecond; set.Remove(itemSecond); }
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));

            HashSet<T> set = new(first);

            foreach (var itemSecond in second)
                if (set.Contains(itemSecond)) set.Remove(itemSecond);

            return set;
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));

            foreach (var item in first) 
                yield return item;
            
            foreach (var item in second) 
                yield return item;
        }

        public static T Single<T>(this IEnumerable<T> source)
        {
            if (source.Count() > 1) throw new InvalidOperationException(nameof(source));
            if (source == null) throw new ArgumentNullException(nameof(source));
            
            return source.SingleOrDefault();
        }

        public static T SingleOrDefault<T>(this IEnumerable<T> source)
        {
            if (source.Count() == 0) return default;
            if (source == null) throw new ArgumentNullException(nameof(source));

            if (source.Count() == 1)
            {
                IEnumerator<T> e = source.GetEnumerator();
                if (e.MoveNext()) return e.Current;
            }

            return default;
        }

        public static T Single<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            // if (source == null) throw new ArgumentNullException(nameof(source));
            // if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            // return source.SingleOrDefault(predicate);

            return source.Count() == 0
                ? throw new ArgumentNullException(nameof(source))
                : source.SingleOrDefault(predicate);
        }

        public static T SingleOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source.Count() == 0) return default;
            if (source == null) throw new ArgumentNullException(nameof(source));

            IEnumerator<T> e = source.GetEnumerator();
            while (e.MoveNext())
            {
                T result = e.Current;
                if (predicate(result))
                {
                    while (e.MoveNext())
                        if (predicate(e.Current))
                            throw new InvalidOperationException(nameof(source));

                    return result;
                }
            }

            return default;
        }

        public static T First<T>(this IEnumerable<T> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return source.FirstOrDefault();
            
            //Or
            //return System.Linq.Enumerable.First(source);
        }
        
        public static T FirstOrDefault<T>(this IEnumerable<T> source)
        {
            if (source.Count() == 0) return default;
            if (source == null) throw new ArgumentNullException(nameof(source));

            IEnumerator<T> e = source.GetEnumerator();
            if (e.MoveNext()) return e.Current;

            return default;
            
            //Or
            //return System.Linq.Enumerable.FirstOrDefault(source);
        }
        
        public static T FirstOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate) {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            foreach (var item in source) 
                if (predicate(item)) return item;

            return default;
        }


        public static T Last<T>(this IEnumerable<T> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return source.LastOrDefault();
        }

        public static T LastOrDefault<T>(this IEnumerable<T> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            IEnumerator<T> e = source.GetEnumerator();
            if (!e.MoveNext()) return default;
            
            T result;
            do
                result = e.Current;
            while (e.MoveNext());
            return result;
        }
        
        public static T LastOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate) {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            object returnValue = null;
            bool foundLast = false;

            foreach (var item in source) {
                if (predicate(item)) {
                    foundLast = true;
                    returnValue = item;
                }
            }

            if (foundLast) return (T)returnValue;
            return default;
        }

        public static IEnumerable<T> TakeWhile<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            foreach (var item in source)
            {
                if (!predicate(item)) break;
                yield return item;
            }

            // IEnumerator<T> e = source.GetEnumerator();
            // while (e.MoveNext())
            // {
            //     if (predicate(e.Current))
            //         yield return e.Current;
            //     else
            //         break;
            // }
        }

        public static IEnumerable<T> TakeWhile<T>(this IEnumerable<T> source, Func<T, int, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            int index = -1;
            foreach (var item in source)
            {
                index++;
                if (!predicate(item, index)) break;
                yield return item;
            }
        }

        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int count)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            
            return count <= 0 ?
                System.Linq.Enumerable.Empty<T>() :
                TakeLastIterator(source, count);
        }

        private static IEnumerable<TSource> TakeLastIterator<TSource>(IEnumerable<TSource> source, int count)
        {
            Queue<TSource> queue;
            using (IEnumerator<TSource> e = source.GetEnumerator())
            {
                if (!e.MoveNext()) yield break; 

                queue = new Queue<TSource>();
                queue.Enqueue(e.Current);

                while (e.MoveNext())
                {
                    if (queue.Count < count)
                    {
                        queue.Enqueue(e.Current);
                    }
                    else
                    {
                        do
                        {
                            queue.Dequeue();
                            queue.Enqueue(e.Current);
                        } while (e.MoveNext());

                        break;
                    }
                }
            }
            
            do
            {
                yield return queue.Dequeue();
            }
            while (queue.Count > 0);
        }

        public static IEnumerable<T> Take<T>(this IEnumerable<T> source, int count)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (count < 1) yield break;

            IEnumerator<T> e = source.GetEnumerator();
            for (int i = 0; e.MoveNext(); i++)
                if (i < count)
                    yield return e.Current;
        }

        public static IEnumerable<T> SkipWhile<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            IEnumerator<T> e = source.GetEnumerator();
            while (e.MoveNext())
            {
                if (!predicate(e.Current))
                {
                    yield return e.Current;
                    while (e.MoveNext())
                        yield return e.Current;
                }
            }
        }

        public static IEnumerable<T> SkipWhile<T>(this IEnumerable<T> source, Func<T, int, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            int index = -1;
            IEnumerator<T> e = source.GetEnumerator();
            while (e.MoveNext())
            {
                index++;
                if (!predicate(e.Current, index))
                {
                    yield return e.Current;
                    while (e.MoveNext())
                        yield return e.Current;
                }
            }
        }

        public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> source, int count)
        {
            if (source == null ) throw new ArgumentNullException(nameof(source));

            return count <= 0 ?
                source.Skip(0) :
                SkipLastIterator(source, count);
        }
        
        private static IEnumerable<TSource> SkipLastIterator<TSource>(IEnumerable<TSource> source, int count)
        {
            var queue = new Queue<TSource>();

            using (IEnumerator<TSource> e = source.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    if (queue.Count == count)
                    {
                        do
                        {
                            yield return queue.Dequeue();
                            queue.Enqueue(e.Current);
                        }
                        while (e.MoveNext());
                        break;
                    }
                    else
                    {
                        queue.Enqueue(e.Current);
                    }
                }
            }
        }
        
        public static IEnumerable<T> Skip<T>(this IEnumerable<T> source, int count)
        {
            if (source == null ) throw new ArgumentNullException(nameof(source));

            var e = source.GetEnumerator();
            for (int i = 0; e.MoveNext(); i++)
            {
                if (i < count) continue;
                yield return e.Current;
            }
        }


        public static IEnumerable<T> Where<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            foreach (var item in source)
                if (predicate(item))
                    yield return item;
        }

        public static bool Any<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            foreach (var item in source)
                if (predicate(item))
                    return true;
            return false;
        }

        public static bool All<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            foreach (var item in source)
                if (!predicate(item))
                    return false;
            return true;
        }

        public static int Count<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            
            int count = 0;
            foreach (var item in source)
                if (predicate(item)) count++;

            return count;
        }

        public static int Count<T>(this IEnumerable<T> source) => Count(source, x => true);
    }
}