using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ArchitectNET.Core._Internal_;

namespace ArchitectNET.Core
{
    /// <summary>
    /// Auxiliary static class which provides methods for checking some runtime assertions.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// Ensures that some argument of the method is NOT <see langword="null" />. <see cref="ArgumentNullException" /> is thrown
        /// if argument is <see langword="null" />.
        /// </summary>
        /// <typeparam name="TValue">
        /// Type of the respective method's parameter. Generic approach is used due to performance reasons. If the actual
        /// <typeparamref name="TValue" /> is some other type parameter <code>TX</code>, it's possible to avoid boxing when
        /// <code>TX</code> will be substituted by value-type in the future.
        /// </typeparam>
        /// <param name="value"> Actual argument value. </param>
        /// <param name="parameterName"> Mathod's parameter name. </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentNotNull<TValue>(TValue value, string parameterName)
        {
            if (value == null)
                ThrowArgumentNullException(parameterName);
        }

        /// <summary>
        /// Internal method for throwing <see cref="ArgumentNullException" /> with additional stack-trace information. This method
        /// is needed for reducing IL which would be generated for <see cref="ArgumentNotNull{TValue}" /> during its inlining.
        /// </summary>
        /// <param name="parameterName"> Name of parameter having <see langword="null" /> value. </param>
        /// <param name="stackFramesToSkip">
        /// Number of additional skipped stack frames during investigation of method's call stack.
        /// This parameter is needed for correct determination of the actual method where invalid <see langword="null" /> argument
        /// was passed.
        /// </param>
        /// <remarks>
        /// The method always throws <see cref="ArgumentNullException" />. If call stack investigation fails, the
        /// exception is thrown anyway but with a fewer information.
        /// </remarks>
        internal static void ThrowArgumentNullException(string parameterName, int stackFramesToSkip = 1)
        {
            parameterName = parameterName ?? "<undefined>";
            var stackTrace = new StackTrace();
            var callerFrame = stackTrace.GetFrame(stackFramesToSkip + 1);
            if (callerFrame == null)
            {
                throw new ArgumentNullException(parameterName,
                    Resources.FormatString("17CEEEC2-89D6-461F-A877-A2C7A25182E6",
                        parameterName));
            }
            var targetMethod = callerFrame.GetMethod();
            throw new ArgumentNullException(parameterName,
                Resources.FormatString("535A5B99-6F4C-4C39-B9EA-692A52369ED4",
                    parameterName,
                    targetMethod));
        }
    }
}