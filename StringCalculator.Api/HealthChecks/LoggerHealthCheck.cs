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
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            const string logFile = "../logs/log.txt";

            try
            {
                File.OpenWrite(logFile);
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
