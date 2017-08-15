using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Visyn.Io
{
    public static class DelimitedExtensions
    {
        #region Implementation of IDelimitedString

        public static string ToDelimitedString(this IDelimitedData data, string delimiter) => String.Join(delimiter, data.ToStringArray());
        public static string ToDelimitedString(this IDelimitedData data, char delimiter) => String.Join(delimiter.ToString(), data.ToStringArray());
        public static string DelimitedHeader(this IDelimitedData data, string delimiter) => String.Join(delimiter, data.ToHeaderArray());
        public static string DelimitedHeader(this IDelimitedData data, char delimiter) => String.Join(delimiter.ToString(), data.ToHeaderArray());
        #endregion


        #region Implementation of IDelimitedString

        public static string DelimitedHeader<T>(this IList<T> list, string delimiter) where T : IDelimitedString
        {
            return list.Count > 0 ? $"Index{delimiter}{list[0].DelimitedHeader(delimiter)}" : $"Index{delimiter}Null";
        }

        [Obsolete("Not currently used...")]
        public static string ToDelimitedString<T>(this IList<T> list, string delimiter) where T : IDelimitedString
        {
            var builder = new StringBuilder();
            for (var i = 0; i < list.Count; i++)
            {
                builder.AppendLine($"{i}{delimiter}{list[i].ToDelimitedString(delimiter)}");
            }
            return builder.ToString();
        }

        [Obsolete("Not currently used...")]
        public static string ToDelimitedString<T>(this IList<T> list1, IList<T> list2 , string delimiter) where T : IDelimitedString
        {
            var builder = new StringBuilder();
            for (var i = 0; i < list1.Count; i++)
            {
                builder.AppendLine($"{i}{delimiter}{list1[i].ToDelimitedString(delimiter)}{delimiter}{list2[i].ToDelimitedString(delimiter)}");
            }
            return builder.ToString();
        }

        #endregion


        public static string ToDelimitedString<T>(this IEnumerable<T> enumerable, bool includeHeader, string elementDelimiter, string lineDelimiter) where T : IDelimitedData
        {
            if (enumerable == null) return "";
            var strings = new StringBuilder();
            foreach (var item in enumerable)
            {
                if (includeHeader)
                {
                    strings.Append(item.DelimitedHeader(elementDelimiter) + lineDelimiter);
                    includeHeader = false;
                }
                strings.Append(item.ToDelimitedString(elementDelimiter) + lineDelimiter);
            }
            return strings.ToString();
        }

        public static string ToDelimitedString<T,U>(this IEnumerable<T> enumerable1, IEnumerable<U> enumerable2, bool includeHeader, string elementDelimiter, string lineDelimiter) 
            where T : IDelimitedData
            where U : IDelimitedData
        {
            if (enumerable1 == null) return "";
            var strings = new StringBuilder();
            using (var enum2 = enumerable2.GetEnumerator())
            {
                foreach (var item in enumerable1)
                {
                    enum2.MoveNext();
                    if (includeHeader)
                    {
                        strings.Append(item.DelimitedHeader(elementDelimiter) + elementDelimiter);
                        strings.Append(enum2.Current + lineDelimiter);
                        includeHeader = false;
                    }
                    strings.Append(item.ToDelimitedString(elementDelimiter) + elementDelimiter);
                    strings.Append(enum2.Current.ToDelimitedString(elementDelimiter) + lineDelimiter);
                }
            }
            return strings.ToString();
        }


        public static IEnumerable<TOut> Concat<TOut, TIn1, TIn2>(this IEnumerable<TIn1> e1, IEnumerable<TIn2> e2)
            where TIn1 : TOut
            where TIn2 : TOut
        {
            return e1.Select((i) => (TOut)i).Concat(e2.Select((i) => (TOut)i));
        }

        public static IEnumerable<TOut> Interleave<TOut, TIn1, TIn2>(this IEnumerable<TIn1> e1, IEnumerable<TIn2> e2, int count)
            where TIn1 : TOut
            where TIn2 : TOut
        {
            using (var enum1 = e1.GetEnumerator())
            {
                using (var enum2 = e2.GetEnumerator())
                {
                    enum1.Reset();
                    enum2.Reset();

                    for (var i = 0; i < count; i++)
                    {
                        var ok1 = enum1.MoveNext();
                        var ok2 = enum2.MoveNext();

                        if (!ok1 || !ok2)
                        {
                            if (ok1) throw new Exception($"Collection {nameof(e2)} does not have {count} elements");
                            if (ok2) throw new Exception($"Collection {nameof(e1)} does not have {count} elements");
                            throw new Exception(
                                $"Collections {nameof(e1)} and {nameof(e2)} do not have {count} elements");
                        }

                        TIn1 current1 = enum1.Current;
                        TIn2 current2 = enum2.Current;

                        yield return enum1.Current;
                        yield return enum2.Current;
                    }
                }
            }
        }
    }
}