using System.Windows;

namespace Visyn.Public.Geometry
{
    public class PointXY : IPoint 
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

        public static explicit operator Point(PointXY point) => new Point(point.X, point.Y);

        #region Implementation of IPoint

        public double X { get; }

        public double Y { get; }

        #endregion

        #region Overrides of Object

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{X},{Y}";
        }

        #endregion
    }
}