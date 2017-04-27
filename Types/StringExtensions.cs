using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Visyn.Public.Types
{   
    public static class StringExtensions
    {
        [Obsolete("Use item?ToString() ?? string.Empty",true)]
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


        public static string FromBytes(byte[] bytes)
        {
            var chars = new char[bytes.Length];
            for (var i = 0; i < bytes.Length; i++)
            {
                chars[i] = (char)bytes[i];
            }
            return new string(chars);
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

        public static bool ContainsChar(this string text, IEnumerable<char> charsToFind) => charsToFind.Any(ch => text.Cast<char>().Any(c => c == ch));

        public static string NotNull(this string text) => string.IsNullOrEmpty(text) ? "" : text;

        public static string NotNull(this string text, string alternate)
        {
            if (!string.IsNullOrEmpty(text)) return text;
            return !string.IsNullOrEmpty(alternate) ? alternate : "";
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

        public static bool TryParseDelimitedString<T>(this string message, char[] delimiter, TryParseDelegate<T> convertFunc, StringSplitOptions options, out T[] result, out int successCount)
        {
            if (string.IsNullOrEmpty(message))
            {
                result = new T[0];
                successCount = 0;
                return false;
            }
            var split = message.Split(delimiter, options);

            successCount = 0;
            if (options == StringSplitOptions.None)
            {
                result = new T[split.Length];
                for (var i = 0; i < split.Length; i++)
                {
                    T output;
                    if (convertFunc.Invoke(split[i], out output))
                    {
                        successCount++;
                    }
                    result[i] = output;
                }
                return successCount == split.Length;
            }
            else if(options == StringSplitOptions.RemoveEmptyEntries)
            {
                var resultList = new List<T>(split.Length);
                foreach (string item in split)
                {
                    T output;
                    if (convertFunc.Invoke(item.Trim(), out output))
                    {
                        resultList.Add( output);
                        successCount++;
                    }
                }
                result = resultList.ToArray();
                return successCount == split.Length;
            }
            throw new ArgumentOutOfRangeException(nameof(options),$"StringSplitOptions value not supported [{options}]");
        }

        public static string LettersOnly(this string source)
        {
            if (string.IsNullOrWhiteSpace(source)) return "";
            return source.FilterCharacters(char.IsLetter);
        }

        public static string LettersOnly(this string source, char[] acceptableChars)
        {
            if (string.IsNullOrWhiteSpace(source)) return "";
            if (acceptableChars == null || acceptableChars.Length == 0) return LettersOnly(source);

            return source.FilterCharacters((c) => char.IsLetter(c) || acceptableChars.Contains(c));
        }

        public static string LettersAndNumbersOnly(this string source)
        {
            if (string.IsNullOrWhiteSpace(source)) return "";
            return source.FilterCharacters(char.IsLetterOrDigit);
        }

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
