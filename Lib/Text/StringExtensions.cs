#region Copyright (c) 2015-2017 Visyn
// The MIT License(MIT)
// 
// Copyright(c) 2015-2017 Visyn
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Visyn.Public.Text
{
    public static class StringExtensions
    {
        [Obsolete("Use item?ToString() ?? string.Empty",true)]
        public static string ToStringNotNull(object item) => item?.ToString() ?? "";

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


        public static string FromBytes(byte[] bytes)
        {
            if (bytes == null) return null;
            var chars = new char[bytes.Length];
            for (var i = 0; i < bytes.Length; i++)
            {
                chars[i] = (char)bytes[i];
            }
            return new string(chars);
        }

        [Obsolete("Mispelled, use: IndexesOfAll", true)]
        public static IEnumerable<int> IndexsOfAll(this string text, params char[] find) => IndexesOfAll(text, find);

        /// <summary>
        /// Returns the indexes of all specified characters
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="find">The characters to find.</param>
        /// <returns>IEnumerable&lt;System.Int32&gt; Indexes of specified characters</returns>
        public static IEnumerable<int> IndexesOfAll(this string text, params char[] find)
        {
            if(string.IsNullOrEmpty(text)) yield break;
            for(var i=0;i<text.Length;i++)
            {
                if (find.Any((f) => f == text[i])) yield return i;
                //var ch = text[i];
                //foreach(var f in find)
                //    if (ch == f) yield return i;
            }
        }

        public static string JoinWith(this IEnumerable<string> strings, char ch ) => string.Join(ch.ToString(), strings);

        public static string JoinWith(this IEnumerable strings, char ch) => string.Join(ch.ToString(), strings);

        public static string JoinWith(this IEnumerable strings, string chars) => string.Join(chars, strings);

        public static bool ContainsControlChar(this string text)
        {
            if (text == null) return false;
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

        public static bool Contains(this string text, char ch) => text.Cast<char>().Any(c => c == ch);

        public static bool ContainsAny(this string text, params char[] charsToFind)  => charsToFind.Any(ch => text.Cast<char>().Any(c => c == ch));
        public static bool ContainsAny(this string text, IEnumerable<char> charsToFind) => charsToFind.Any(ch => text.Cast<char>().Any(c => c == ch));

        public static bool ContainsAll(this string text, params char[] charsToFind )
        {
            if (text == null) throw new NullReferenceException($"string cannot be null");
            return charsToFind.All(ch => text.Cast<char>().Any(c => c == ch));
        }

        public static string NotNull(this string text) => string.IsNullOrEmpty(text) ? "" : text;

        public static string NotNull(this string text, string alternate)
        {
            if (text != null) return text;
            return !string.IsNullOrEmpty(alternate) ? alternate : "";
        }

        public static IList<string> SplitAndKeepDelimiters(this string s, params char[] delimiters)
        {
            var parts = new List<string>();
            if (string.IsNullOrEmpty(s)) return parts;
            var iFirst = 0;
            do
            {
                var iLast = s.IndexOfAny(delimiters, iFirst);
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
            kvp = split?.Length == 2 ? new KeyValuePair<string, string>(split[0],split[1]) : new KeyValuePair<string, string>();
            return split?.Length == 2;
        }

        public static T[] ParseDelimitedString<T>(this string message, char[] delimiter, Func<string, T> convertFunc, StringSplitOptions options = StringSplitOptions.None)
        {
            var split = message.Split(delimiter, options);
            var result = new T[split.Length];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = convertFunc(split[i]);
            }
            return result;
        }

        public delegate bool TryParseDelegate<T>(string input, out T output);

        public static T TryParse<T>(this string text, TryParseDelegate<T> tryParseFunction )
        {
            T value;
            return tryParseFunction(text, out value) ? value : default(T);
        }

        public static bool TryParseDelimitedString<T>(this string message, char[] delimiter, TryParseDelegate<T> tryConvertFunc , StringSplitOptions options, out List<T> result, out int successCount)
        {
            successCount = 0;
            if (!string.IsNullOrEmpty(message))
            {
                var split = message.Split(delimiter, options);
                if (split != null)
                {
                    result = new List<T>(split.Length);
                    foreach (var item in split)
                    {
                        T output;
                        if (tryConvertFunc.Invoke(item?.Trim(), out output))
                        {
                            result.Add(output);
                            successCount++;
                        }
                        else if (options == StringSplitOptions.None) result.Add(output);
                    }
                    return successCount == split.Length;
                }
            }
            result = new List<T>();
            return false;
        }

        public static string LettersOnly(this string source) 
            => string.IsNullOrWhiteSpace(source) ? "" : source.FilterCharacters(char.IsLetter);

        public static string LettersOnly(this string source, char[] acceptableChars)
        {
            if (string.IsNullOrWhiteSpace(source)) return "";
            if (acceptableChars == null || acceptableChars.Length == 0) return LettersOnly(source);

            return source.FilterCharacters((c) => char.IsLetter(c) || acceptableChars.Contains(c));
        }

        public static string LettersAndNumbersOnly(this string source) 
            => string.IsNullOrWhiteSpace(source) ? "" : source.FilterCharacters(char.IsLetterOrDigit);

        public static string LettersAndNumbersOnly(this string source, char[] acceptableChars)
        {
            if (string.IsNullOrWhiteSpace(source)) return "";
            if (acceptableChars == null || acceptableChars.Length == 0) return LettersAndNumbersOnly(source);

            return source. FilterCharacters((c) => char.IsLetterOrDigit(c) || acceptableChars.Contains(c));
        }

        public static string FilterCharacters(this string text, Func<char,bool> filter )
        {
            if (string.IsNullOrWhiteSpace(text)) return "";
            if (filter == null) return text;
            var result = new char[text.Length+1];
            var index = 0;
            foreach(var ch in text)
            {
                if (ch == '\0') break;
                if (filter(ch)) result[index++] = ch;
            }
           // result[index] = '\0';
            return new string(result,0,index);
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
            var len = source.Length;
            if (occurrence > 0)
            {   // Search from head
                for (var y = 0; y < len; y++)
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
                for (var y = len-1; y >=0; y--)
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



        public static IEnumerable<string> SplitAtIndex(this string source, IEnumerable<int> indexes)
        {
            var previousSplit = 0;
            foreach(var index in indexes)
            {
                yield return source.Substring(previousSplit, index - previousSplit);
                previousSplit = index;
            }
            yield return source.Substring(previousSplit);
        }

        public static IEnumerable<string> SplitAtIndex(this string source, IEnumerable<int> indexes, StringSplitIndexOptions options)
        {
            string substring;
            var previousSplit = 0;
            foreach (var index in indexes)
            {
                substring = source.Substring(previousSplit, index - previousSplit);
                if (!options.HasFlag(StringSplitIndexOptions.RemoveEmptyEntries) || !string.IsNullOrWhiteSpace(substring))
                {
                    yield return substring;
                }
                previousSplit = index;
                if(options.HasFlag(StringSplitIndexOptions.RemoveIndexChar)) previousSplit++;
            }
            substring = source.Substring(previousSplit);
            if (!options.HasFlag(StringSplitIndexOptions.RemoveEmptyEntries) || !string.IsNullOrWhiteSpace(substring))
            {
                yield return substring;
            }
        }

        public static string ReplaceChars(this string str, char[] chars, char replaceChar)
        {
            if (str == null) return null;
            if (!str.ContainsAny(chars)) return str;

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
            var prevIndex = 0;
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
            if (str == null) return null;
            var before = str.Substring(0,index);
            var after = str.Substring(index, str.Length-index);
            return trimChars == null ? new[] {before, after} : new[] { before?.Trim(trimChars), after?.Trim(trimChars) };
        }

        public static IEnumerable<string> Trim(this string[] strings, char[] trimChars) => strings.Select(str => str?.Trim(trimChars));

        public static IEnumerable<string> Trim(this string[] strings, char[] trimChars, StringSplitOptions options)
        {
            return options == StringSplitOptions.None
                ? strings.Trim(trimChars)
                : strings.Select(str => str?.Trim(trimChars)).Where(trim => trim?.Length > 0);
        }

        public static IEnumerable<string> Trim(this IEnumerable<string> strings, string[] trimStrings) => strings.Select(str => str?.Trim(trimStrings));
        public static IEnumerable<string> Trim(this IEnumerable<string> strings, string[] trimStrings, StringSplitOptions options)
        {
            return options == StringSplitOptions.None
                ? strings.Trim(trimStrings)
                : strings.Select(str => str?.Trim(trimStrings)).Where(trim => trim?.Length > 0);
        }

        public static string Trim(this string text, string[] trimStrings)
        {
            if (string.IsNullOrEmpty(text)) return text;
            var trimmed = text;
            int length;
            do
            {
                length = trimmed.Length;
                foreach (var trim in trimStrings)
                {
                    if (trimmed.StartsWith(trim)) trimmed = trimmed.Substring(trim.Length);
                    if (trimmed.EndsWith(trim)) trimmed = trimmed.Substring(0, trimmed.Length - trim.Length);
                }
            } while (length > 0 && length != trimmed.Length);
            return trimmed;
        }
    }
}
