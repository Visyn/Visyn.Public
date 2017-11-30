
using System.Windows;

namespace Visyn.Geometry
{
    public static class IPointExtensions
    {
        public static Point ToPoint(this IPoint point) => new Point(point.X,point.Y);
        public static PointXY ToPointXY(this IPoint point) => new PointXY(point.X, point.Y);
    }
}
