﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IslandLanding.Helper
{
  public class HttpLoggingHandler : DelegatingHandler
  {
    public HttpLoggingHandler(HttpMessageHandler innerHandler = null)
        : base(innerHandler ?? new HttpClientHandler())
    { }
    async protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
      var req = request;
      var id = Guid.NewGuid().ToString();
      var msg = $"[{id} -  Request]";

      Debug.WriteLine($"{msg}========Start==========");
      Debug.WriteLine($"{msg} {req.Method} {req.RequestUri.PathAndQuery} {req.RequestUri.Scheme}/{req.Version}");
      Debug.WriteLine($"{msg} Host: {req.RequestUri.Scheme}://{req.RequestUri.Host}");

      foreach (var header in req.Headers)
        Debug.WriteLine($"{msg} {header.Key}: {string.Join(", ", header.Value)}");

      if (req == null)
        return null;
      if (req.Content != null)
      {
        foreach (var header in req.Content.Headers)
          Debug.WriteLine($"{msg} {header.Key}: {string.Join(", ", header.Value)}");

        if (req.Content is StringContent || this.IsTextBasedContentType(req.Headers) || this.IsTextBasedContentType(req.Content.Headers))
        {
          string result = "FAILURE";
          try
          {
            result = await req.Content.ReadAsStringAsync();
          }
          catch (Exception ex)
          {
            Console.WriteLine("HttpLoggingHandler: " + ex.Message);
          }
          Debug.WriteLine($"{msg} Content:");
          Debug.WriteLine($"{msg} {string.Join("", result.Cast<char>().Take(255))}...");
        }
      }

      var start = DateTime.Now;
      var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
      var end = DateTime.Now;
      Debug.WriteLine($"{msg} Duration: {end - start}");
      Debug.WriteLine($"{msg}==========End==========");
      msg = $"[{id} - Response]";
      Debug.WriteLine($"{msg}=========Start=========");
      var resp = response;
      Debug.WriteLine($"{msg} {req.RequestUri.Scheme.ToUpper()}/{resp.Version} {(int)resp.StatusCode} {resp.ReasonPhrase} {req.RequestUri.PathAndQuery}");
      foreach (var header in resp.Headers)
        Debug.WriteLine($"{msg} {header.Key}: {string.Join(", ", header.Value)}");
      if (resp == null && resp.Content != null)
      {
        foreach (var header in resp.Content.Headers)
          Debug.WriteLine($"{msg} {header.Key}: {string.Join(", ", header.Value)}");

        if (resp.Content is StringContent || this.IsTextBasedContentType(resp.Headers) || this.IsTextBasedContentType(resp.Content.Headers))
        {
          start = DateTime.Now;
          var result = await resp.Content.ReadAsStringAsync();
          end = DateTime.Now;
          Debug.WriteLine($"{msg} Content:");
          Debug.WriteLine($"{msg} {string.Join("", result.Cast<char>().Take(255))}...");
          Debug.WriteLine($"{msg} Duration: {end - start}");
        }
      }
      Debug.WriteLine($"{msg}==========End==========");
      return response;
    }

    readonly string[] types = new[] { "html", "text", "xml", "json", "txt", "x-www-form-urlencoded" };
    bool IsTextBasedContentType(HttpHeaders headers)
    {
      IEnumerable<string> values;
      if (!headers.TryGetValues("Content-Type", out values))
        return false;
      var header = string.Join(" ", values).ToLowerInvariant();
      return types.Any(t => header.Contains(t));
    }
  }
}