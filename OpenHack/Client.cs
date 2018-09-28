using System;
using System.Net.Http;

namespace OpenHack
{
    public static class Client
    {
        private static readonly Lazy<HttpClient> _client = new Lazy<HttpClient>(() => new HttpClient());
        public static HttpClient Instance => _client.Value;
    }
}
