using System;
using System.Collections.Generic;

namespace Visyn.Io
{
    public class OutputToCollectionSeverity : OutputToCollection<object>
    {
        public OutputToCollectionSeverity(ICollection<object> collection, Action<IEnumerable<object>> addRangeFunction) 
            : base(collection, addRangeFunction)
        {
        }
    }
}