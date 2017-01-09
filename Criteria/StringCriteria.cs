using System.Collections.Generic;
using System.Linq;

namespace Visyn.Public.Criteria
{
    public abstract class StringCriteria : ICriteria<string>
    {
        public string Reference { get; set; }

        protected StringCriteria() {}

        protected StringCriteria(string reference)
        {
            Reference = reference;
        }

        #region Implementation of ICriteria<string>

        public abstract bool MeetCriteria(string entity);

        public List<string> MeetCriteria(IEnumerable<string> entities)
        {
            return new List<string>(entities.Where(MeetCriteria));
        }

        #endregion

        #region Overrides of Object

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => $"{this.GetType().Name}: {Reference}";

        #endregion
    }

    public class StringContainsCriteria : StringCriteria
    {
        public StringContainsCriteria() { }

        public StringContainsCriteria(string reference) : base(reference) {}

        #region Overrides of StringCriteria

        public override bool MeetCriteria(string entity)
        {
            return entity.Contains(Reference);
        }

        #endregion

        #region Overrides of StringCriteria

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => $"String.Contains: {Reference}";

        #endregion
    }

    public class StringStartsWithCriteria : StringCriteria
    {
        public StringStartsWithCriteria() { }

        public StringStartsWithCriteria(string reference) : base(reference) { }
        #region Overrides of StringCriteria

        public override bool MeetCriteria(string entity)
        {
            return entity.StartsWith(Reference);
        }

        #endregion

        #region Overrides of StringCriteria

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => $"String.StartWith: {Reference}";

        #endregion
    }

    public class StringEqualsCriteria : StringCriteria
    {
        public StringEqualsCriteria() { }

        public StringEqualsCriteria(string reference) : base(reference) { }

        #region Overrides of StringCriteria

        public override bool MeetCriteria(string entity)
        {
            return entity.Equals(Reference);
        }

        #endregion

        #region Overrides of StringCriteria

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => $"String.Equals: {Reference}";

        #endregion
    }
}