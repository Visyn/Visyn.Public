using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Visyn.Public.JetBrains;

namespace Visyn.Public.Exceptions
{
    public static class ExceptionExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Try(this IExceptionHandler handler, Action action)
        {
            try
            {
                action();
            }
            catch (Exception exc)
            {
                if (!handler.HandleException(handler, exc)) throw;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Try<T>(this IExceptionHandler handler, Func<T> funct)
        {
            try
            {
                return funct();
            }
            catch (Exception exc)
            {
                if (!handler.HandleException(handler, exc)) throw;
            }
            return default(T);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Obsolete("Misleading name!  Supress should not throw under any conditon", true)]
        public static void Supress<TExc>(this IExceptionHandler handler, Action action) 
            where TExc : Exception
        {
            try
            {
                action();
            }
            catch (TExc) { }    // Supressed
            catch(Exception exc)
            {
                if (!handler.HandleException(handler, exc)) throw;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Obsolete("Misleading name!  Supress should not throw under any conditon", true)]
        public static TOut Supress<TExc,TOut>(this IExceptionHandler handler, Func<TOut> funct)
            where TExc : Exception
        {
            try
            {
                return funct();
            }
            catch (TExc) { }    // Supressed
            catch (Exception exc)
            {
                if (!handler.HandleException(handler, exc)) throw;
            }
            return default(TOut);
        }

        [Obsolete("Not used",true)]
        public static bool SupressException(this IExceptionHandler handler, object sender, Exception exc) => true;

        public static bool SupressException(object sender, Exception exc) => true;

        /// <summary>
        /// Verifies the itemToCheck is not null or will throw ArgumentNullException.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemToCheck">The item to check.</param>
        /// <param name="parameter">The itemToCheck parameter name.</param>
        /// <param name="message">Optional:  Message to assign Exception.Message.</param>
        /// <returns>T. itemToCheck</returns>
        /// <exception cref="ArgumentNullException">If itemToCheck is null</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [NotNull]
        public static T NotNullOrThrow<T>(this T itemToCheck, string parameter, string message=null)
        {
            if (itemToCheck != null) return itemToCheck;
            throw new ArgumentNullException(parameter, string.IsNullOrEmpty(message) ? message = $"{parameter} can not be null!" : message);
        }

        public static IEnumerable<Exception> CheckForNullItems(IEnumerable<KeyValuePair<string,object>>  itemsToCheck, string message = null)
        {
            foreach(var item in itemsToCheck)
            {
                if (item.Value == null)   yield return new ArgumentNullException(item.Key, $"{item.Key} can not be null!");
            }
        }

        public static bool AllItemsNotNull(this IExceptionHandler handler, IEnumerable<KeyValuePair<string, object>> itemsToCheck, string message = null)
        {
            var success = true;
            foreach (var item in itemsToCheck)
            {
                if (item.Value != null) continue;
                success = false;
                var exc = new ArgumentNullException(item.Key, $"{item.Key} can not be null!");
                if (handler?.HandleException(null, exc) != true) throw exc;
            }
            return success;
        }

        public static int CheckItemsForNull(this IExceptionHandler handler, IEnumerable<KeyValuePair<string, object>> itemsToCheck)
        {
            var nullCount = 0;
            foreach (var item in itemsToCheck)
            {
                if (item.Value != null) continue;
                nullCount++;
                var exc = new ArgumentNullException(item.Key, $"{item.Key} can not be null!");
                if (handler?.HandleException(null, exc) != true) throw exc;
            }
            return nullCount;
        }


        /// <summary>
        /// Checks an object for null value and passes an ArgumentNullException to the IExceptionHandler if the item is null.
        /// </summary>
        /// <param name="handler">Exception handler</param>
        /// <param name="itemToCheck">Object to verify is not null</param>
        /// <param name="sender">Sending class and/or member name for logging</param>
        /// <param name="parameter">Property name to check.  nameof(itemToCheck)</param>
        /// <param name="message">Optional:  Message to assign Exception.Message</param>
        /// <returns>True if item is not null, false if item is null and exception was handled by handler </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Obsolete("Use Assignment Method instead", true)]
        public static bool NotNullOrThrow(this IExceptionHandler handler, object itemToCheck,object sender, string parameter, string message)
        {
            if (itemToCheck != null) return true;
            var argumentNullException = new ArgumentNullException(parameter,message);

            if (handler?.HandleException(sender, argumentNullException) != true) throw argumentNullException;
            return false;
        }

        /// <summary>
        /// Checks an object for null value and throws an ArgumentNullException if the item is null.
        /// </summary>
        /// <param name="itemToCheck">Object to verify is not null</param>
        /// <param name="parameter">Property name to check.  nameof(itemToCheck)</param>
        /// <param name="message">Optional:  Message to assign Exception.Message</param>
        /// <exception cref="ArgumentNullException">itemToCheck has null value</exception>
        /// <returns></returns>
        [Obsolete("Use Assignment Method instead", true)]
        public static bool NotNullOrThrow([CanBeNull]this object itemToCheck, string parameter, string message)
        {
            if (itemToCheck != null) return true;
            if (string.IsNullOrEmpty(message)) message = $"{parameter} can not be null!";
            throw  new ArgumentNullException(parameter,message);
        }

        [Obsolete("Use Assignment Method instead",true)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NotNullOrThrow(IEnumerable<KeyValuePair<string,object>> parameters)
        {
            Debug.Assert(parameters != null);
            foreach(var parameter in parameters)
            {
                if (parameter.Value == null) throw new ArgumentNullException(parameter.Key, $"{parameter.Key} can not be null!");
            }
            return true;
        }
    }
}