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
    }
}