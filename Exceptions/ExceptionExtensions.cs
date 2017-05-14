using System;
using System.Runtime.CompilerServices;

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool VerifyNotNullOrThrow(this IExceptionHandler handler, object itemToCheck,object sender, string message)
        {
            if (itemToCheck != null) return true;
            var nullReferenceException = new NullReferenceException(message);

            if (handler?.HandleException(sender, nullReferenceException) != true) throw nullReferenceException;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool VerifyNotNullOrThrow(this object itemToCheck, string message)
        {
            if (itemToCheck != null) return true;
            throw  new NullReferenceException(message);
        }
    }
}