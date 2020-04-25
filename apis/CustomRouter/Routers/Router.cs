
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CustomRouter {
  public class MyRouter: IRouter {
    public Task RouteAsync(RouteContext rContext) {
      foreach (var item in rContext.RouteData.Values)
      {
        Console.WriteLine("Key: {0}, Value: {1}", item.Key, item.Value);
      }
      string number = rContext.RouteData.Values["number"] as string;

      if (string.IsNullOrEmpty(number)) {
        return Task.FromResult(0);
      }

      int value;

      if (!Int32.TryParse(number, out value)) {
        return Task.FromException(new Exception($"Failed to parse {number} as integer"));
      }

      Console.WriteLine("Request Path: {0}", rContext.HttpContext.Request.Path);
      rContext.Handler = async c => {
        int answer = value * 3;
        await c.Response.WriteAsync($"Fib[{number}] = {answer}");
      };

      return Task.FromResult(0);
    }

    public VirtualPathData GetVirtualPath(VirtualPathContext vPathContext) => null;
  }
}