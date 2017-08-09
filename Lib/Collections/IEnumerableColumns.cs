using System.Collections.Generic;

namespace Visyn.Public.Collections
{
    public interface IEnumerableColumns
    {
        int Count { get; }
        IList<string> SelectionKeys { get; }

        IEnumerable<List<object>> ColumnData(IEnumerable<string> columnNames, bool includeColumnHeaders);
    }
}