namespace DeviceEmulator.Services
{
    public interface IHttpService
    {
        Task<int?> AuthenticateDeviceAsync(string deviceName, string token, string propertyName);
        Task<Tuple<int?, string>> LoginUserAsync(string username, string password);
        Task<int?> RegisterDeviceAsync(string deviceName, string token, int userId, string propertyName);
        Task<int?> RegisterUserAsync(string username, string password);
        Task<bool> SendTemperatureDataAsync(double temperature, int deviceId);
    }
}