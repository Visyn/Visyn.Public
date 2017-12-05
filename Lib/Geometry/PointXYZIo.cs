using System;
using System.Xml.Serialization;
using Visyn.Types;

namespace Visyn.Geometry
{
    public class PointXYZIo : IPoint3D, IComparable<IPoint3D>, IValue<IPoint3D>, IConvertTo<IPoint3D>, IComparable<PointXYZIo>
    {
        #region Implementation of IPoint3D

        public double X
        {
            get { return Point3D.X; }
            set { Point3D = new PointXYZ(value, Point3D.Y, Point3D.Z); }
        }

        public double Y
        {
            get { return Point3D.Y; }
            set { Point3D = new PointXYZ(Point3D.X, value, Point3D.Z); }
        }

        public double Z
        {
            get { return Point3D.Z; }
            set { Point3D = new PointXYZ(Point3D.X, Point3D.Y, value); }
        }

        #endregion

        protected IPoint3D Point3D { get; private set; }
        public PointXYZIo() : this(0,0,0) { }

        public PointXYZIo(IPoint3D point3D)
        {
            Point3D = point3D;
        }

        public PointXYZIo(double x, double y, double z)
        {
            Point3D = new PointXYZ(x,y,z);
        }



        #region Relational members

        public int CompareTo(IPoint3D other) => IPoint3DExtensions.CompareTo(this,other);
        public int CompareTo(PointXYZIo other) => IPoint3DExtensions.CompareTo(this, other);

        public int CompareTo(IPoint other) => IPointExtensions.CompareTo(this, other);

        #endregion

        #region Implementation of IValue

        public Type Type => typeof(IPoint3D);
        public object ValueAsObject() => Point3D;

        [XmlIgnore]
        public IPoint3D Value => Point3D;

        #endregion

        #region Implementation of IConvertTo<out IPoint3D>

        public IPoint3D ConvertTo() => Point3D;

        #endregion

        #region Overrides of Object

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => $"{X},{Y},{Z}";

        #endregion
    }
}