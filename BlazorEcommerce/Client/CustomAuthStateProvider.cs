using System.Security.Claims;
using System.Text.Json;

namespace BlazorEcommerce.Client
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _httpClient;

        public CustomAuthStateProvider(ILocalStorageService localStorageService, HttpClient httpClient)
        {
            _localStorageService = localStorageService;
            _httpClient = httpClient;
        }

        // this will grap the auth token from localstorage and then pass the claims and the create a
        // new claims identity and notify the components that needs if the user is authenticated or not.
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // get token from local storage
            string authToken = await _localStorageService.GetItemAsStringAsync("authToken");

            var identity = new ClaimsIdentity();
            // user is not authorizes as a default
            _httpClient.DefaultRequestHeaders.Authorization = null;

            // pars the claims and set new claims
            if (!string.IsNullOrWhiteSpace(authToken))
            {
                try
                {
                    identity = new ClaimsIdentity(ParsClaimsFromJwt(authToken), "jwt");
                    // remove qoutation marks and set a new valid auth header.
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken.Replace("\"", ""));
                }
                catch (Exception e)
                {
                    await _localStorageService.RemoveItemAsync("authToken");
                    identity = new ClaimsIdentity();
                }             
            }
            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }
        private byte[] ParseBase64WithOutPadding(string base64)
        {
            switch(base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
        private IEnumerable<Claim> ParsClaimsFromJwt(string authToken)
        {
            var payload = authToken.Split(".")[1];
            var jsonBytes = ParseBase64WithOutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            var claims = keyValuePairs.Select(kv => new Claim(kv.Key, kv.Value.ToString()));

            return claims;
        }
    }
}
