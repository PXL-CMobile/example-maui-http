using MauiHttp.Models;
using System.Net.Http.Headers;
using System.Text;

namespace MauiHttp.Services
{
    public class HTTPService : IHTTPService
    {
        private HttpClient _httpClient;
        private JsonSerializerOptions _serializerOptions;
        private const string _URI = "DEV_TUNNEL_HERE";
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

        public async Task<bool> Login(string userEmail, string userPassword)
        {
            Uri uri = new Uri(string.Format(_URI + "Authentication/login", string.Empty));

            var loginObject = new { email = userEmail, password = userPassword };
            string json = JsonSerializer.Serialize(loginObject);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var token = JsonSerializer.Deserialize<LoginModel>(responseData, _serializerOptions);
                _token = token?.Token;
                return true;
            }
            return false;
        }

        public async Task<ObservableCollection<Actor>> GetFavoriteActors()
        {
            return new ObservableCollection<Actor>();
        }

        public async Task<ObservableCollection<Actor>> GetActors()
        {
            Uri uri = new Uri(string.Format(_URI + "actors", string.Empty));
            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var actorList = JsonSerializer.Deserialize<List<Actor>>(content, _serializerOptions);
                return new ObservableCollection<Actor>(actorList);
            }
            else
            {
                // Throw exception -> bad request? 
                return new ObservableCollection<Actor>();
            }
        }

    }
}
