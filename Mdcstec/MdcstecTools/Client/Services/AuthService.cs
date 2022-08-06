using Blazored.LocalStorage;
using MdcstecTools.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MdcstecTools.Client.Services
{
    // Press Ctrl-R, Ctrl-I to Extract an Interface
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<RegisterResponse> Register(RegisterRequest registerRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("account", registerRequest);
            return await response.Content.ReadFromJsonAsync<RegisterResponse>();
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            //var loginAsJson = JsonSerializer.Serialize(loginRequest);
            //var response = await _httpClient.PostAsync("login", 
            //    new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
            //var loginResponse = JsonSerializer.Deserialize<LoginResponse>(
            //    await response.Content.ReadAsStringAsync(), 
            //    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var response = await _httpClient.PostAsJsonAsync("login", loginRequest);
            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (!response.IsSuccessStatusCode)
            {
                return loginResponse;
            }

            await _localStorage.SetItemAsync("authToken", loginResponse.Token);
            ((ApiAuthenticationStateProvider)_authenticationStateProvider)
                .MarkUserAsAuthenticated(loginRequest.Email);
            _httpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("bearer", loginResponse.Token);

            return loginResponse;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider)
                .MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
