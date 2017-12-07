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
using System.Windows;

namespace Visyn.Geometry
{
    public class PointXY : IPoint , IComparable<IPoint>, IComparable<Point>, IComparable<PointXY>
    {
        public PointXY() : this(0,0) { }

        public PointXY(Point point) : this(point.X,point.Y) { }
        public PointXY(Size size) : this(size.Height, size.Width) { }

        public PointXY(IPoint point) : this(point.X, point.Y) { }

        public PointXY(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static PointXY operator +(PointXY p1, IPoint p2) => new PointXY(p1.X + p2.X, p1.Y + p2.Y);
        public static PointXY operator -(PointXY p1, IPoint p2) => new PointXY(p1.X - p2.X, p1.Y - p2.Y);

        public static explicit operator Point(PointXY point) => new Point(point.X, point.Y);

        public static PointXY operator +(PointXY p1, Vector v1) => new PointXY(p1.X + v1.X, p1.Y + v1.Y);
        public static PointXY operator -(PointXY p1, Vector v1) => new PointXY(p1.X - v1.X, p1.Y - v1.Y);

        public static PointXY operator *(PointXY p, double d) => new PointXY(p.X*d, p.Y*d);
        public static PointXY operator /(PointXY p, double d) => new PointXY(p.X / d, p.Y / d);
        #region Implementation of IPoint

        public double X { get; }

        public double Y { get; }

        #endregion

        #region Overrides of Object

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => $"{X},{Y}";

        #endregion

        #region Relational members

        public int CompareTo(PointXY other) => IPointExtensions.CompareTo(this, other);

        /// <summary>Compares the current object with another object of the same type.</summary>
        /// <returns>A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This object is equal to <paramref name="other" />. Greater than zero This object is greater than <paramref name="other" />. </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(IPoint other) => IPointExtensions.CompareTo(this, other);

        public int CompareTo(Point other) => IPointExtensions.CompareTo(this, other);

        #endregion

        [Obsolete("Backing field, do not use!")]
        private static PointXY _zero;

#pragma warning disable 618
        public static PointXY Zero => _zero ?? (_zero = new PointXY(0, 0));
#pragma warning restore 618
    }
}