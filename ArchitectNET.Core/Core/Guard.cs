using System;
using System.Diagnostics;
using ArchitectNET.Core._Internal_;

namespace ArchitectNET.Core
{
    public static class Guard
    {
        public static void ArgumentNotNull<TValue>(TValue value, string parameterName)
        {
            if (value != null)
                return;
            parameterName = parameterName ?? "<undefined>";
            var stackTrace = new StackTrace();
            var callerFrame = stackTrace.GetFrame(1);
            if (callerFrame == null)
            {
                throw new ArgumentNullException(parameterName,
                    Resources.FormatString("17CEEEC2-89D6-461F-A877-A2C7A25182E6", parameterName));
            }
            var targetMethod = callerFrame.GetMethod();
            throw new ArgumentNullException(parameterName,
                Resources.FormatString("535A5B99-6F4C-4C39-B9EA-692A52369ED4", parameterName, targetMethod));
        }
    }
}