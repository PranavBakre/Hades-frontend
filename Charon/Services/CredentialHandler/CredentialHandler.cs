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
            
            return await base.SendAsync(request,token);
        }
    }
}
