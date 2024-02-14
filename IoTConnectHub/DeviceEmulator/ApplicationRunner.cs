using DeviceEmulator.Services;

namespace DeviceEmulator
{
    public class ApplicationRunner
    {
        private readonly IEmulatorService _emulatorService;
        private readonly IHttpService _httpService;
        private string _userToken = string.Empty;

        public ApplicationRunner(IEmulatorService emulatorService, IHttpService httpService)
        {
            _emulatorService = emulatorService;
            _httpService = httpService;
        }

        public async Task RunAsync()
        {
            int? userId = await HandleUserRegistrationAndLoginAsync();
            if (!userId.HasValue) return;

            int? deviceId = await HandleDeviceOperationsAsync(userId.Value, "deviceId");
            if (!deviceId.HasValue) return;

            Console.WriteLine("Device authenticated successfully.");

            double minTemp = ReadDoubleFromConsole("Please enter the minimum temperature:");
            double maxTemp = ReadDoubleFromConsole("Please enter the maximum temperature:");
            if (minTemp >= maxTemp)
            {
                Console.WriteLine("Minimum temperature must be less than maximum temperature.");
                return;
            }

            await RunDeviceEmulationAsync(minTemp, maxTemp, deviceId.Value);
        }

        private async Task<int?> HandleUserRegistrationAndLoginAsync()
        {
            if (PromptUserChoice("Do you want to register a new user? (yes/no)"))
            {
                string? username = ReadFromConsole("Enter username for registration:");
                string? password = ReadFromConsole("Enter password:");
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    Console.WriteLine("Registration failed.");
                    return null;
                }

                var userId = await _httpService.RegisterUserAsync(username, password);
                if (!userId.HasValue)
                {
                    Console.WriteLine("Registration failed.");
                    return null;
                }
            }

            var loginUsername = ReadFromConsole("Enter username for login:");
            var loginPassword = ReadFromConsole("Enter password:");
            if (!string.IsNullOrEmpty(loginUsername) || !string.IsNullOrEmpty(loginPassword))
            {
                var loginResponse = await _httpService.LoginUserAsync(loginUsername, loginPassword);
                if (loginResponse.Item1.HasValue)
                {
                    SetUserToken(loginResponse.Item2);
                    return loginResponse.Item1;
                }
            }

            Console.WriteLine("Login failed.");
            return null;
        }

        private async Task<int?> HandleDeviceOperationsAsync(int userId, string propertyName = "userId")
        {
            string? deviceName;
            if (PromptUserChoice("Do you want to add a new device? (yes/no)"))
            {
                deviceName = ReadFromConsole("Enter device name:");
                return await _httpService.RegisterDeviceAsync(deviceName, _userToken, userId, propertyName);
            }

            deviceName = ReadFromConsole("Enter device name:");
            return await _httpService.AuthenticateDeviceAsync(deviceName, _userToken, propertyName);
        }

        private async Task RunDeviceEmulationAsync(double minTemp, double maxTemp, int deviceId)
        {
            using var cancellationTokenSource = new CancellationTokenSource();
            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = true;
                cancellationTokenSource.Cancel();
            };

            while (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                try
                {
                    double temperature = _emulatorService.GenerateRandomTemperature(minTemp, maxTemp);
                    Console.WriteLine($"Generated Temperature: {temperature}°C");
                    await _httpService.SendTemperatureDataAsync(temperature, deviceId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

                await Task.Delay(1000, cancellationTokenSource.Token);
            }
        }
        private static string? ReadFromConsole(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        private static bool PromptUserChoice(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine().Equals("yes", StringComparison.OrdinalIgnoreCase);
        }

        private static double ReadDoubleFromConsole(string message)
        {
            while (true)
            {
                Console.WriteLine(message);
                if (double.TryParse(Console.ReadLine(), out double result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }
        }
        private void SetUserToken(string token)
        {
            _userToken = token;
        }
    }
}
