using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Security.AccessControl;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace StringCalculator.Api.HealthChecks
{
    [Produces("application/json")]
    public class LoggerHealthCheck : IHealthCheck
    {
        private readonly string logFilePath;

        public LoggerHealthCheck(string logFilePath)
        {
            this.logFilePath = logFilePath;
        }
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                File.OpenWrite(logFilePath);
                return Task.FromResult(
                    HealthCheckResult.Healthy("A healthy result."));
            }
            catch (Exception e)
            {
                return Task.FromResult(
                    new HealthCheckResult(context.Registration.FailureStatus,
                        "An unhealthy result."));
            }
        }
    }
}
