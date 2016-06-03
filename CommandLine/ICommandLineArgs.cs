using System;

namespace Visyn.Public.CommandLine
{
    /// <summary>
    /// Interface ICommandLineArgs
    /// Interface for defining and parsing command line arguments
    /// </summary>
    public interface ICommandLineArgs
    {
        /// <summary>
        /// Try to parse command line arguments
        /// </summary>
        /// <param name="args">The arguments to parse.</param>
        /// <param name="result">The parsed result if successful.  Null if parse failed.</param>
        /// <returns><c>true</c> if arguments were successfuly parsed, <c>false</c> otherwise.</returns>
        bool TryParse(string[] args, out ICommandLineArgs result);

        /// <summary>
        /// Try to parse command line arguments as a specific type
        /// </summary>
        /// <typeparam name="T">Type to parse as</typeparam>
        /// <param name="args">The arguments to parse.</param>
        /// <param name="result">The parsed result if successful.  null if parse failed.</param>
        /// <returns><c>true</c> if arguments were successfuly parsed, <c>false</c> otherwise.</returns>
        bool TryParse<T>(string[] args, out T result) where T : class;
    }
}
