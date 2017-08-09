using System.Collections.Generic;

namespace Visyn.Public.Automation
{
    public class ExecutionResult<T> : ExecutionResult
    {
        public T Result { get; set; }

        public ExecutionResult(bool success, bool keepExecuting) : base(success, keepExecuting) { }

        public ExecutionResult(bool success, bool keepExecuting,T result) : base(success, keepExecuting)
        {
            Result = result;
        }
    }

    public class ExecutionResult
    {
        public bool Success { get; set; }
        public bool KeepExecuting { get; set; }

        public object Payload { get; set; }

        public ExecutionResult(bool success, bool keepExecuting)
        {
            Success = success;
            KeepExecuting = keepExecuting;
        }
    }
}