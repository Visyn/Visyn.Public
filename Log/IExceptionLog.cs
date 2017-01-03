using Visyn.Public.Exceptions;

namespace Visyn.Public.Log
{
    /// <summary>
    /// Interface IExceptionLog
    /// Indicates a log can handle exceptions
    /// </summary>
    /// <typeparam name="TLogEntry">The type of the t log entry.</typeparam>
    /// <seealso cref="Visyn.Public.Log.ILog{TLogEntry}" />
    /// <seealso cref="Visyn.Public.Exceptions.IExceptionHandler" />
    public interface IExceptionLog<TEventLevel> : ILog<TEventLevel>, IExceptionHandler
    {
    }
}