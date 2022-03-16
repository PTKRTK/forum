namespace ActiveForum.Client.Auth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Security.Claims;
    using System.Text.Json;
    using System.Threading.Tasks;
    using ActiveForum.Client.Helpers;
    using ActiveForum.Shared.DTOs;
    using ActiveForum.Shared.Interfaces;
    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.JSInterop;

    public class JWTAuthenticationStateProvider : AuthenticationStateProvider, ILoginService
    {
        private readonly IJSRuntime js;
        private readonly HttpClient httpClient;
        private readonly IAccountsRepository accountsRepository;
        private readonly string tokenkey = "TOKENKEY";
        private readonly string expirationkey = "EXPIRATIONTOKENKEY";

        public JWTAuthenticationStateProvider(IJSRuntime js, HttpClient httpClient, IAccountsRepository usersRepository)
        {
            this.js = js;
            this.httpClient = httpClient;
            this.accountsRepository = usersRepository;
        }

        private AuthenticationState Anonymous =>
            new (new ClaimsPrincipal(new ClaimsIdentity()));

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await js.GetFromLocalStorage(tokenkey);

            if (string.IsNullOrEmpty(token))
            {
                return Anonymous;
            }

            var expirationTimeString = await js.GetFromLocalStorage(expirationkey);

            if (DateTime.TryParse(expirationTimeString, out DateTime expirationTime))
            {
                if (IsTokenExpired(expirationTime))
                {
                    await CleanUp();
                    return Anonymous;
                }

                if (ShouldRenewToken(expirationTime))
                {
                    token = await RenewToken(token);
                }
            }

            return BuildAuthenticationState(token);
        }

        public async Task Login(UserToken userToken)
        {
            await js.SetInLocalStorage(tokenkey, userToken.Token);
            await js.SetInLocalStorage(expirationkey, userToken.Expiration.ToString());
            var authState = BuildAuthenticationState(userToken.Token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task Logout()
        {
            await CleanUp();
            NotifyAuthenticationStateChanged(Task.FromResult(Anonymous));
        }

        public async Task<string> GetToken()
        {
            var token = await js.GetFromLocalStorage(tokenkey);

            if (string.IsNullOrEmpty(token))
            {
                return "empty";
            }

            return token;
        }

        public async Task TryRenewToken()
            {
            var expirationTimeString = await js.GetFromLocalStorage(expirationkey);

            if (DateTime.TryParse(expirationTimeString, out DateTime expirationTime))
            {
                if (IsTokenExpired(expirationTime))
                {
                    await Logout();
                }

                if (ShouldRenewToken(expirationTime))
                {
                    var token = await js.GetFromLocalStorage(tokenkey);
                    var newToken = await RenewToken(token);
                    var authState = BuildAuthenticationState(newToken);
                    NotifyAuthenticationStateChanged(Task.FromResult(authState));
                }
            }
        }

        public AuthenticationState BuildAuthenticationState(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
        }

        private async Task<string> RenewToken(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var newToken = await accountsRepository.RenewToken();
            await js.SetInLocalStorage(tokenkey, newToken.Token);
            await js.SetInLocalStorage(expirationkey, newToken.Expiration.ToString());
            return newToken.Token;
        }

        private bool ShouldRenewToken(DateTime expirationTime)
        {
            return expirationTime.Subtract(DateTime.UtcNow) < TimeSpan.FromMinutes(5);
        }

        private bool IsTokenExpired(DateTime expirationTime)
        {
            return expirationTime <= DateTime.UtcNow;
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }

            return Convert.FromBase64String(base64);
        }

        private async Task CleanUp()
        {
            await js.RemoveItem(tokenkey);
            await js.RemoveItem(expirationkey);
            httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
