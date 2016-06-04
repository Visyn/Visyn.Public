using System;

namespace Visyn.Public.CommandLine
{
    /// <summary>
    /// Class CommandLineArgs base class called from all applications.
    /// </summary>
    /// <seealso cref="Visyn.Public.CommandLine.ICommandLineArgs" />
    public class CommandLineArgs : ICommandLineArgs
    {
        /// <summary>
        /// Try to parse command line arguments
        /// </summary>
        /// <param name="args">The arguments to parse.</param>
        /// <param name="result">The parsed result if successful.  Null if parse failed.</param>
        /// <returns><c>true</c> if arguments were successfully parsed, <c>false</c> otherwise.</returns>
        public virtual bool TryParse(string[] args, out ICommandLineArgs result)
        {
            if (args == null || args.Length == 0)
            {
                result = null;
                return false;
            }
            result = Parse(args);
            return result != null;
        }

        /// <summary>
        /// Try to parse command line arguments as a specific type
        /// </summary>
        /// <typeparam name="T">Type to parse as</typeparam>
        /// <param name="args">The arguments to parse.</param>
        /// <param name="result">The parsed result if successful.  null if parse failed.</param>
        /// <returns><c>true</c> if arguments were successfully parsed, <c>false</c> otherwise.</returns>
        public virtual bool TryParse<T>(string[] args, out T result) where T : class
        {
            result = null;
            if (args == null) return false;

            result = ParseAs<T>(args);
            return (result != null);
        }

        /// <summary>
        /// Try to parse command line arguments
        /// </summary>
        /// <param name="args">The arguments to parse.</param>
        /// <returns>The parsed result if successful.  Null if parse failed.</returns>
        public static ICommandLineArgs Parse(string[] args)
        {
            if (args == null) return null;
            return null;
        }

        /// <summary>
        /// Try to parse command line arguments as a specific type
        /// </summary>
        /// <typeparam name="T">Type to parse as</typeparam>
        /// <param name="args">The arguments to parse.</param>
        /// <returns>The parsed result if successful.  null if parse failed.</returns>
        public static T ParseAs<T>(string[] args) where T : class
        {
            if (args == null) return null;
            if (typeof(T) is ICommandLineArgs)
            {
                try
                {
                    var parser = Activator.CreateInstance(typeof(T)) as ICommandLineArgs;
                    if (parser != null)
                    {
                        T result;
                        parser.TryParse<T>(args, out result);
                        return result;
                    }
                }
                catch(Exception exc)
                {
                }
            }
            return null;
        }
    }
}
