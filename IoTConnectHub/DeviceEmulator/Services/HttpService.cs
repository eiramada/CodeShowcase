using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace DeviceEmulator.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> SendTemperatureDataAsync(double temperature, int deviceId)
        {
            var deviceData = new { DeviceId = deviceId, DataType = 2, Value = temperature };
            string url = $"{_httpClient.BaseAddress}api/data/{deviceId}";
            return await SendDataAsync(url, deviceData);
        }

        public async Task<int?> AuthenticateDeviceAsync(string deviceName, string token, string proprtyName)
        {
            SetAuthorizationHeader(token);
            string url = $"{_httpClient.BaseAddress}api/devices/authenticate?deviceName={deviceName}";
            return await SendGetRequestForId(url, proprtyName);
        }

        public async Task<int?> RegisterUserAsync(string username, string password)
        {
            var user = new { Username = username, Password = password };
            string url = $"{_httpClient.BaseAddress}api/users/register";
            return await SendPostRequestForId(url, user);
        }

        public async Task<Tuple<int?, string>> LoginUserAsync(string username, string password)
        {
            var user = new { Username = username, Password = password };
            string url = $"{_httpClient.BaseAddress}api/auth/login";
            return await SendLoginRequest(url, user);
        }

        public async Task<int?> RegisterDeviceAsync(string deviceName, string token, int userId, string propertyName)
        {
            SetAuthorizationHeader(token);
            var device = new { DeviceName = deviceName, UserId = userId };
            string url = $"{_httpClient.BaseAddress}api/devices";
            return await SendPostRequestForId(url, device, propertyName);
        }

        private async Task<bool> SendDataAsync(string url, object data)
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            if (!response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Failed to send data. Status: {response.StatusCode}, Details: {responseContent}");
                return false;
            }

            Console.WriteLine("Data sent successfully to device");
            return true;
        }

        private async Task<int?> SendGetRequestForId(string url, string propertyName)
        {
            var response = await _httpClient.GetAsync(url);
            return await ProcessResponseForId(response, propertyName);
        }

        private async Task<int?> SendPostRequestForId(string url, object data, string propertyName = "userId")
        {
            var response = await SendPostRequest(url, data);
            return await ProcessResponseForId(response, propertyName);
        }

        private async Task<Tuple<int?, string>> SendLoginRequest(string url, object data)
        {
            HttpResponseMessage response = await SendPostRequest(url, data);
            if (!response.IsSuccessStatusCode) return Tuple.Create<int?, string>(null, string.Empty);

            var responseContent = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject<dynamic>(responseContent);
            return Tuple.Create((int?)result.userId, (string)result.token);
        }

        private async Task<HttpResponseMessage> SendPostRequest(string url, object data)
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync(url, content);
        }

        private async Task<int?> ProcessResponseForId(HttpResponseMessage response, string propertyName)
        {
            if (!response.IsSuccessStatusCode) return null;

            string responseContent = await response.Content.ReadAsStringAsync();
            dynamic? result = JsonConvert.DeserializeObject<dynamic>(responseContent);

            if (result != null)
            {
                if (int.TryParse(result.ToString(), out int directId))
                {
                    return directId;
                }

                if (result is JObject obj)
                {
                    if (obj.TryGetValue(propertyName, out JToken IdToken) && int.TryParse(IdToken.ToString(), out int Id))
                    {
                        return Id;
                    }
                }

                if (result.userId != null)
                {
                    if (int.TryParse(result.userId.ToString(), out int propertyUserId))
                    {
                        return propertyUserId;
                    }
                }
            }
            return null;
        }

        private void SetAuthorizationHeader(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}


