using Microsoft.AspNetCore.Builder;
using System;

namespace UsersRestApi.Helpers
{
  public static class ExtensionMethods
  {
    /// <summary>
    /// Extension method to add LatencySimulation middleware to the application builder
    /// </summary>
    /// <param name="app">ApplicationBuilder to which the latency simulator needs to be added</param>
    /// <param name="minDelay">Minimum delay for latency</param>
    /// <param name="maxDelay">Maximum delay for latency</param>
    /// <returns>ApplicationBuilder with LatencySimulationMiddleware</returns>
    public static IApplicationBuilder UseLatencySimulation(this IApplicationBuilder app, TimeSpan minDelay, TimeSpan maxDelay)
    {
      return app.UseMiddleware(typeof(LatencySimulationMiddleware), minDelay, maxDelay);
    }
  }
}
