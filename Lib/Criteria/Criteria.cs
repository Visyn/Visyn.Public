using System.Collections.Generic;
using System.Linq;

namespace Visyn.Public.Criteria
{
    /// <summary>
    /// Interface ICriteria : Marker interface for ICriteria<T>
    /// </summary>
    public interface ICriteria
    {

    }

    public interface ICriteria<T> : ICriteria
    {
        bool MeetCriteria(T entity);
        List<T> MeetCriteria(IEnumerable<T> entities);
    }

    internal class AndCriteria<T> : ICriteria<T>
    {
        private readonly ICriteria<T> _criteria;
        private readonly ICriteria<T> _otherCriteria;

        internal AndCriteria(ICriteria<T> criteria, ICriteria<T> otherCriteria)
        {
            _criteria = criteria;
            _otherCriteria = otherCriteria;
        }

        public bool MeetCriteria(T entity)
        {
            return _criteria.MeetCriteria(entity) && _otherCriteria.MeetCriteria(entity);
        }

        public List<T> MeetCriteria(IEnumerable<T> entities)
        {
            return new List<T>(entities.Where(MeetCriteria));
        }
    }

    internal class OrCriteria<T> : ICriteria<T>
    {
        private readonly ICriteria<T> _criteria;
        private readonly ICriteria<T> _otherCriteria;

        internal OrCriteria(ICriteria<T> criteria, ICriteria<T> otherCriteria)
        {
            _criteria = criteria;
            _otherCriteria = otherCriteria;
        }

        public bool MeetCriteria(T entity)
        {
            return _criteria.MeetCriteria(entity) || _otherCriteria.MeetCriteria(entity);
        }

        public List<T> MeetCriteria(IEnumerable<T> entities)
        {
            return new List<T>(entities.Where(MeetCriteria));
        }
    }

    internal class NotCriteria<T> : ICriteria<T>
    {
        private readonly ICriteria<T> _criteria;

        internal NotCriteria(ICriteria<T> x)
        {
            _criteria = x;
        }

        public bool MeetCriteria(T entity)
        {
            return !_criteria.MeetCriteria(entity);
        }

        public List<T> MeetCriteria(IEnumerable<T> entities)
        {
            return new List<T>(entities.Where(MeetCriteria));
        }
    }

    public static class Criteria
    {
        public static ICriteria<T> And<T>(this ICriteria<T> criteria, ICriteria<T> otherCriteria)
        {
            return new AndCriteria<T>(criteria, otherCriteria);
        }

        public static ICriteria<T> And<T>(ICollection<ICriteria<T>> criteriaList)
        {
            if(criteriaList == null || criteriaList.Count == 0) return new AlwaysTrueCriteria<T>();
            ICriteria<T> criteria = null;
            foreach (var crit in criteriaList)
            {
                if (criteria == null)
                {
                    criteria = crit;
                    continue;
                }
                criteria = criteria.And(crit);
            }
            return criteria;
        }

        public static ICriteria<T> Or<T>(this ICriteria<T> criteria, ICriteria<T> otherCriteria)
        {
            return new OrCriteria<T>(criteria, otherCriteria);
        }

        public static ICriteria<T> Or<T>(ICollection<ICriteria<T>> criteriaList)
        {
            if (criteriaList == null || criteriaList.Count == 0) return new AlwaysTrueCriteria<T>();
            ICriteria<T> criteria = null;
            foreach (var crit in criteriaList)
            {
                if (criteria == null)
                {
                    criteria = crit;
                    continue;
                }
                criteria = criteria.Or(crit);
            }
            return criteria;
        }

        public static ICriteria<T> Not<T>(this ICriteria<T> criteria)
        {
            return new NotCriteria<T>(criteria);
        }
    }
}
