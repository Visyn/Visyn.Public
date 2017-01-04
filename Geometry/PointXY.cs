using System;
using System.Windows;

namespace Visyn.Public.Geometry
{
    public class PointXY : IPoint , IComparable<IPoint>
    {
        public PointXY(Point point)
        {
            X = point.X;
            Y = point.Y;
        }
        public PointXY(IPoint point)
        {
            X = point.X;
            Y = point.Y;
        }

        public PointXY(double x, double y)
        {
            X = x;
            Y = y;
        }
        public static PointXY operator +(PointXY t1, PointXY t2) => new PointXY(t1.X + t2.X, t1.Y + t2.Y);
        public static PointXY operator -(PointXY t1, PointXY t2) => new PointXY(t1.X - t2.X, t1.Y - t2.Y);

        public static explicit operator Point(PointXY point) => new Point(point.X, point.Y);

        #region Implementation of IPoint

        public double X { get; }

        public double Y { get; }

        #endregion

        #region Overrides of Object

        /// <summary>Compares the current object with another object of the same type.</summary>
        /// <returns>A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This object is equal to <paramref name="other" />. Greater than zero This object is greater than <paramref name="other" />. </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(IPoint other)
        {
            var dx = X - other.X;
//            var ct = X.CompareTo(other.X);
            var dy = Y - other.Y;
//            var cty = Y.CompareTo(other.Y);
            if (dx + dy > 0) return 1;
            if (dx + dy < 0) return -1;
            if (dx == 0 && dy == 0) return 0;
            // Odd case, dx == dy and both non-zero...
            return (dx > dy) ? 1 : -1;
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{X},{Y}";
        }

        #endregion
    }
}