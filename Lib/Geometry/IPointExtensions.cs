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

using System.Windows;

namespace Visyn.Geometry
{
    public static class IPointExtensions
    {
        public static Point ToPoint(this IPoint point) => new Point(point.X,point.Y);
        public static PointXY ToPointXY(this IPoint point) => new PointXY(point.X, point.Y);

        public static int CompareTo(this IPoint point, IPoint other)
        {
            if (point == null) point = PointXY.Zero;
            if (other == null) other = PointXY.Zero;

            // ReSharper disable MergeConditionalExpression
            var dx = point.X - other.X;
            var dy = point.Y - other.Y;
            // ReSharper restore MergeConditionalExpression
            if (dx + dy  > 0) return 1;
            if (dx + dy  < 0) return -1;
            if (dx == 0 && dy == 0 ) return 0;
            // Odd case, dx == -dy  and both non-zero...

            return (dx > dy) ? 1 : -1;
        }

        public static int CompareTo(this IPoint point, Point other)
        {
            if (point == null) point = PointXY.Zero;

            // ReSharper disable MergeConditionalExpression
            var dx = point.X - other.X;
            var dy = point.Y - other.Y;
            // ReSharper restore MergeConditionalExpression
            if (dx + dy > 0) return 1;
            if (dx + dy < 0) return -1;
            if (dx == 0 && dy == 0) return 0;

            // Odd case, dx == -dy  and both non-zero...
            return (dx > dy) ? 1 : -1;
        }
    }

}
