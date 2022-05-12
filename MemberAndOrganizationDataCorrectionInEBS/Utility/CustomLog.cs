using MemberAndOrganizationDataCorrectionInEBS.Model;
using Newtonsoft.Json;
using System;
using System.Threading;

namespace MemberAndOrganizationDataCorrectionInEBS.Utility
{
    public static class CustomLog
    {
        public static string GetCustomLog(string logInfo, string logLevel, string message, int? httpStatusCode = null)
        {
            ////HttpContext context = HttpContext.Current;
            string userId = null;
            string finalJson = null;

            ////if (context != null)
            ////{
            ////    var request = context.Request;
            ////    var headers = request.Headers;
            ////    if (headers.GetValues("UserId") != null)
            ////    {
            ////        userId = headers.GetValues("UserId") != null && headers.GetValues("UserId").Length > 0 ? headers.GetValues("UserId")[0].ToString() : null;
            ////    }

            ////    var hostName = Dns.GetHostName();

            ////    var log = new NLogDetails
            ////    {
            ////        TimeStamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
            ////        LogInfo = logInfo,
            ////        LogLevel = logLevel,
            ////        ThreadId = Thread.CurrentThread.ManagedThreadId,
            ////        TenantId = "ASTM Tenant",
            ////        AccountId = "ASTM Account",
            ////        UserId = userId,
            ////        Api = request.Url.LocalPath,
            ////        Message = message,
            ////        HttpMethod = request.HttpMethod,
            ////        HostName = hostName,
            ////        HttpStatusCode = httpStatusCode
            ////    };

            ////    finalJson = JsonConvert.SerializeObject(log);
            ////}

            var log = new NLogDetails
            {
                TimeStamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                LogInfo = logInfo,
                LogLevel = logLevel,
                ThreadId = Thread.CurrentThread.ManagedThreadId,
                TenantId = "ASTM Tenant",
                AccountId = "ASTM Account",
                UserId = userId,
                Api = string.Empty,
                Message = message,
                HttpMethod = string.Empty,
                HostName = string.Empty,
                HttpStatusCode = httpStatusCode
            };

            finalJson = JsonConvert.SerializeObject(log);

            return finalJson;
        }

        public static LogEntry GetCustomWarnLog(double elapsedTime, string queryName)
        {
            int statusCode = 200;
            string message = "This query took " + elapsedTime + " milliseconds to respond - " + queryName;
            string finalJson = GetCustomLog("LoggingUserSystemAPI", "WARNING", message, statusCode);
            return new LogEntry(LogLevel.WARN, finalJson);
        }
    }
}
