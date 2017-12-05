using System;
using System.Collections.Generic;
using System.Windows;

namespace Visyn.Geometry
{
    public static class SizeExtensions
    {
        public static Size Average(this IEnumerable<Size> points)
        {
            var width = 0.0;
            var height = 0.0;
            var count = 0;
            foreach (var point in points)
            {
                width += point.Width;
                height += point.Height;
                count++;
            }
            return new Size(width / count, height / count);
        }

        public static double Distance(this Size a, Size b) => Math.Sqrt(Math.Pow(a.Width - b.Width, 2) + Math.Pow(a.Height - b.Height, 2));

        public static double Slope(this Size p1, Size p2) => (p1.Height - p2.Height) / (p1.Width - p2.Width);

        public static Size Project(this Size p1, Size p2, double distance)
        {
            var deltaZ = p1.Distance(p2);
            var ratio = distance / deltaZ;

            return new Size(p2.Width + ratio * (p1.Width - p2.Width), p2.Height + ratio * (p1.Height - p2.Height));
        }

        public static Size Multiply(this Size size, double factor) => new Size(size.Width * factor, size.Height * factor);

        public static Size Divide(this Size size, double factor) => new Size(size.Width / factor, size.Height / factor);
    }
}
