using System;
using System.Collections.Generic;
using System.Linq;

namespace Visyn.Public.Criteria
{
    public abstract class  ComparableCriteria<T> : ICriteria<T> where T : IComparable
    {
        public T Reference { get; set; }

        #region Implementation of ICriteria<T>

        public abstract bool MeetCriteria(T entity);

        public List<T> MeetCriteria(IEnumerable<T> entities)
        {
            return new List<T>(entities.Where(MeetCriteria));
        }

        #endregion
    }

    public class GreaterThanCriteria<T> : ComparableCriteria<T> where T : IComparable
    {
        #region Implementation of ICriteria<DateTime>

        public override bool MeetCriteria(T entity) { return entity.CompareTo(Reference) > 0; }

        #endregion
    }

    public class GreaterThanOrEqualCriteria<T> : ComparableCriteria<T> where T : IComparable
    {
        #region Implementation of ICriteria<DateTime>

        public override bool MeetCriteria(T entity) { return entity.CompareTo(Reference) >= 0; }

        #endregion
    }

    public class LessThanCriteria<T> : ComparableCriteria<T> where T : IComparable
    {
        #region Implementation of ICriteria<T>

        public override bool MeetCriteria(T entity) { return entity.CompareTo(Reference) < 0; }

        #endregion
    }
    public class LessThanOrEqualCriteria<T> : ComparableCriteria<T> where T : IComparable
    {
        #region Implementation of ICriteria<T>

        public override bool MeetCriteria(T entity) { return entity.CompareTo(Reference) <= 0; }

        #endregion
    }

    public class EqualsCriteria<T> : ComparableCriteria<T> where T : IComparable
    {
        #region Implementation of ICriteria<T>

        public override bool MeetCriteria(T entity) { return entity.CompareTo(Reference) == 0; }

        #endregion
    }

    public class AlwaysTrueCriteria<T> : ICriteria<T>
    {
        #region Implementation of ICriteria<T>

        public bool MeetCriteria(T entity) { return true; ; }

        public List<T> MeetCriteria(IEnumerable<T> entities) { return new List<T>(entities); }
        #endregion
    }

    public class AlwaysFalseCriteria<T> : ICriteria<T>
    {
        #region Implementation of ICriteria<T>

        public bool MeetCriteria(T entity) { return false; }

        public List<T> MeetCriteria(IEnumerable<T> entities) { return new List<T>(); }

        #endregion
    }
}