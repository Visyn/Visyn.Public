using System;

namespace Visyn.Public.Types
{
    public interface IType
    {
        /// <summary>
        /// Type marker.  Typically used to indicate underlying
        /// generic type.
        /// </summary>
        Type Type { get; }
    }
}
