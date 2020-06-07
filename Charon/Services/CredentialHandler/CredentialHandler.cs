using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Charon.Services
{
    public class CredentialHandler: DelegatingHandler
    {
        private readonly ILocalStorageService _localStorageService;

        public CredentialHandler(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
        {
            if (!request.Headers.Contains("credentials"))
            {
                var credentials = await _localStorageService.GetItemAsync<string>("Credentials");
                if (!string.IsNullOrEmpty(credentials))
                {
                    request.Headers.Add("Credentials", credentials);
                }
                
            }
            request.Headers.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("Charon","1.0"));
            
            Console.WriteLine(request.Headers.UserAgent.ToString());
            return await base.SendAsync(request,token);
        }
    }
}
