﻿#region Copyright (c) 2015-2018 Visyn
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
using System.Windows;

namespace Visyn.Geometry
{

    public class PointXYZ : PointXY , IPoint3D, IComparable<IPoint3D>
    {
        public double Z { get; }
        public PointXYZ(Point point) : base(point)
        {
            Z = 0.0;
        }
        public PointXYZ(IPoint point) : base(point)
        {
            Z = 0.0;
        }

        public PointXYZ(double x, double y, double z) : base(x,y)
        {
            Z = z;
        }

        public static PointXYZ operator +(PointXYZ p1, PointXYZ p2) => new PointXYZ(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
        public static PointXYZ operator -(PointXYZ p1, PointXYZ p2) => new PointXYZ(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);

  //      public static explicit operator Point(PointXYZ point) => new Point(point.X, point.Y);

        //public static PointXYZ operator +(PointXYZ p1, Vector v1) => new PointXYZ(p1.X + v1.X, p1.Y + v1.Y, p1.Z + v1.Z);
        //public static PointXYZ operator -(PointXYZ p1, Vector v1) => new PointXYZ(p1.X - v1.X, p1.Y - v1.Y, p1.Z - v1.Z);

        public static PointXYZ operator *(PointXYZ p, double d) => new PointXYZ(p.X*d, p.Y*d, p.Z*d);

        [Obsolete("Backing field, do not use!")]
        private static PointXYZ _zero;

#pragma warning disable 618
        public new static PointXYZ Zero => _zero ?? (_zero = new PointXYZ(0, 0, 0));
#pragma warning restore 618

        #region Overrides of Object

        /// <summary>Compares the current object with another object of the same type.</summary>
        /// <returns>A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This object is equal to <paramref name="other" />. Greater than zero This object is greater than <paramref name="other" />. </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(IPoint3D other) => IPoint3DExtensions.CompareTo(this,other);

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => $"{X},{Y},{Z}";

        #endregion
    }
}