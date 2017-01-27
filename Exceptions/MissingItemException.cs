using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visyn.Public.Exceptions
{
    public class MissingItemException : Exception, IVisynException
    {
        public MissingItemException(string message, Exception innerException) : base(message,innerException)
        {
        }

        public static MissingItemException ItemMissing(string source, string itemName, Type itemtype, Exception innerException)
        {
            return new MissingItemException($"Item missing: {itemName} of type '{itemtype.Name}' from {source}", innerException);
        }

        public static MissingItemException MandatoryItemMissing(string source, string itemName, Type itemtype, Exception innerException)
        {
            return new MissingItemException($"Mandatory Item missing: {itemName} of type '{itemtype.Name}' from {source}", innerException);
        }
    }
}
