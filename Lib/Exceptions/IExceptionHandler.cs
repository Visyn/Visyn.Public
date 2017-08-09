using System;

namespace Visyn.Public.Exceptions
{
    /// <summary>
    /// Interface IExceptionHandler
    /// </summary>
    public interface IExceptionHandler
    {
        /// <summary>
        /// Handles the exception
        /// If false is returned, sender should throw the exception.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="exception">The exception to handle.</param>
        /// <returns><c>true</c> if exception was handled, <c>false</c> otherwise.</returns>
        bool HandleException(object sender, Exception exception);
    }
}