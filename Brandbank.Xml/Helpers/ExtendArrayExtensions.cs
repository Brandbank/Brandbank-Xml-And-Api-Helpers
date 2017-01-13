using System;
using System.Collections.Generic;
using System.Linq;

namespace Brandbank.Xml.Helpers
{
    public static class ExtendArrayExtensions
    {
        public static T[] ExtendArray<T, Tkey>(
            this T[] originalArray, 
            T addItem, 
            Func<T, Tkey> orderer) 
            where T : class, new() =>
            originalArray
                .ExtendArrayInner(new[] { addItem })
                .OrderBy(orderer)
                .ToArray();
        
        public static T[] ExtendArray<T>(
            this T[] originalArray, 
            T addItem) 
            where T : class, new() => 
            originalArray
                .ExtendArrayInner(new[] { addItem })
                .ToArray();

        public static T[] ExtendArray<T>(
            this T[] originalArray, 
            T[] addItems) 
            where T : class, new() =>
            originalArray
                .ExtendArrayInner(addItems)
                .ToArray();

        private static IEnumerable<T> ExtendArrayInner<T>(
            this T[] originalArray, T[] addItems) 
            where T : class, new() =>
             originalArray == null ?
                addItems :
                originalArray.Concat(addItems);
        
    }
}
