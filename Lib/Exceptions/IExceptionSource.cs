using System.Windows.Threading;

namespace Visyn.Public.Exceptions
{
    /// <summary>
    /// Interface marking class for delegation of exceptions to registered destination
    /// </summary>
    public interface IExceptionSource
    {
        /// <summary>
        /// Registers exception handler for exception routing
        /// If dispacher is specified, exception shall be thrown of thread specified by dispatcher
        /// </summary>
        /// <param name="exceptionHandler">Exception handler to delegate exception handling to</param>
        /// <param name="dispatcher">Dispatcher of thread to throw exception</param>
        void RegisterExceptionHandler(ExceptionHandler exceptionHandler, Dispatcher dispatcher);
    }
}