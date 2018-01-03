#region Copyright (c) 2015-2018 Visyn
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

using System.Collections.Generic;
using System.Linq;

namespace Visyn.Criteria
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