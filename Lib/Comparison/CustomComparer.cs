#region Copyright (c) 2015-2018 Visyn
// The MIT License(MIT)
// 
// Copyright (c) 2015-2018 Visyn
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
using Visyn.JetBrains;

namespace Visyn.Comparison
{
    public class CustomComparer<T> :  IComparer<T>, IComparer
    {
        [NotNull]
        public Func<T, T, int> CompareFunction { get; }
        public CustomComparer(Func<T,T,int> comparer )
        {
            if(comparer == null) throw new ArgumentNullException(nameof(comparer),"Comparer function must be non-null");
            CompareFunction = comparer;
        }


        // ReSharper disable once PossibleNullReferenceException
        public int Compare(T x, T y) => CompareFunction(x, y);

        #region Implementation of IComparer

        //[Obsolete("How to get System.Collections.Comparer.Default in portable class library???")]
        public int Compare(object x, object y)
        {
            if (x is T && y is T)
                // ReSharper disable once PossibleNullReferenceException
                return CompareFunction((T) x, (T) y);

//            throw new NotImplementedException($"The following has not been tested!!!");
          
            var comparableX = x as IComparable;
            if (comparableX != null) return comparableX.CompareTo(y);
            var comparableY = y as IComparable;
            if (comparableY != null) return comparableY.CompareTo(x) * -1;
            throw new NotImplementedException($"How to get System.Collections.Comparer.Default in portable class library???");
            //return System.Collections.Comparer.Default.Compare(x,y);
        }

        #endregion

        private static Func<double, double, int> tolerancedDouble(double tolerance) 
            => (a, b) => Math.Abs(a - b) < tolerance ? 0 : Comparer<double>.Default.Compare(a, b);

        public static CustomComparer<double> ToleranceDouble(double tolerance) 
            => new CustomComparer<double>(tolerancedDouble(tolerance));
    }
}
