using MauiHttp.Models;
using System.Net.Http.Headers;
using System.Text;

namespace MauiHttp.Services
{
    public class HTTPService : IHTTPService
    {
        private HttpClient _httpClient;
        private JsonSerializerOptions _serializerOptions;
        private const string _URI = "https://YOURDEVTUNNELHERE/api/";
        private string? _token;

        public HTTPService()
        {
            _httpClient = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        private Uri BuildUri(string endpoint)
        {
            return new Uri($"{_URI}{endpoint}");
        }

        private void SetAuthorizationHeader()
        {
            if (!string.IsNullOrEmpty(_token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Preferences.Get("Bearer", _token));
            }
        }

        private async Task<T?> DeserializeResponse<T>(HttpResponseMessage response)
        {
            string content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content, _serializerOptions);
        }

        public async Task<bool> Login(string userEmail, string userPassword)
        {
            var uri = BuildUri("Authentication/login");
            var loginObject = new { email = userEmail, password = userPassword };
            var content = new StringContent(JsonSerializer.Serialize(loginObject), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, content);
            if (!response.IsSuccessStatusCode) return false;

            var tokenModel = await DeserializeResponse<LoginModel>(response);
            _token = tokenModel?.Token;
            return true;
        }

        public async Task<bool> MakeActorFavorite(Guid id)
        {
            SetAuthorizationHeader();
            var uri = BuildUri($"actor/add-favorite/{id}");
            var response = await _httpClient.PostAsync(uri, null);
            return response.IsSuccessStatusCode;
        }

        public async Task<ObservableCollection<Actor>> GetFavoriteActors()
        {
            SetAuthorizationHeader();
            var uri = BuildUri("actors/my-actors");
            var response = await _httpClient.GetAsync(uri);

            if (!response.IsSuccessStatusCode) return new ObservableCollection<Actor>();

            var actorList = await DeserializeResponse<List<Actor>>(response);

            if (actorList == null)
            {
                actorList = new List<Actor>();
            }
            return new ObservableCollection<Actor>(actorList);
        }

        public async Task<ObservableCollection<Actor>> GetActors()
        {
            var uri = BuildUri("actors");
            var response = await _httpClient.GetAsync(uri);

            if (!response.IsSuccessStatusCode) return new ObservableCollection<Actor>();

            var actorList = await DeserializeResponse<List<Actor>>(response);
            if (actorList == null)
            {
                actorList = new List<Actor>();
            }
            return new ObservableCollection<Actor>(actorList);
        }

        public async Task AddFavoriteActor(int actorId)
        {
            SetAuthorizationHeader();
            var uri = BuildUri($"actors/add-favorite/{actorId}");
            var response = await _httpClient.PostAsync(uri, null);

            if (response.IsSuccessStatusCode)
            {
                // Show toast message
            }
        }

    }
}
