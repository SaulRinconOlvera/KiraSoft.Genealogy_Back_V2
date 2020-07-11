using Microsoft.Extensions.Logging;
using System;

namespace KiraSoft.CrossCutting.Operation.Executor
{
    public static class LoggInformation
    {
        public static void LogException(Exception ex, ILogger logger)
        {
            logger.LogError($"'{ex.Message}' {Environment.NewLine} in {ex.Source.Trim()} {Environment.NewLine} {ex.StackTrace.Trim()}", ex);

            if (ex.InnerException != null)
                LogException(ex.InnerException, logger);
        }
    }
}
