namespace Visyn.Public.Automation
{
    public interface IExecute
    {
        /// <summary>
        /// Execute item
        /// </summary>
        /// <returns>Execution results</returns>
        ExecutionResult Execute();
    }

    /// <summary>
    /// Execute item on payload type T
    /// </summary>
    /// <typeparam name="T">Execution results of type T</typeparam>
    public interface IExecute<T> : IExecute where T : class
    {
        ExecutionResult<T> Execute(T payload);
    }

    /// <summary>
    /// Pipelined exectuion on payload type T
    /// </summary>
    /// <typeparam name="T">Payload for execution of type T</typeparam>
    /// <typeparam name="U">Result of execution with payload type U</typeparam>
    public interface IExecute<T,U> : IExecute
    {
        ExecutionResult<U> Execute(T payload);
    }
}