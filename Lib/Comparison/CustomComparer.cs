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


        public int Compare(T x, T y) => CompareFunction(x, y);

        #region Implementation of IComparer

        //[Obsolete("How to get System.Collections.Comparer.Default in portable class library???")]
        public int Compare(object x, object y)
        {
            if (x is T && y is T)
                return CompareFunction((T) x, (T) y);

            throw new NotImplementedException($"The following has not been tested!!!");
            //return (Comparer<T>)
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
