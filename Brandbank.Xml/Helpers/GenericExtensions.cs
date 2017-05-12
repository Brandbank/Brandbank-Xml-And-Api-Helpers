using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Brandbank.Xml.Helpers
{
    public static class GenericExtensions
    {
        public static TResult Then<T, TResult>(
            this T message,
            Func<T, TResult> fn) =>
            fn(message);

        public static void Then<T>(
            this T message,
            Action<T> fn) => 
            fn(message);

        public static void Then<T>(
            this T message,
            Action fn) =>
            fn();

        public static string ToJson<T>(this T obj) where T : class
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T FromJson<T>(this string obj) where T : class
        {
            return JsonConvert.DeserializeObject<T>(obj);
        }

        public static T NewIfNull<T>(this T obj) where T : class, new()
        {
            return obj ?? new T();
        }

        public static T[] NewIfNull<T>(this T[] obj) where T : class
        {
            return obj ?? Enumerable.Empty<T>().ToArray();
        }

        public static IEnumerable<T> NewIfNull<T>(this IEnumerable<T> obj) where T : class
        {
            return obj ?? Enumerable.Empty<T>();
        }

        public static IEnumerable<decimal> NewIfNull(this IEnumerable<decimal> obj)
        {
            return obj ?? Enumerable.Empty<decimal>();
        }

        public static T FirstOrNewIfNull<T>(this IEnumerable<T> obj, Func<T, bool> fn) where T : class, new()
        {
            return obj.NewIfNull().FirstOrDefault(fn).NewIfNull();
        }

        public static T FirstOrNewIfNull<T>(this IEnumerable<T> obj) where T : class, new()
        {
            return obj.NewIfNull().FirstOrDefault().NewIfNull();
        }

        public static IEnumerable<TResult> SelectManyOrDefault<TSource, TResult>(
            this IEnumerable<TSource> source, 
            Func<TSource, IEnumerable<TResult>> selector) 
            where TResult : class 
            where TSource : class
        {
            var retVal = Enumerable.Empty<TResult>().ToList();
            foreach (var item in source.NewIfNull())
                retVal.AddRange(selector(item).NewIfNull());
            return retVal;
        }

    }
}
