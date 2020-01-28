using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace UsersRestApi.Helpers
{
  /// <summary>
  /// A Middleware to simulate latency in a request
  /// </summary>
  public class LatencySimulationMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly int _minDelayInMs;
    private readonly int _maxDelayInMs;
    private readonly ThreadLocal<Random> _random;

    public LatencySimulationMiddleware(RequestDelegate next, TimeSpan minDelay, TimeSpan maxDelay)
    {
      _next = next;
      _minDelayInMs = (int) minDelay.TotalMilliseconds;
      _maxDelayInMs = (int) maxDelay.TotalMilliseconds;
      _random = new ThreadLocal<Random>(() => new Random());
    }


    /// <summary>
    /// Sets a random milliseconds of delay before continuing the request in the request pipeline
    /// </summary>
    /// <param name="context">Http Context of the request</param>
    /// <returns>The request delegate to continue</returns>
    public async Task Invoke(HttpContext context)
    {
      int delayInMs = _random.Value.Next(_minDelayInMs, _maxDelayInMs);
      await Task.Delay(delayInMs);
      await _next(context);
    }
  }
}
