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

namespace Visyn.Geometry
{
    // ReSharper disable once InconsistentNaming
    public static class IPoint3DExtensions
    {
        private const double TOLERANCE = 1e-12;

        public static IPoint3D Average(this IEnumerable<IPoint3D> points)
        {
            var x = 0.0;
            var y = 0.0;
            var z = 0.0;
            var count = 0;
            foreach (var point in points)
            {
                x += point.X;
                y += point.Y;
                z += point.Z;
                count++;
            }
            return new PointXYZ(x / count, y / count, z / count);
        }

        public static bool ContainsValue<TPoint>(this IEnumerable<TPoint> points, TPoint value, double tolerance=TOLERANCE) where TPoint : IPoint3D 
            => points.Any(point => point.Distance(value) < tolerance);

        public static int AddUniquePoints<TPoint>(this IList<TPoint> points, IEnumerable<TPoint> pointsToAdd, double tolerance = TOLERANCE) where TPoint : IPoint3D
        {
            if (pointsToAdd == null) return 0;

            var count = 0;
            
            foreach(var point in pointsToAdd)
            {
                if (points.ContainsValue(point, tolerance)) continue;
                points.Add(point);
                count++;
            }
            return count;
        }

        public static IList<TPoint> UniquePoints<TPoint>(this IEnumerable<TPoint> points, double tolerance = TOLERANCE) where TPoint : IPoint3D
        {
            var result = new List<TPoint>();

            if (points == null) return result;
            foreach(var point in points)
            {
                if(!result.ContainsValue(point,tolerance))    result.Add(point);
            }
            return result;
        }


        public static IList ToList<T>(this IPoint3D point, Func<double,T> convertFunc) where T : IComparable
        {
            if(convertFunc == null)
            {
                if (typeof(T) == typeof(double)) return (new[] { point.X, point.Y, point.Z }).ToList();
                if (typeof(T) == typeof(object)) return (new object[] { point.X, point.Y, point.Z }).ToList();
                if (typeof(T) == typeof(double)) return (new string[] { point.X.ToString(), point.Y.ToString(), point.Z.ToString() }).ToList();
                throw new NotSupportedException($"IPoint.ToList() requires convert function to create a list of type '{typeof(T).Name}'");
            }
            var list = new List<T> {convertFunc(point.X), convertFunc(point.Y)};
            return list;
        }

        public static List<double> ToListDouble(this IPoint3D point) => new List<double>(new[] {point.X, point.Y, point.Z});
        public static List<object> ToListObject(this IPoint3D point) => new List<object>(new object [] { point.X, point.Y, point.Z });

        public static double Distance(this IPoint3D a, IPoint3D b) => Math.Sqrt(Math.Pow(a.X - b.X, 2.0) + Math.Pow(a.Y - b.Y, 2.0) + Math.Pow(a.Z - b.Z, 2.0));

        public static int CompareTo(this IPoint3D point, IPoint3D other)
        {
            if (point == null) point = PointXYZ.Zero;
            if (other == null) other = PointXYZ.Zero;
            
            // ReSharper disable MergeConditionalExpression
            var dx = point.X - other.X;
            var dy = point.Y - other.Y;
            var dz = point.Z - other.Z;
            // ReSharper restore MergeConditionalExpression
            if (dx + dy + dz > 0) return 1;
            if (dx + dy + dz < 0) return -1;
            if (dx == 0 && dy == 0 && dz == 0) return 0;
            // Odd case, dx == dy == dz and both non-zero...
            return (dx > dy) ? 1 : -1;
        }
    }
}
