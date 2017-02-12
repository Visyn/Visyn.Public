﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Visyn.Public.Types
{
    public static class StringExtensions
    {
        [Obsolete("Use item?ToString()",true)]
        public static string ToStringNotNull(object item) { return item?.ToString() ?? ""; }
        [Obsolete("Use LettersOnly instead", true)]
        public static string CharactersOnly(this string source, char[] skip = null) => LettersOnly(source, skip);

        public static byte[] ToByteArray(this string text)
        {
            if (text == null) return null;
            var objects = new byte[text.Length];
            var index = 0;

            foreach (var item in text)
            {
                objects[index++] = (byte)item;
            }
            return objects;
        }

        public static bool ContainsControlChar(this string text)
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach(var ch in text)
            {
                if (!Char.IsControl(ch)) continue;
                if (ch == '\r') continue;
                if (ch == '\n') continue;
                if (ch == '\t') continue;
                return true;
            }
            return false;
        }
        public static bool ContainsChar(this string text, char ch) => text.Cast<char>().Any(c => c == ch);

        public static bool ContainsChar(this string text, IEnumerable<char> charsToFind)
        {
            return charsToFind.Any(ch => text.Cast<char>().Any(c => c == ch));
        }

        public static string NotNull(this string text)
        {
            return string.IsNullOrEmpty(text) ? "" : text;
        }

        public static string NotNull(this string text, string alternate)
        {
            if (string.IsNullOrEmpty(text))
            {
                return !string.IsNullOrEmpty(alternate) ? alternate : "";
            }
            return text;
        }
        public static IList<string> SplitAndKeepDelimiters(this string s, params char[] delimiters)
        {
            var parts = new List<string>();
            if (string.IsNullOrEmpty(s)) return parts;
            var iFirst = 0;
            do
            {
                int iLast = s.IndexOfAny(delimiters, iFirst);
                if (iLast >= 0)
                {
                    if (iLast > iFirst)
                        parts.Add(s.Substring(iFirst, iLast - iFirst)); //part before the delimiter
                    parts.Add(new string(s[iLast], 1));//the delimiter
                    iFirst = iLast + 1;
                    continue;
                }
                //No delimiters were found, but at least one character remains. Add the rest and stop.
                parts.Add(s.Substring(iFirst, s.Length - iFirst));
                break;
            } while (iFirst < s.Length);

            return parts;
        }

        public static bool TrySplitKeyValuePair(this string source, char[] delimiter, out KeyValuePair<string, string> kvp)
        {
            if (source == null) throw new NullReferenceException("String to split is null!");
            var split = source.Split(delimiter);
            kvp = split.Length == 2 ? new KeyValuePair<string, string>(split[0],split[1]) : new KeyValuePair<string, string>();
            return split.Length == 2;
        }

        public static string LettersOnly(this string source, char[] skip=null)
        {
            if (string.IsNullOrWhiteSpace(source)) return "";
            var result = new StringBuilder();
            foreach(var ch in source)
            {
                if((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z'))
                {
                    result.Append(ch);
                }
                else if(skip != null && skip.Contains(ch)) 
                {
                    result.Append(ch);
                }
            }
            return result.ToString();
        }



        /// <summary>
        /// Splits the string at the specified split character.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="splitChar">The split character.</param>
        /// <param name="occurrence">The nth occurrence in the string. Negative numbers search from end->start.
        /// i.e. -1 indicates last occurrance in the string.  
        /// Split("System.Collections.Generic",-1) will return string[] {"System.Collections","Generic"} </param>
        /// <returns>System.String[].</returns>
        public static string[] Split(this string source, char splitChar, int occurrence)
        {
            if (string.IsNullOrEmpty(source) || occurrence == 0) return new[] {source};
            int len = source.Length;
            if (occurrence > 0)
            {   // Search from head
                for (int y = 0; y < len; y++)
                {
                    if (source[y] != splitChar) continue;
                    if (--occurrence == 0)
                    {
                        var first = source.Remove(y);
                        return (y == len - 1) ? new[] { first } : new [] { first , source.Substring(y+1, source.Length - y-1) };
                    }
                }
                return new[] {source};
            }
            else
            {   // search from tail
                for (int y = len-1; y >=0; y--)
                {
                    if (source[y] != splitChar) continue;
                    if (++occurrence == 0)
                    {
                        var first = source.Remove(y);
                        return (y == len - 1) ? new[] { first } : new[] { first, source.Substring(y + 1, source.Length - y - 1) };
                    }
                }
                return new[] { source };
            }
        }

        public static string ReplaceChars(this string str, char[] chars, char replaceChar)
        {
            if (!str.ContainsChar(chars)) return str;

            var newStr = new char[str.Length];
            for(var i=0;i<str.Length;i++)
            {
                if (chars.Contains(str[i])) newStr[i] = replaceChar;
                else newStr[i] = str[i];
            }
            return new string(newStr);
        }

        public static List<string> SubStrings(this string str, IEnumerable<int> indexes )
        {
            var split = new  List<string>();

            var remaining = str;
            int prevIndex = 0;
            foreach(var index in indexes)
            {
                if(index > prevIndex) split.Add(remaining.Substring(prevIndex, index));
                remaining = remaining.Substring(index);
                prevIndex = index;
            }
            split.Add(remaining);
            return split;
        }

        public static string [] SubStrings(this string str, int index, char[] trimChars=null)
        {
            var before = str.Substring(0,index);
            var after = str.Substring(index, str.Length-index);
            if(trimChars == null) return new string[] {before, after};
            return new string[] { before.Trim(trimChars), after.Trim(trimChars) };

        }

        public static IEnumerable<string> Trim(this string[] strings, char[] trimChars) => strings.Select(str => str.Trim(trimChars));

        public static IEnumerable<string> Trim(this string[] strings, char[] trimChars, StringSplitOptions options)
        {
            return options == StringSplitOptions.None
                ? strings.Trim(trimChars)
                : strings.Select(str => str.Trim(trimChars)).Where(trim => trim.Length > 0);
        }
    }
}
