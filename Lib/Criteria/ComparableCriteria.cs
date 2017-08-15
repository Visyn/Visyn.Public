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
using System.Collections.Generic;
using System.Linq;

namespace Visyn.Criteria
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